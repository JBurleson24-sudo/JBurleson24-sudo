using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;


namespace BurlesonJLogonView
{
    public partial class frmDiscountEditor : Form
    {
        public frmDiscountEditor()
        {
            InitializeComponent();
        }
        public frmDiscountEditor(int discountID)
        {
            InitializeComponent();

            this.discountID = discountID;
            editMode = true;
        }
        private int discountID = 0;
        private bool editMode = false;
        private void frmDiscountEditor_Load(
            object sender,
            EventArgs e)
        {
            cboDiscountLevel.Items.Clear();
            cboDiscountLevel.Items.Add("Entire Order");
            cboDiscountLevel.Items.Add("Individual Item");

            cboDiscountType.Items.Clear();
            cboDiscountType.Items.Add("Percentage");
            cboDiscountType.Items.Add("Dollar Amount");

            cboDiscountLevel.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboDiscountType.DropDownStyle =
                ComboBoxStyle.DropDownList;

            LoadProducts();

            cboDiscountLevel.SelectedIndex = 0;
            cboDiscountType.SelectedIndex = 0;

            //dtpStartDate.Value = DateTime.Today;
            //dtpExpirationDate.Value =
            //    DateTime.Today.AddMonths(1);
            if (editMode)
            {
                Text = "Edit Discount";
                lblTitle.Text = "Edit Discount";

                btnSave.Text = "Save Changes";

                LoadDiscount();
            }
            else
            {
                Text = "Add Discount";
                lblTitle.Text = "Add Discount";

                btnSave.Text = "Save";

                dtpStartDate.Value =
                    DateTime.Today;

                dtpExpirationDate.Value =
                    DateTime.Today.AddMonths(1);
            }
        }
        private void LoadProducts()
        {
            DataTable dt =
                clsDatabase.GetInventory();

            cboProduct.DataSource = dt;

            cboProduct.DisplayMember =
                "ItemName";

            cboProduct.ValueMember =
                "InventoryID";

            cboProduct.SelectedIndex = -1;
        }
        private void LoadDiscount()
        {
            DataRow row =
                clsDatabase.GetDiscountByID(
                    discountID);

            if (row == null)
            {
                MessageBox.Show(
                    "The selected discount could not be found.");

                Close();
                return;
            }

            txtDiscountCode.Text =
                row["DiscountCode"].ToString();

            txtDescription.Text =
                row["Description"].ToString();

            int discountLevel =
                Convert.ToInt32(
                    row["DiscountLevel"]);

            int discountType =
                Convert.ToInt32(
                    row["DiscountType"]);

            cboDiscountLevel.SelectedIndex =
                discountLevel;

            if (discountLevel == 1 &&
                row["InventoryID"] != DBNull.Value)
            {
                cboProduct.SelectedValue =
                    Convert.ToInt32(
                        row["InventoryID"]);
            }

            cboDiscountType.SelectedIndex =
                discountType;

            if (discountType == 0 &&
                row["DiscountPercentage"] != DBNull.Value)
            {
                decimal percentage =
                    Convert.ToDecimal(
                        row["DiscountPercentage"]);

                nudDiscountAmount.Value =
                    percentage * 100m;
            }
            else if (
                row["DiscountDollarAmount"] != DBNull.Value)
            {
                nudDiscountAmount.Value =
                    Convert.ToDecimal(
                        row["DiscountDollarAmount"]);
            }

            if (row["StartDate"] != DBNull.Value)
            {
                dtpStartDate.Value =
                    Convert.ToDateTime(
                        row["StartDate"]);
            }

            dtpExpirationDate.Value =
                Convert.ToDateTime(
                    row["ExpirationDate"]);
        }
        private void cboDiscountLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool individualItem =
       cboDiscountLevel.SelectedIndex == 1;

            cboProduct.Enabled =
                individualItem;

            if (!individualItem)
            {
                cboProduct.SelectedIndex = -1;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string code =
       txtDiscountCode.Text.Trim();

            string description =
                txtDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show(
                    "Please enter a discount code.");

                txtDiscountCode.Focus();
                return;
            }

            if (code.Length > 20)
            {
                MessageBox.Show(
                    "Discount code cannot exceed 20 characters.");

                txtDiscountCode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show(
                    "Please enter a description.");

                txtDescription.Focus();
                return;
            }

            if (description.Length > 50)
            {
                MessageBox.Show(
                    "Description cannot exceed 50 characters.");

                txtDescription.Focus();
                return;
            }

            if (cboDiscountLevel.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select where the discount applies.");

                cboDiscountLevel.Focus();
                return;
            }

            if (cboDiscountType.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select a discount type.");

                cboDiscountType.Focus();
                return;
            }

            if (cboDiscountLevel.SelectedIndex == 1 &&
                cboProduct.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select a product.");

                cboProduct.Focus();
                return;
            }

            if (nudDiscountAmount.Value <= 0)
            {
                MessageBox.Show(
                    "Discount amount must be greater than zero.");

                nudDiscountAmount.Focus();
                return;
            }

            if (cboDiscountType.SelectedIndex == 0 &&
                nudDiscountAmount.Value > 100)
            {
                MessageBox.Show(
                    "Percentage discount cannot exceed 100%.");

                nudDiscountAmount.Focus();
                return;
            }

            if (dtpExpirationDate.Value.Date <
                dtpStartDate.Value.Date)
            {
                MessageBox.Show(
                    "Expiration date cannot be before the start date.");

                dtpExpirationDate.Focus();
                return;
            }

            int discountLevel =
                cboDiscountLevel.SelectedIndex;

            int discountType =
                cboDiscountType.SelectedIndex;

            int? inventoryID = null;

            if (discountLevel == 1)
            {
                inventoryID =
                    Convert.ToInt32(
                        cboProduct.SelectedValue);
            }

            decimal? percentage = null;
            decimal? dollarAmount = null;

            if (discountType == 0)
            {
                percentage =
                    nudDiscountAmount.Value / 100m;
            }
            else
            {
                dollarAmount =
                    nudDiscountAmount.Value;
            }

            try
            {
                bool success;

                if (editMode)
                {
                    success =
                        clsDatabase.UpdateDiscount(
                            discountID,
                            code,
                            description,
                            discountLevel,
                            inventoryID,
                            discountType,
                            percentage,
                            dollarAmount,
                            dtpStartDate.Value,
                            dtpExpirationDate.Value);
                }
                else
                {
                    success =
                        clsDatabase.AddDiscount(
                            code,
                            description,
                            discountLevel,
                            inventoryID,
                            discountType,
                            percentage,
                            dollarAmount,
                            dtpStartDate.Value,
                            dtpExpirationDate.Value);
                }

                if (success)
                {
                    MessageBox.Show( editMode
         ? "Discount updated successfully."
         : "Discount added successfully.",
                         "Add Discount",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult =
                        DialogResult.OK;

                    Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "The discount could not be added.\n\n" +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string helpPath =
                    Path.Combine(
                        Application.StartupPath,
                        "DiscountEditorHelp.txt");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "The Discount Editor help file could not be found.",
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
