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
    public partial class frmCustomerEditor : Form
    {
        private int personID;
        public frmCustomerEditor(int personID)
        {
            InitializeComponent();

            this.personID = personID;
        }
        private void LoadCustomer()
        {
            DataRow row =
                clsDatabase.GetCustomerByID(
                    personID);

            if (row == null)
            {
                MessageBox.Show(
                    "The selected customer could not be found.");

                Close();
                return;
            }

            cboTitle.Text =
                row["Title"].ToString();

            txtFirstName.Text =
                row["NameFirst"].ToString();

            txtLastName.Text =
                row["NameLast"].ToString();

            txtSuffix.Text =
                row["Suffix"].ToString();

            txtAddress1.Text =
                row["Address1"].ToString();

            txtAddress2.Text =
                row["Address2"].ToString();

            txtAddress3.Text =
                row["Address3"].ToString();

            txtCity.Text =
                row["City"].ToString();

            txtZip.Text =
                row["Zipcode"].ToString();

            cboState.Text =
                row["State"].ToString();

            txtEmail.Text =
                row["Email"].ToString();

            txtPhone.Text =
                row["PhonePrimary"].ToString();

            txtPhoneSecondary.Text =
                row["PhoneSecondary"].ToString();

            txtUsername.Text =
                row["LogonName"].ToString();
        }

        private void frmCustomerEditor_Load(object sender, EventArgs e)
        {
            cboTitle.Items.Clear();

            cboTitle.Items.AddRange(
                new string[]
                {
            "Mr.",
            "Mrs.",
            "Ms.",
            "Miss",
            "Dr."
                });

            cboState.Items.Clear();

            cboState.Items.AddRange(
                new string[]
                {
            "AA","AE","AP",
            "AL","AK","AZ","AR","CA",
            "CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA",
            "KS","KY","LA","ME","MD",
            "MA","MI","MN","MS","MO",
            "MT","NE","NV","NH","NJ",
            "NM","NY","NC","ND","OH",
            "OK","OR","PA","RI","SC",
            "SD","TN","TX","UT","VT",
            "VA","WA","WV","WI","WY"
                });

            cboTitle.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboState.DropDownStyle =
                ComboBoxStyle.DropDownList;

            LoadCustomer();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!clsValidation.ValidName(
                txtFirstName.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid first name.");

                txtFirstName.Focus();
                return;
            }

            if (!clsValidation.ValidName(
                txtLastName.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid last name.");

                txtLastName.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !clsValidation.ValidEmail(
                    txtEmail.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid email address.");

                txtEmail.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtPhone.Text) &&
                !clsValidation.ValidPhone(
                    txtPhone.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid primary phone number.");

                txtPhone.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(
                    txtPhoneSecondary.Text) &&
                !clsValidation.ValidPhone(
                    txtPhoneSecondary.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid secondary phone number.");

                txtPhoneSecondary.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtAddress1.Text))
            {
                MessageBox.Show(
                    "Please enter Address 1.");

                txtAddress1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtCity.Text))
            {
                MessageBox.Show(
                    "Please enter a city.");

                txtCity.Focus();
                return;
            }

            if (!clsValidation.ValidState(
                cboState.Text))
            {
                MessageBox.Show(
                    "Please select a valid state.");

                cboState.Focus();
                return;
            }

            if (!clsValidation.ValidZip(
                txtZip.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid ZIP code.");

                txtZip.Focus();
                return;
            }

            if (!clsValidation.ValidUsername(
                txtUsername.Text.Trim()))
            {
                MessageBox.Show(
                    "Username must be 8-20 characters and contain at least one letter and one number.");

                txtUsername.Focus();
                return;
            }

            try
            {
                bool success =
                    clsDatabase.UpdateCustomer(
                        personID,
                        cboTitle.Text,
                        txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(),
                        txtSuffix.Text.Trim(),
                        txtAddress1.Text.Trim(),
                        txtAddress2.Text.Trim(),
                        txtAddress3.Text.Trim(),
                        txtCity.Text.Trim(),
                        txtZip.Text.Trim(),
                        cboState.Text,
                        txtEmail.Text.Trim(),
                        txtPhone.Text.Trim(),
                        txtPhoneSecondary.Text.Trim(),
                        txtUsername.Text.Trim());

                if (success)
                {
                    MessageBox.Show(
                        "Customer updated successfully.",
                        "Edit Customer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 ||
                    ex.Number == 2601)
                {
                    MessageBox.Show(
                        "That username already exists.\n\nPlease choose another username.",
                        "Duplicate Username",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "The customer could not be updated.\n\n" +
                        ex.Message,
                        "Database Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The customer could not be updated.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string helpPath = Path.Combine(
                    Application.StartupPath,
                    "EditCustomerHelp.txt");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "The Edit Customer help file could not be found.",
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
