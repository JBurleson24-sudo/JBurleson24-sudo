using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;




namespace BurlesonJLogonView
{
    public partial class frmPOS : Form
    {
        private int managerPersonID;
        private int selectedCustomerPersonID = -1;
        private clsCart cart = new clsCart();

        public frmPOS(int managerPersonID)
        {
            InitializeComponent();

            this.managerPersonID = managerPersonID;

            dgvProducts.DataSource =
                clsDatabase.GetInventory();
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void btnLookupCustomer_Click(object sender, EventArgs e)
        {
            using (frmPOSCustomerLookup form =
                new frmPOSCustomerLookup())
            {
                if (form.ShowDialog() ==
                    DialogResult.OK)
                {
                    selectedCustomerPersonID =
                        form.SelectedPersonID;

                    DataRow customer =
                        clsDatabase.GetCustomerInfo(
                            selectedCustomerPersonID);

                    if (customer != null)
                    {
                        lblSelectedCustomer.Text =
                            "Selected Customer: " +
                            customer["NameFirst"].ToString() +
                            " " +
                            customer["NameLast"].ToString();
                    }
                }
            }
        }

        private void btnTransactionHistory_Click(object sender, EventArgs e)
        {
            if (selectedCustomerPersonID <= 0)
            {
                MessageBox.Show(
                    "Please select a customer first.",
                    "Transaction History",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DataRow customer =
                clsDatabase.GetCustomerInfo(
                    selectedCustomerPersonID);

            if (customer == null)
            {
                MessageBox.Show(
                    "The selected customer could not be found.");

                return;
            }

            string customerName =
                customer["NameFirst"].ToString() +
                " " +
                customer["NameLast"].ToString();

            using (frmPOSTransactionHistory form =
                new frmPOSTransactionHistory(
                    selectedCustomerPersonID,
                    customerName))
            {
                form.ShowDialog();
            }
        }

        private void gbCart_Enter(object sender, EventArgs e)
        {

        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            FormatProductGrid();

            DataTable dtCategories =
                clsDatabase.GetCategories();

            DataRow row =
                dtCategories.NewRow();

            row["CategoryID"] = 0;
            row["CategoryName"] = "All Categories";

            dtCategories.Rows.InsertAt(row, 0);

            cboCategory.DataSource =
                dtCategories;

            cboCategory.DisplayMember =
                "CategoryName";

            cboCategory.ValueMember =
                "CategoryID";

            cboCategory.SelectedIndex = 0;

            if (dgvProducts.Rows.Count > 0)
            {
                dgvProducts.Rows[0].Selected = true;
            }

            RefreshCart();
        }
        private void FormatProductGrid()
        {
            dgvProducts.Columns["InventoryID"].Visible = false;
            dgvProducts.Columns["CategoryID"].Visible = false;
            dgvProducts.Columns["ItemImage"].Visible = false;
            dgvProducts.Columns["ItemDescription"].Visible = false;

            dgvProducts.Columns["ItemName"].HeaderText =
                "Game";

            dgvProducts.Columns["RetailPrice"].HeaderText =
                "Price";

            dgvProducts.Columns["Quantity"].HeaderText =
                "In Stock";

            dgvProducts.Columns["RetailPrice"]
                .DefaultCellStyle.Format = "C";

            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void RefreshProducts()
        {
            int categoryID = 0;

            if (cboCategory.SelectedValue != null &&
                !(cboCategory.SelectedValue is DataRowView))
            {
                categoryID =
                    Convert.ToInt32(
                        cboCategory.SelectedValue);
            }

            dgvProducts.DataSource =
                clsDatabase.SearchInventory(
                    txtProductSearch.Text.Trim(),
                    categoryID);

            FormatProductGrid();
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            RefreshProducts();
        }

        private void btnProductClear_Click(object sender, EventArgs e)
        {
            txtProductSearch.Clear();

            cboCategory.SelectedIndex = 0;

            RefreshProducts();

            txtProductSearch.Focus();
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshProducts();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
                return;

            lblProductName.Text =
     "Product Name: " +
     dgvProducts.CurrentRow
         .Cells["ItemName"].Value
         .ToString();

            lblProductPrice.Text =
                "Price: " +
                Convert.ToDecimal(
                    dgvProducts.CurrentRow
                        .Cells["RetailPrice"].Value)
                .ToString("C");

            lblProductDescription.Text =
                "Description: " +
                dgvProducts.CurrentRow
                    .Cells["ItemDescription"].Value
                    .ToString();

            object imageData =
                dgvProducts.CurrentRow
                    .Cells["ItemImage"].Value;

            if (imageData != DBNull.Value)
            {
                byte[] bytes =
                    (byte[])imageData;

                using (MemoryStream ms =
                    new MemoryStream(bytes))
                {
                    picProduct.Image =
                        Image.FromStream(ms);
                }
            }
            else
            {
                picProduct.Image = null;
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a product.");

                return;
            }

            int inventoryID =
                Convert.ToInt32(
                    dgvProducts.CurrentRow
                        .Cells["InventoryID"].Value);

            int stock =
                Convert.ToInt32(
                    dgvProducts.CurrentRow
                        .Cells["Quantity"].Value);

            int requested =
                (int)nudQuantity.Value;

            int alreadyInCart =
                cart.GetQuantityInCart(
                    inventoryID);

            if (requested + alreadyInCart > stock)
            {
                MessageBox.Show(
                    "Only " + stock +
                    " copies of this game are available.\n" +
                    "You already have " +
                    alreadyInCart +
                    " in the cart.");

                return;
            }

            clsCartItem item =
                new clsCartItem();

            item.InventoryID =
                inventoryID;

            item.ItemName =
                dgvProducts.CurrentRow
                    .Cells["ItemName"].Value
                    .ToString();

            item.Price =
                Convert.ToDecimal(
                    dgvProducts.CurrentRow
                        .Cells["RetailPrice"].Value);

            item.Quantity =
                requested;

            cart.AddItem(item);

            RefreshCart();
        }
        private void RefreshCart()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = cart.Items;

            if (dgvCart.Columns.Count > 0)
            {
                dgvCart.Columns["InventoryID"].Visible = false;

                dgvCart.Columns["ItemName"].HeaderText =
                    "Game";

                dgvCart.Columns["Price"].HeaderText =
                    "Price";

                dgvCart.Columns["Quantity"].HeaderText =
                    "Qty";

                dgvCart.Columns["Total"].HeaderText =
                    "Total";

                dgvCart.Columns["Price"]
                    .DefaultCellStyle.Format = "C";

                dgvCart.Columns["Total"]
                    .DefaultCellStyle.Format = "C";
            }

            lblSubtotal.Text =
     "Subtotal: " +
     cart.Subtotal.ToString("C");

            lblDiscount.Text =
                "Discount: " +
                cart.DiscountAmount.ToString("C");

            lblTax.Text =
                "Tax: " +
                cart.Tax.ToString("C");

            lblTotal.Text =
                "Total: " +
                cart.GrandTotal.ToString("C");
            dgvCart.AutoSizeColumnsMode =
    DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select an item to remove.");

                return;
            }

            int inventoryID =
                Convert.ToInt32(
                    dgvCart.CurrentRow
                        .Cells["InventoryID"].Value);

            cart.RemoveItem(inventoryID);

            RefreshCart();
        }

        private void btnApplyPromo_Click(object sender, EventArgs e)
        {
            string code =
    txtPromoCode.Text
        .Trim()
        .ToUpper();

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show(
                    "Please enter a promotion code.");

                txtPromoCode.Focus();
                return;
            }

            clsDiscount discount =
                clsDatabase.GetDiscount(code);

            if (discount == null)
            {
                MessageBox.Show(
                    "Invalid or inactive Promotion Code.");

                return;
            }

            if (discount.DiscountLevel == 1 &&
                discount.InventoryID.HasValue &&
                cart.GetQuantityInCart(
                    discount.InventoryID.Value) == 0)
            {
                MessageBox.Show(
                    "This promotion code does not apply to any item currently in the cart.");

                return;
            }

            cart.AppliedDiscount =
                discount;

            RefreshCart();

            btnApplyPromo.Enabled =
                false;

            MessageBox.Show(
                "Promotion Applied!");
        }

        private void btnAvailablePromo_Click(object sender, EventArgs e)
        {
            using (frmPOSAvailablePromos form =
       new frmPOSAvailablePromos())
            {
                form.ShowDialog();
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (selectedCustomerPersonID <= 0)
            {
                MessageBox.Show(
                    "Please select a customer before checkout.");

                return;
            }

            if (cart.Items.Count == 0)
            {
                MessageBox.Show(
                    "Please add at least one item to the cart.");

                return;
            }

            using (frmPOSCheckout checkout = new frmPOSCheckout())
            {
                if (checkout.ShowDialog() != DialogResult.OK)
                    return;

                int orderID =
                    clsDatabase.PlacePOSOrder(
                        selectedCustomerPersonID,
                        managerPersonID,
                        cart,
                        checkout.CardNumber,
                        checkout.Expiration,
                        checkout.CCV);

                if (orderID == -1)
                {
                    MessageBox.Show(
                        "The sale could not be completed.");

                    return;
                }

                clsHTML.CreatePOSReceipt(
                     cart,
                     orderID,
                     selectedCustomerPersonID,
                     managerPersonID);

                MessageBox.Show(
                    "Sale completed successfully.\n" +
                    "Order #" + orderID);

                ResetAfterCheckout();
            }
        }
        private void ResetAfterCheckout()
        {
            // Clear the cart
            cart.Items.Clear();

            // Remove applied promo
            cart.AppliedDiscount = null;

            // Clear promo code
            txtPromoCode.Clear();

            // Allow another promo to be applied
            btnApplyPromo.Enabled = true;

            // Reset quantity
            nudQuantity.Value = 1;

            // Refresh inventory quantities
            RefreshProducts();

            // Refresh cart and totals
            RefreshCart();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string helpPath =
                    Path.Combine(
                        Application.StartupPath,
                        "POSHelp.txt");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "The POS help file could not be found.",
                        "Help",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }

                Process.Start(
                    new ProcessStartInfo
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
