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
    public partial class frmManagerMain : Form
    {
        private int managerPersonID;

        public frmManagerMain(int managerPersonID)
        {
            InitializeComponent();

            this.managerPersonID =
                managerPersonID;
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            using (frmInventoryManagement inventoryForm =
       new frmInventoryManagement())
            {
                inventoryForm.ShowDialog();
            }
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            using (frmCustomerManagement form =
       new frmCustomerManagement())
            {
                form.ShowDialog();
            }
        }

        private void btnManagers_Click(object sender, EventArgs e)
        {
            using (frmManagerManagement form =
         new frmManagerManagement(
             managerPersonID))
            {
                form.ShowDialog();
            }
        }

        private void btnDiscounts_Click(object sender, EventArgs e)
        {
            using (frmDiscountManagement form =
       new frmDiscountManagement())
            {
                form.ShowDialog();
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            using (frmReports form =
        new frmReports())
            {
                form.ShowDialog();
            }
        }

        private void btnMyAccount_Click(object sender, EventArgs e)
        {
            using (frmManagerEditor form =
       new frmManagerEditor(managerPersonID))
            {
                form.ShowDialog();
            }
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            using (frmPOS form =
        new frmPOS(managerPersonID))
            {
                form.ShowDialog();
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string filePath =
       Path.Combine(
           Application.StartupPath,
           "ManagerMainHelp.txt");

            if (File.Exists(filePath))
            {
                Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
            }
            else
            {
                MessageBox.Show(
                    "Help file not found.");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Are you sure you want to log out?",
                    "Log Out",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                clsCurrentUser.Logout();

                Close();
            }
        }
    }
}
