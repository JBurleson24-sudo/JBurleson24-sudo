using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BurlesonJLogonView
{
    public partial class frmReturns : Form
    {
        public frmReturns()
        {
            InitializeComponent();
            txtReturnReason.TextChanged += (sender, e) => UpdateRefundPreview();
            nudReturnQuantity.ValueChanged += (sender, e) => UpdateRefundPreview();
        }
        private void UpdateRefundPreview()
        {
            btnProcessReturn.Enabled = false;
            lblRefundTotal.Text = "Refund Total: —";

            DataGridViewRow row = dgvReturnItems.CurrentRow;
            DataTable items = dgvReturnItems.DataSource as DataTable;

            if (row == null || row.IsNewRow ||
                items == null || nudReturnQuantity.Value <= 0)
            {
                return;
            }

            try
            {
                int detailID =
                    Convert.ToInt32(row.Cells["OrderDetailsID"].Value);

                decimal[] refund = clsDatabase.CalculateReturnRefund(
                    items,
                    detailID,
                    Convert.ToInt32(nudReturnQuantity.Value));

                lblRefundTotal.Text =
                    "Refund Total: " + (refund[0] + refund[1]).ToString("C2");

                btnProcessReturn.Enabled =
                    !string.IsNullOrWhiteSpace(txtReturnReason.Text) &&
                    clsCurrentUser.PersonID > 0;
            }
            catch (Exception ex)
            {
                lblRefundTotal.Text = "Refund unavailable";
                lblRefundTotal.AccessibleDescription = ex.Message;
            }
        }
        private void btnFindOrder_Click(object sender, EventArgs e)
        {
            dgvReturnItems.DataSource = null;

            int orderID;

            if (!int.TryParse(txtOrderID.Text.Trim(), out orderID) ||
                orderID <= 0)
            {
                MessageBox.Show("Please enter a valid order number.");
                txtOrderID.Focus();
                return;
            }

            try
            {
                DataTable items =
                    clsDatabase.GetOrderItemsForReturn(orderID);

                if (items.Rows.Count == 0)
                {
                    MessageBox.Show("No items were found for that order.");
                    return;
                }

                dgvReturnItems.DataSource = items;
                dgvReturnItems.Columns["ItemDiscountID"].Visible = false;
                dgvReturnItems.Columns["OrderDiscountAmount"].Visible = false;
                dgvReturnItems.Columns["OrderTaxAmount"].Visible = false;

                dgvReturnItems.Columns["OrderDetailsID"].Visible = false;
                dgvReturnItems.Columns["InventoryID"].Visible = false;

                dgvReturnItems.Columns["ItemName"].HeaderText = "Game";
                dgvReturnItems.Columns["UnitPrice"].HeaderText = "Sale Price";
                dgvReturnItems.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
                dgvReturnItems.Columns["UnitPrice"].DefaultCellStyle.NullValue =
                    "Unavailable";

                dgvReturnItems.Columns["PurchasedQuantity"].HeaderText = "Purchased";
                dgvReturnItems.Columns["ReturnedQuantity"].HeaderText = "Returned";
                dgvReturnItems.Columns["RemainingQuantity"].HeaderText = "Remaining";
                if (dgvReturnItems.Rows.Count > 0)
                {
                    dgvReturnItems.CurrentCell =
                        dgvReturnItems.Rows[0].Cells["ItemName"];

                    dgvReturnItems_SelectionChanged(
                        dgvReturnItems, EventArgs.Empty);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load the order:\n" + ex.Message);
            }
        }

        private void lblReturnReason_Click(object sender, EventArgs e)
        {

        }

        private void dgvReturnItems_SelectionChanged(object sender, EventArgs e)
        {
            btnProcessReturn.Enabled = false;
            lblRefundTotal.Text = "Refund Total: —";

            nudReturnQuantity.Enabled = false;
            nudReturnQuantity.Minimum = 0;
            nudReturnQuantity.Value = 0;

            DataGridViewRow row = dgvReturnItems.CurrentRow;

            if (row == null || row.IsNewRow ||
                !dgvReturnItems.Columns.Contains("RemainingQuantity"))
            {
                return;
            }

            object remainingValue = row.Cells["RemainingQuantity"].Value;

            if (remainingValue == null || remainingValue == DBNull.Value)
                return;

            int remaining = Convert.ToInt32(remainingValue);

            if (remaining <= 0)
                return;

            nudReturnQuantity.Maximum = remaining;
            nudReturnQuantity.Minimum = 1;
            nudReturnQuantity.Value = 1;
            nudReturnQuantity.Enabled = true;
            UpdateRefundPreview();
        }

        private void btnProcessReturn_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvReturnItems.CurrentRow;
            DataTable items = dgvReturnItems.DataSource as DataTable;

            int orderID;

            if (row == null || row.IsNewRow || items == null ||
                !int.TryParse(txtOrderID.Text.Trim(), out orderID))
            {
                MessageBox.Show("Find an order and select an item first.");
                return;
            }

            string reason = txtReturnReason.Text.Trim();

            if (reason.Length == 0)
            {
                MessageBox.Show("Please enter a return reason.");
                txtReturnReason.Focus();
                return;
            }

            int returnID;
            decimal total;

            try
            {
                int detailID =
                    Convert.ToInt32(row.Cells["OrderDetailsID"].Value);

                int quantity = Convert.ToInt32(nudReturnQuantity.Value);
                bool restock = chkRestock.Checked;

                decimal[] refund = clsDatabase.CalculateReturnRefund(
                    items, detailID, quantity);

                total = refund[0] + refund[1];

                DialogResult answer = MessageBox.Show(
                    "Record a return of " + quantity + " item(s)?\n\n" +
                    "Refund: " + total.ToString("C2") + "\n" +
                    "Restore inventory: " + (restock ? "Yes" : "No"),
                    "Confirm Return",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (answer != DialogResult.Yes)
                    return;

                btnProcessReturn.Enabled = false;

                returnID = clsDatabase.ProcessReturn(
                    orderID,
                    detailID,
                    quantity,
                    reason,
                    restock,
                    clsCurrentUser.PersonID,
                    total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Return was not recorded:\n" + ex.Message);
                btnFindOrder_Click(sender, e);
                return;
            }

            MessageBox.Show(
                "Return #" + returnID + " recorded.\n" +
                "Refund amount: " + total.ToString("C2"));

            txtReturnReason.Clear();
            chkRestock.Checked = false;

            btnFindOrder_Click(sender, e);
        }
    }
}
