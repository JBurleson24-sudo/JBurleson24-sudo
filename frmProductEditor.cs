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
    public partial class frmProductEditor : Form
    {
        public frmProductEditor()
        {
            InitializeComponent();
        }
        private int inventoryID = 0;
        private bool editMode = false;

        public frmProductEditor(int inventoryID)
        {
            InitializeComponent();

            this.inventoryID = inventoryID;
            editMode = true;
        }
        private void LoadCategories()
        {
            DataTable dt = clsDatabase.GetCategories();

            cboCategory.DataSource = dt;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryID";

            cboCategory.SelectedIndex = -1;
        }
        private void LoadProduct()
        {
            DataRow row =
                clsDatabase.GetInventoryItemByID(
                    inventoryID);

            if (row == null)
            {
                MessageBox.Show(
                    "The selected product could not be found.");

                Close();
                return;
            }

            txtItemName.Text =
                row["ItemName"].ToString();

            txtDescription.Text =
                row["ItemDescription"].ToString();

            cboCategory.SelectedValue =
                Convert.ToInt32(row["CategoryID"]);

            txtRetailPrice.Text =
                Convert.ToDecimal(
                    row["RetailPrice"]).ToString("0.00");

            txtCost.Text =
                Convert.ToDecimal(
                    row["Cost"]).ToString("0.00");

            nudQuantity.Value =
                Convert.ToInt32(row["Quantity"]);

            nudRestockThreshold.Value =
                Convert.ToInt32(
                    row["RestockThreshold"]);

            if (row["ItemImage"] != DBNull.Value)
            {
                byte[] imageBytes =
                    (byte[])row["ItemImage"];

                using (System.IO.MemoryStream ms =
                    new System.IO.MemoryStream(
                        imageBytes))
                {
                    using (Image tempImage =
                        Image.FromStream(ms))
                    {
                        picItemImage.Image =
                            new Bitmap(tempImage);
                    }
                }
            }
        }

        private void frmProductEditor_Load(
     object sender,
     EventArgs e)
        {
            LoadCategories();

            if (editMode)
            {
                Text = "Edit Product";
                lblTitle.Text = "Edit Product";

                LoadProduct();
            }
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select Product Image";
                dialog.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (Image tempImage =
                        Image.FromFile(dialog.FileName))
                    {
                        picItemImage.Image =
                            new Bitmap(tempImage);
                    }
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            if (picItemImage.Image != null)
            {
                picItemImage.Image.Dispose();
                picItemImage.Image = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private byte[] GetImageBytes()
        {
            if (picItemImage.Image == null)
            {
                return null;
            }

            using (System.IO.MemoryStream ms =
                new System.IO.MemoryStream())
            {
                picItemImage.Image.Save(
                    ms,
                    System.Drawing.Imaging.ImageFormat.Png);

                return ms.ToArray();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                MessageBox.Show("Enter a product name.");
                txtItemName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Enter a product description.");
                txtDescription.Focus();
                return;
            }

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show("Select a category.");
                cboCategory.Focus();
                return;
            }

            decimal cost;
            decimal retailPrice;

            if (!decimal.TryParse(txtCost.Text, out cost) ||
                cost < 0)
            {
                MessageBox.Show("Enter a valid cost.");
                txtCost.Focus();
                return;
            }

            if (!decimal.TryParse(
                txtRetailPrice.Text,
                out retailPrice) ||
                retailPrice < 0)
            {
                MessageBox.Show("Enter a valid retail price.");
                txtRetailPrice.Focus();
                return;
            }

            int categoryID =
                Convert.ToInt32(cboCategory.SelectedValue);

            int quantity =
                (int)nudQuantity.Value;

            int restockThreshold =
                (int)nudRestockThreshold.Value;

            byte[] itemImage = GetImageBytes();

            bool success;

            if (editMode)
            {
                success =
                    clsDatabase.UpdateInventoryItem(
                        inventoryID,
                        txtItemName.Text.Trim(),
                        txtDescription.Text.Trim(),
                        categoryID,
                        retailPrice,
                        cost,
                        quantity,
                        restockThreshold,
                        itemImage);
            }
            else
            {
                success =
                    clsDatabase.AddInventoryItem(
                        txtItemName.Text.Trim(),
                        txtDescription.Text.Trim(),
                        categoryID,
                        retailPrice,
                        cost,
                        quantity,
                        restockThreshold,
                        itemImage);
            }
            if (!success)
            {
                MessageBox.Show(
                    editMode
                        ? "The product could not be updated."
                        : "The product could not be added.");

                return;
            }

            MessageBox.Show(
                editMode
                    ? "Product updated successfully."
                    : "Product added successfully.");

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
