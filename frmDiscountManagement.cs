using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BurlesonJLogonView
{
    public partial class frmDiscountManagement : Form
    {
        public frmDiscountManagement()
        {
            InitializeComponent();
        }
        private void LoadDiscounts()
        {
            DataTable dt =
                clsDatabase.GetDiscounts();

            DisplayDiscounts(dt);
        }
        private void DisplayDiscounts(DataTable dt)
        {
            dgvDiscounts.DataSource = dt;

            dgvDiscounts.Columns["DiscountID"].HeaderText =
                "ID";

            dgvDiscounts.Columns["DiscountCode"].HeaderText =
                "Code";

            dgvDiscounts.Columns["Description"].HeaderText =
                "Description";

            dgvDiscounts.Columns["AppliesTo"].HeaderText =
                "Applies To";

            dgvDiscounts.Columns["DiscountTypeName"].HeaderText =
                "Type";

            dgvDiscounts.Columns["DiscountPercentage"].HeaderText =
                "Percent";

            dgvDiscounts.Columns["DiscountDollarAmount"].HeaderText =
                "Dollar Amount";

            dgvDiscounts.Columns["StartDate"].HeaderText =
                "Start Date";

            dgvDiscounts.Columns["ExpirationDate"].HeaderText =
                "Expiration Date";

            dgvDiscounts.Columns["InventoryID"].HeaderText =
                "Item ID";
        }

        private void frmDiscountManagement_Load(object sender, EventArgs e)
        {
            LoadDiscounts();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender,EventArgs e)
        {
            string searchText =
                txtSearch.Text.Trim();

            if (searchText == "")
            {
                LoadDiscounts();
                return;
            }

            DataTable dt =
                clsDatabase.SearchDiscounts(
                    searchText);

            DisplayDiscounts(dt);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            LoadDiscounts();

            txtSearch.Focus();
        }

        private void btnAddDiscount_Click(object sender, EventArgs e)
        {
            using (frmDiscountEditor form =
       new frmDiscountEditor())
            {
                if (form.ShowDialog() ==
                    DialogResult.OK)
                {
                    LoadDiscounts();
                }
            }
        }

        private void btnEditDiscount_Click(object sender, EventArgs e)
        {
            if (dgvDiscounts.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a discount to edit.");

                return;
            }

            int discountID =
                Convert.ToInt32(
                    dgvDiscounts.CurrentRow
                        .Cells["DiscountID"].Value);

            using (frmDiscountEditor form =
                new frmDiscountEditor(discountID))
            {
                if (form.ShowDialog() ==
                    DialogResult.OK)
                {
                    LoadDiscounts();
                }
            }
        }

        private void btnDeleteDiscount_Click(object sender, EventArgs e)
        {
            if (dgvDiscounts.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a discount to delete.");

                return;
            }

            int discountID =
                Convert.ToInt32(
                    dgvDiscounts.CurrentRow
                        .Cells["DiscountID"].Value);

            string discountCode =
                dgvDiscounts.CurrentRow
                    .Cells["DiscountCode"].Value
                    .ToString();

            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to delete discount code '" +
                    discountCode + "'?",
                    "Delete Discount",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool success =
                    clsDatabase.DeleteDiscount(
                        discountID);

                if (success)
                {
                    MessageBox.Show(
                        "Discount deleted successfully.",
                        "Delete Discount",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadDiscounts();
                }
                else
                {
                    MessageBox.Show(
                        "The discount could not be deleted.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "The discount could not be deleted.\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string helpPath = Path.Combine(
                    Application.StartupPath,
                    "DiscountManagementHelp.txt");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "The Discount Management help file could not be found.",
                        "Help",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = helpPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The help file could not be opened.\n\n" +
                    ex.Message,
                    "Help Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
