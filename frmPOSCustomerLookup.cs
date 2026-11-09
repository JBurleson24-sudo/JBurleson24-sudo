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
    public partial class frmPOSCustomerLookup : Form
    {
        public int SelectedPersonID { get; private set; }
        public frmPOSCustomerLookup()
        {
            InitializeComponent();
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
        }

        private void frmPOSCustomerLookup_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText =
       txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show(
                    "Please enter customer information to search for.");

                txtSearch.Focus();
                return;
            }

            try
            {
                DataTable dt =
                    clsDatabase.SearchPOSCustomers(
                        searchText);

                DisplayCustomers(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No matching customers were found.",
                        "Customer Lookup",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The customer search could not be completed.\n\n" +
                    ex.Message,
                    "Search Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            dgvCustomers.DataSource = null;

            txtSearch.Focus();
        }

        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a customer.");

                return;
            }

            SelectedPersonID =
                Convert.ToInt32(
                    dgvCustomers.CurrentRow
                        .Cells["PersonID"].Value);

            DialogResult = DialogResult.OK;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }
    }
}
