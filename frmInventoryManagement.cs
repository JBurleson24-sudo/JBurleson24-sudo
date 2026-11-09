using System;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace BurlesonJLogonView
{
    public partial class frmInventoryManagement : Form
    {
        public frmInventoryManagement()
        {
            InitializeComponent();
        }

        private void LoadInventory()
        {
            DataTable dt = clsDatabase.GetManagerInventory();

            DisplayInventory(dt);
        }

        private void DisplayInventory(DataTable dt)
        {
            dgvInventory.DataSource = dt;

            dgvInventory.Columns["InventoryID"].HeaderText = "ID";
            dgvInventory.Columns["ItemName"].HeaderText = "Product";
            dgvInventory.Columns["Cost"].HeaderText = "Cost";
            dgvInventory.Columns["RetailPrice"].HeaderText = "Price";
            dgvInventory.Columns["Quantity"].HeaderText = "Qty";
            dgvInventory.Columns["RestockThreshold"].HeaderText =
                "Restock Threshold";
            dgvInventory.Columns["Discontinued"].HeaderText =
                "Discontinued";

            int restockCount = 0;

            foreach (DataRow row in dt.Rows)
            {
                int quantity =
                    Convert.ToInt32(row["Quantity"]);

                int threshold =
                    Convert.ToInt32(row["RestockThreshold"]);

                bool discontinued =
                    row["Discontinued"] != DBNull.Value &&
                    Convert.ToBoolean(row["Discontinued"]);

                if (!discontinued && quantity <= threshold)
                {
                    restockCount++;
                }
            }

            lblRestockWarning.Text =
                "Products needing restock: " + restockCount;
        }

        private void LoadCategories()
        {
            DataTable dt = clsDatabase.GetCategories();

            DataRow allRow = dt.NewRow();
            allRow["CategoryID"] = 0;
            allRow["CategoryName"] = "All Categories";

            dt.Rows.InsertAt(allRow, 0);

            cboCategory.DataSource = dt;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryID";

            cboCategory.SelectedIndex = 0;
        }

        private void frmInventoryManagement_Load(
            object sender,
            EventArgs e)
        {
            LoadCategories();
            LoadInventory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int categoryID = 0;

            if (cboCategory.SelectedValue != null)
            {
                categoryID =
                    Convert.ToInt32(cboCategory.SelectedValue);
            }

            DataTable dt =
                clsDatabase.SearchManagerInventory(
                    txtSearch.Text.Trim(),
                    categoryID);

            DisplayInventory(dt);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboCategory.SelectedIndex = 0;

            LoadInventory();

            txtSearch.Focus();
        }

        private void btnAdjustQuantity_Click(object sender,EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Select a product first.");

                return;
            }

            DataGridViewRow row =
                dgvInventory.SelectedRows[0];

            int inventoryID =
                Convert.ToInt32(
                    row.Cells["InventoryID"].Value);

            string productName =
                row.Cells["ItemName"].Value.ToString();

            int currentQuantity =
                Convert.ToInt32(
                    row.Cells["Quantity"].Value);

            using (frmAdjustQuantity form =
                new frmAdjustQuantity(
                    inventoryID,
                    productName,
                    currentQuantity))
            {
                if (form.ShowDialog() ==
                    DialogResult.OK)
                {
                    LoadInventory();
                }
            }
        }

        private void btnAddProduct_Click(
     object sender,
     EventArgs e)
        {
            using (frmProductEditor form =
                new frmProductEditor())
            {
                if (form.ShowDialog() ==
                    DialogResult.OK)
                {
                    LoadInventory();
                }
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Select a product first.");

                return;
            }

            int inventoryID =
                Convert.ToInt32(
                    dgvInventory.SelectedRows[0]
                        .Cells["InventoryID"].Value);

            using (frmProductEditor form =
                new frmProductEditor(inventoryID))
            {
                if (form.ShowDialog() ==
                    DialogResult.OK)
                {
                    LoadInventory();
                }
            }
        }

        private void btnDiscontinue_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Select a product first.");

                return;
            }

            DataGridViewRow row =
                dgvInventory.SelectedRows[0];

            int inventoryID =
                Convert.ToInt32(
                    row.Cells["InventoryID"].Value);

            string itemName =
                row.Cells["ItemName"].Value.ToString();

            bool discontinued =
                row.Cells["Discontinued"].Value != DBNull.Value &&
                Convert.ToBoolean(
                    row.Cells["Discontinued"].Value);

            string action =
                discontinued
                    ? "restore"
                    : "discontinue";

            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to " +
                    action + " " + itemName + "?",
                    discontinued
                        ? "Restore Product"
                        : "Discontinue Product",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool success =
                clsDatabase.SetInventoryDiscontinued(
                    inventoryID,
                    !discontinued);

            if (!success)
            {
                MessageBox.Show(
                    "The product could not be updated.");

                return;
            }

            MessageBox.Show(
                discontinued
                    ? "Product restored successfully."
                    : "Product discontinued successfully.");

            LoadInventory();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string filePath =
                Path.Combine(
                    Application.StartupPath,
                    "InventoryManagementHelp.txt");

            if (File.Exists(filePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show(
                    "Inventory Management help file not found.");
            }
        }
    }
}
