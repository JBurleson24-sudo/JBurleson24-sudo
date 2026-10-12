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
    public partial class frmManagerManagement : Form
    {
        private int loggedInManagerID;
        public frmManagerManagement(
    int loggedInManagerID)
        {
            InitializeComponent();

            this.loggedInManagerID =
                loggedInManagerID;
        }
        private void LoadManagers()
        {
            DataTable dt =
      clsDatabase.GetManagers();

            DisplayManagers(dt);
        }

        private void frmManagerManagement_Load(object sender, EventArgs e)
        {
            LoadManagers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DisplayManagers(DataTable dt)
        {
            dgvManagers.DataSource = dt;

            dgvManagers.Columns["PersonID"].HeaderText =
                "ID";

            dgvManagers.Columns["NameFirst"].HeaderText =
                "First Name";

            dgvManagers.Columns["NameLast"].HeaderText =
                "Last Name";

            dgvManagers.Columns["Email"].HeaderText =
                "Email";

            dgvManagers.Columns["PhonePrimary"].HeaderText =
                "Phone";

            dgvManagers.Columns["LogonName"].HeaderText =
                "Username";

            dgvManagers.Columns["AccountDisabled"].HeaderText =
                "Disabled";

            dgvManagers.Columns["AccountDeleted"].HeaderText =
                "Deleted";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText =
        txtSearch.Text.Trim();

            if (searchText == "")
            {
                LoadManagers();
                return;
            }

            DataTable dt =
                clsDatabase.SearchManagers(searchText);

            DisplayManagers(dt);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            LoadManagers();

            txtSearch.Focus();
        }

        private void btnAddManager_Click(
           object sender,
           EventArgs e)
        {
            using (frmManagerEditor form =
                new frmManagerEditor())
            {
                if (form.ShowDialog() ==
                    DialogResult.OK)
                {
                    LoadManagers();
                }
            }
        }

        private void btnEditManger_Click(object sender, EventArgs e)
        {
            if (dgvManagers.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a manager to edit.");

                return;
            }

            int managerID =
                Convert.ToInt32(
                    dgvManagers.CurrentRow
                        .Cells["PersonID"].Value);

            using (frmManagerEditor form =
                new frmManagerEditor(managerID))
            {
                if (form.ShowDialog() ==
                    DialogResult.OK)
                {
                    LoadManagers();
                }
            }
        }

        private void btnDisableCustomer_Click(object sender, EventArgs e)
        {
            if (dgvManagers.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a manager first.");

                return;
            }

            int personID =
                Convert.ToInt32(
                    dgvManagers.CurrentRow
                        .Cells["PersonID"].Value);
            if (personID == loggedInManagerID)
            {
                MessageBox.Show(
                    "You cannot disable your own account while you are logged in.",
                    "Manager Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            bool currentlyDisabled =
                dgvManagers.CurrentRow
                    .Cells["AccountDisabled"].Value != DBNull.Value &&
                Convert.ToBoolean(
                    dgvManagers.CurrentRow
                        .Cells["AccountDisabled"].Value);

            bool newDisabledStatus =
                !currentlyDisabled;

            string action =
                newDisabledStatus
                    ? "disable"
                    : "enable";

            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to " +
                    action +
                    " this manager account?",
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
                    clsDatabase.SetManagerDisabled(
                        personID,
                        newDisabledStatus);

                if (success)
                {
                    MessageBox.Show(
                        "Manager account " +
                        (newDisabledStatus
                            ? "disabled."
                            : "enabled."),
                        "Manager Management",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadManagers();
                }
                else
                {
                    MessageBox.Show(
                        "The manager account could not be updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The manager account could not be updated.\n\n" +
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
                    "ManagerManagementHelp.txt");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "The Manager Management help file could not be found.",
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
