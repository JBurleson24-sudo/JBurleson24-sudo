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
            //Stopwatch timer = Stopwatch.StartNew();

            //DataTable inventory = clsDatabase.GetInventory();

            //timer.Stop();
            //Trace.WriteLine(
            //    $"Customer inventory query: {timer.Elapsed.TotalMilliseconds:F2} ms");

            //dgvInventory.DataSource = inventory;

        }
        private clsCart cart = new clsCart();
        private async void frmCustomer_Load(object sender, EventArgs e)
        {
            Control[] loadingControls =
            {
        dgvInventory,
        cboCategory,
        txtSearch,
        btnSearch,
        btnAddToCart,
        btnViewCart,
        btnLogin
    };

            foreach (Control control in loadingControls)
            {
                control.Enabled = false;
            }

            UseWaitCursor = true;

            try
            {
                Trace.WriteLine(
                    $"UI thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}");

                Stopwatch timer = Stopwatch.StartNew();

                var data = await Task.Run(() =>
                {
                    Trace.WriteLine(
                        $"Database worker thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}");

                    DataTable inventory = clsDatabase.GetInventory();
                    DataTable categories = clsDatabase.GetCategories();

                    return new
                    {
                        Inventory = inventory,
                        Categories = categories
                    };
                });

                timer.Stop();

                if (IsDisposed || Disposing)
                    return;

                Trace.WriteLine(
                    $"Background inventory and categories: {timer.Elapsed.TotalMilliseconds:F2} ms");

                DataRow row = data.Categories.NewRow();
                row["CategoryID"] = 0;
                row["CategoryName"] = "All Categories";
                data.Categories.Rows.InsertAt(row, 0);

                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryID";
                cboCategory.DataSource = data.Categories;

                dgvInventory.DataSource = data.Inventory;
                FormatInventoryGrid();

                if (dgvInventory.Rows.Count > 0)
                {
                    dgvInventory.Rows[0].Selected = true;
                }

                foreach (Control control in loadingControls)
                {
                    control.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (IsDisposed || Disposing)
                    return;

                MessageBox.Show(
                    this,
                    "Inventory could not be loaded.\n\n" + ex.Message,
                    "Loading error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
            }
            finally
            {
                if (!IsDisposed && !Disposing)
                {
                    UseWaitCursor = false;
                }
            }
        }
        private void FormatInventoryGrid()
        {
            dgvInventory.Columns["InventoryID"].Visible = false;
            dgvInventory.Columns["CategoryID"].Visible = false;
            
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
            Stopwatch imageTimer = Stopwatch.StartNew();
            int inventoryID = Convert.ToInt32(
                dgvInventory.CurrentRow.Cells["InventoryID"].Value);

            byte[] imageData = clsDatabase.GetInventoryImage(inventoryID);

            Image newImage = null;

            if (imageData is byte[] bytes && bytes.Length > 0)
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                using (Image source = Image.FromStream(stream))
                {
                    double scale = Math.Min(
    1.0,
    Math.Min(
        (double)picGame.ClientSize.Width / source.Width,
        (double)picGame.ClientSize.Height / source.Height));

                    int width = Math.Max(1, (int)Math.Round(source.Width * scale));
                    int height = Math.Max(1, (int)Math.Round(source.Height * scale));

                    newImage = new Bitmap(width, height);

                    using (Graphics graphics = Graphics.FromImage(newImage))
                    {
                        graphics.InterpolationMode =
                            System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        graphics.DrawImage(source, 0, 0, width, height);
                    }

                    Trace.WriteLine(
                        $"Display bitmap dimensions: {width} x {height}");
                    Trace.WriteLine(
    $"Product image dimensions: {source.Width} x {source.Height}");
                }
            }

            Image previousImage = picGame.Image;
            picGame.Image = newImage;
            previousImage?.Dispose();
            imageTimer.Stop();
            Trace.WriteLine(
                $"Image retrieval and display setup: {imageTimer.Elapsed.TotalMilliseconds:F2} ms");
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
