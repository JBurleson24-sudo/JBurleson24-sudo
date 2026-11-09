using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BurlesonJLogonView
{
    public partial class frmCustomerManagement : Form
    {
        public frmCustomerManagement()
        {
            InitializeComponent();
        }
        private void LoadCustomers()
        {
            DataTable dt =
                clsDatabase.GetCustomers();

            DisplayCustomers(dt);
        }
        private void DisplayCustomers(DataTable dt)
        {
            dgvCustomers.DataSource = dt;

            dgvCustomers.Columns["PersonID"].HeaderText =
                "ID";

            dgvCustomers.Columns["NameFirst"].HeaderText =
                "First Name";

            dgvCustomers.Columns["NameLast"].HeaderText =
                "Last Name";

            dgvCustomers.Columns["Email"].HeaderText =
                "Email";

            dgvCustomers.Columns["PhonePrimary"].HeaderText =
                "Phone";

            dgvCustomers.Columns["LogonName"].HeaderText =
                "Username";

            dgvCustomers.Columns["AccountDisabled"].HeaderText =
                "Disabled";

            dgvCustomers.Columns["AccountDeleted"].HeaderText =
                "Deleted";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCustomerManagement_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText =
       txtSearch.Text.Trim();

            if (searchText == "")
            {
                LoadCustomers();
                return;
            }

            DataTable dt =
                clsDatabase.SearchCustomers(
                    searchText);

            DisplayCustomers(dt);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            LoadCustomers();

            txtSearch.Focus();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            using (CreateAccount frm = new CreateAccount())
            {
                frm.ShowDialog();
            }

            LoadCustomers();
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a customer to edit.",
                    "Edit Customer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            int personID =
                Convert.ToInt32(
                    dgvCustomers.CurrentRow.Cells["PersonID"].Value);

            using (frmCustomerEditor frm =
                new frmCustomerEditor(personID))
            {
                frm.ShowDialog();
            }

            LoadCustomers();
        }

        private void btnDisableCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a customer first.");

                return;
            }

            int personID =
                Convert.ToInt32(
                    dgvCustomers.CurrentRow.Cells["PersonID"].Value);

            bool currentlyDisabled =
                dgvCustomers.CurrentRow.Cells["AccountDisabled"].Value != DBNull.Value &&
                Convert.ToBoolean(
                    dgvCustomers.CurrentRow.Cells["AccountDisabled"].Value);

            bool newDisabledStatus =
                !currentlyDisabled;

            string action =
                newDisabledStatus ? "disable" : "enable";

            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to " +
                    action +
                    " this customer account?",
                    "Confirm Account Status",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool success =
                    clsDatabase.SetCustomerDisabled(
                        personID,
                        newDisabledStatus);

                if (success)
                {
                    MessageBox.Show(
                        "Customer account " +
                        (newDisabledStatus
                            ? "disabled."
                            : "enabled."),
                        "Customer Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadCustomers();
                }
                else
                {
                    MessageBox.Show(
                        "The customer account could not be updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The customer account could not be updated.\n\n" +
                    ex.Message,
                    "Error",
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
                    "CustomerManagementHelp.txt");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "The Customer Management help file could not be found.",
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
