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
    public partial class frmCustomer : Form
    {
        
        public frmCustomer()
        {
            InitializeComponent();
            dgvInventory.DataSource = clsDatabase.GetInventory();
            
        }
        private clsCart cart = new clsCart();
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            FormatInventoryGrid();

            if (dgvInventory.Rows.Count > 0)
            {
                dgvInventory.Rows[0].Selected = true;
            }
            DataTable dtCategories = clsDatabase.GetCategories();

            DataRow row = dtCategories.NewRow();
            row["CategoryID"] = 0;
            row["CategoryName"] = "All Categories";

            dtCategories.Rows.InsertAt(row, 0);

            cboCategory.DataSource = dtCategories;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryID";
        }
        private void FormatInventoryGrid()
        {
            dgvInventory.Columns["InventoryID"].Visible = false;
            dgvInventory.Columns["CategoryID"].Visible = false;
            dgvInventory.Columns["ItemImage"].Visible = false;
            dgvInventory.Columns["ItemDescription"].Visible = false;

            dgvInventory.Columns["ItemName"].HeaderText = "Game";
            dgvInventory.Columns["RetailPrice"].HeaderText = "Price";
            dgvInventory.Columns["Quantity"].HeaderText = "In Stock";

            dgvInventory.Columns["RetailPrice"].DefaultCellStyle.Format = "C";
        }
        private void RefreshInventory()
        {
            int categoryID = 0;

            if (cboCategory.SelectedValue != null &&
                !(cboCategory.SelectedValue is DataRowView))
            {
                categoryID = Convert.ToInt32(cboCategory.SelectedValue);
            }

            dgvInventory.DataSource =
                clsDatabase.SearchInventory(
                    txtSearch.Text.Trim(),
                    categoryID);

            FormatInventoryGrid();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null)
                return;

            int inventoryID = Convert.ToInt32(
                dgvInventory.CurrentRow.Cells["InventoryID"].Value);

            int stock = Convert.ToInt32(
                dgvInventory.CurrentRow.Cells["Quantity"].Value);

            int requested = (int)nudQuantity.Value;

            int alreadyInCart = cart.GetQuantityInCart(inventoryID);

            if (requested + alreadyInCart > stock)
            {
                MessageBox.Show(
                    "Only " + stock + " copies of this game  are available.\n" +
                    "You already have " + alreadyInCart + " in your cart.");

                return;
            }

            clsCartItem item = new clsCartItem();

            item.InventoryID = inventoryID;

            item.ItemName =
                dgvInventory.CurrentRow.Cells["ItemName"].Value.ToString();

            item.Price = Convert.ToDecimal(
                dgvInventory.CurrentRow.Cells["RetailPrice"].Value);

            item.Quantity = requested;

            cart.AddItem(item);

            MessageBox.Show(item.ItemName + " added to cart!");
        }

        private void dgvInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewCart_Click(object sender, EventArgs e)
        {
            frmCart cartForm = new frmCart(cart);

            cartForm.ShowDialog();

            
            cart.AppliedDiscount = null;

            ReloadInventory();
        }

        private void dgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null)
                return;

            lblGameName.Text =
                dgvInventory.CurrentRow.Cells["ItemName"].Value.ToString();

            lblPrice.Text =
                Convert.ToDecimal(
                    dgvInventory.CurrentRow.Cells["RetailPrice"].Value).ToString("C");

            lblQuantity.Text =
                dgvInventory.CurrentRow.Cells["Quantity"].Value.ToString();

            txtDescription.Text =
                dgvInventory.CurrentRow.Cells["ItemDescription"].Value.ToString();
            object imageData = dgvInventory.CurrentRow.Cells["ItemImage"].Value;

            if (imageData != DBNull.Value)
            {
                byte[] bytes = (byte[])imageData;

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    picGame.Image = Image.FromStream(ms);
                }
            }
            else
            {
                picGame.Image = null;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshInventory();
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboCategory.SelectedValue == null)
            //    return;

            //if (cboCategory.SelectedValue is DataRowView)
            //    return;

            //int categoryID = Convert.ToInt32(cboCategory.SelectedValue);

            //dgvInventory.DataSource = clsDatabase.GetInventoryByCategory(categoryID);

            //FormatInventoryGrid();
        }

        private void cboCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshInventory();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.ShowDialog();

            if (clsCurrentUser.IsLoggedIn)
            {
                lblUser.Text = "Welcome, " + clsCurrentUser.UserName;
                btnLogin.Enabled = false;
            }
        }
        public void ReloadInventory()
        {
            RefreshInventory();
        }
        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "Customer.txt");

            if (File.Exists(filePath))
            {
                Process.Start("notepad.exe", filePath);
            }
            else
            {
                MessageBox.Show("Help file not found.");
            }
        }
    }
}
