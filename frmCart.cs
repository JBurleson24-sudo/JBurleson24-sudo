using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace BurlesonJLogonView
{
    public partial class frmCart : Form
    {
        private clsCart cart;

        public frmCart(clsCart shoppingCart)
        {
            InitializeComponent();

            cart = shoppingCart;

            dgvCart.DataSource = cart.Items;

            LoadCart();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void LoadCart()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = cart.Items;

            dgvCart.Columns["InventoryID"].Visible = false;
            dgvCart.Columns["ItemName"].HeaderText = "Game";
            dgvCart.Columns["Price"].HeaderText = "Price";
            dgvCart.Columns["Quantity"].HeaderText = "Qty";
            dgvCart.Columns["Total"].HeaderText = "Total";

            dgvCart.Columns["Price"].DefaultCellStyle.Format = "C";
            dgvCart.Columns["Total"].DefaultCellStyle.Format = "C";

            lblSubtotal.Text = cart.Subtotal.ToString("C");
            lblDiscount.Text = cart.DiscountAmount.ToString("C");
            lblTax.Text = cart.Tax.ToString("C");
            lblTotal.Text = cart.GrandTotal.ToString("C");

            if (dgvCart.Rows.Count > 0)
            {
                dgvCart.ClearSelection();
                dgvCart.CurrentCell = dgvCart.Rows[0].Cells["ItemName"];
            }
            UpdateControls();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null)
                return;

            int inventoryID =
                Convert.ToInt32(dgvCart.CurrentRow.Cells["InventoryID"].Value);

            cart.RemoveItem(inventoryID);

            LoadCart();
        }
        private void UpdateControls()
        {
            bool hasItems = cart.Items.Count > 0;

            btnRemoveItem.Enabled = hasItems;
            btnUpdateQuantity.Enabled = hasItems;
            btnCheckout.Enabled = hasItems;

            nudCartQuantity.Enabled = hasItems;
        }
        private void dgvCart_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null)
                return;

            if (dgvCart.CurrentRow.Index < 0)
                return;

            if (dgvCart.CurrentRow.IsNewRow)
                return;

            object value = dgvCart.CurrentRow.Cells["Quantity"].Value;

            if (value == null || value == DBNull.Value)
                return;

            int qty = Convert.ToInt32(value);

            if (qty < nudCartQuantity.Minimum)
                qty = (int)nudCartQuantity.Minimum;

            if (qty > nudCartQuantity.Maximum)
                qty = (int)nudCartQuantity.Maximum;

            nudCartQuantity.Value = qty;
        }

        private void btnUpdateQuantity_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null)
                return;

            int inventoryID =
                Convert.ToInt32(
                    dgvCart.CurrentRow.Cells["InventoryID"].Value);

            int stock =
                clsDatabase.GetInventoryQuantity(inventoryID);

            int requested =
                (int)nudCartQuantity.Value;

            if (requested > stock)
            {
                MessageBox.Show(
                    "Only " + stock +
                    " copies of this game are available.");

                nudCartQuantity.Value = stock;

                return;
            }

            cart.UpdateQuantity(
                inventoryID,
                requested);

            LoadCart();
        }

        private void btnApplyPromo_Click(object sender, EventArgs e)
        {
            string code = txtPromoCode.Text.Trim().ToUpper();

            clsDiscount discount = clsDatabase.GetDiscount(code);

            if (discount == null)
            {
                MessageBox.Show("Invalid or inactive Promotion Code.");
                return;
            }

            if (discount.DiscountLevel == 1 &&
                discount.InventoryID.HasValue &&
                cart.GetQuantityInCart(discount.InventoryID.Value) == 0)
            {
                MessageBox.Show(
                    "This promotion code does not apply to any item currently in your cart.");

                return;
            }

            cart.AppliedDiscount = discount;

            LoadCart();

            btnApplyPromo.Enabled = false;

            MessageBox.Show("Promotion Applied!");
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cart.Items.Count == 0)
            {
                MessageBox.Show("Your cart is empty.");
                return;
            }

            if (!clsValidation.ValidCreditCard(txtCardNumber.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid credit card number.\n" +
                    "Example: 1234-5678-1234-5678");

                txtCardNumber.Focus();
                return;
            }

            if (!clsValidation.ValidExpirationDate(txtExpiration.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid expiration date.\n" +
                    "Example: 07/2028");

                txtExpiration.Focus();
                return;
            }

            if (!clsValidation.ValidCCV(txtCCV.Text.Trim()))
            {
                MessageBox.Show("Please enter a valid 3-digit CCV.");

                txtCCV.Focus();
                return;
            }

            if (clsCurrentUser.PersonID == 0)
            {
                MessageBox.Show(
                    "You must login before completing a purchase.");

                frmLogin login = new frmLogin();
                login.ShowDialog();

                if (clsCurrentUser.PersonID == 0)
                {
                    return;
                }
            }

            int orderID = clsDatabase.PlaceOrder(
                clsCurrentUser.PersonID,
                cart,
                txtCardNumber.Text.Trim(),
                txtExpiration.Text.Trim(),
                txtCCV.Text.Trim());

            if (orderID > 0)
            {
                clsHTML.CreateReceipt(
                    cart,
                    orderID,
                    clsCurrentUser.PersonID);

                MessageBox.Show("Order placed successfully!");
                cart.Items.Clear();

                cart.AppliedDiscount = null;

                txtPromoCode.Clear();
                btnApplyPromo.Enabled = true;

                LoadCart();
            }
            else
            {
                MessageBox.Show("Checkout failed.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCart_Load(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
       !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCardNumber_TextChanged(object sender, EventArgs e)
        {
            string numbers = txtCardNumber.Text.Replace("-", "");

            if (numbers.Length > 16)
                numbers = numbers.Substring(0, 16);

            StringBuilder formatted = new StringBuilder();

            for (int i = 0; i < numbers.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                    formatted.Append("-");

                formatted.Append(numbers[i]);
            }

            int cursor = formatted.Length;

            txtCardNumber.TextChanged -= txtCardNumber_TextChanged;

            txtCardNumber.Text = formatted.ToString();

            txtCardNumber.SelectionStart = cursor;

            txtCardNumber.TextChanged += txtCardNumber_TextChanged;
        }

        private void txtCCV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
        !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtCCV_TextChanged(object sender, EventArgs e)
        {
            if (txtCCV.Text.Length > 3)
            {
                txtCCV.Text = txtCCV.Text.Substring(0, 3);
                txtCCV.SelectionStart = txtCCV.Text.Length;
            }
        }

        private void txtExpiration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
        !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtExpiration_TextChanged(object sender, EventArgs e)
        {
            string numbers = txtExpiration.Text.Replace("/", "");

            if (numbers.Length > 6)
                numbers = numbers.Substring(0, 6);

            StringBuilder formatted = new StringBuilder();

            if (numbers.Length <= 2)
            {
                formatted.Append(numbers);
            }
            else
            {
                formatted.Append(numbers.Substring(0, 2));
                formatted.Append("/");
                formatted.Append(numbers.Substring(2));
            }

            int cursor = formatted.Length;

            txtExpiration.TextChanged -= txtExpiration_TextChanged;

            txtExpiration.Text = formatted.ToString();
            txtExpiration.SelectionStart = cursor;

            txtExpiration.TextChanged += txtExpiration_TextChanged;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "CartHelp.txt");

            if (File.Exists(filePath))
            {
                Process.Start("notepad.exe", filePath);
            }
            else
            {
                MessageBox.Show("Help file not found.");
            }
        }
        private void frmCart_FormClosed(object sender, FormClosedEventArgs e)
        {
            cart.AppliedDiscount = null;

            txtPromoCode.Clear();
            btnApplyPromo.Enabled = true;

            LoadCart();
        }
    }
}
