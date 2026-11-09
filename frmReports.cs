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
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            DateTime selectedDate =
        dtpSalesDate.Value.Date;

            clsHTML.CreateSalesReport(
                selectedDate,
                selectedDate,
                "Daily Sales Report");
        }

        private void btnWeeklySales_Click(object sender, EventArgs e)
        {
            DateTime startDate =
        dtpSalesDate.Value.Date;

            DateTime endDate =
                startDate.AddDays(6);

            clsHTML.CreateSalesReport(
                startDate,
                endDate,
                "Weekly Sales Report");
        }

        private void btnMonthlySales_Click(object sender, EventArgs e)
        {
            DateTime selectedDate =
        dtpSalesDate.Value.Date;

            DateTime startDate =
                new DateTime(
                    selectedDate.Year,
                    selectedDate.Month,
                    1);

            DateTime endDate =
                startDate
                .AddMonths(1)
                .AddDays(-1);

            clsHTML.CreateSalesReport(
                startDate,
                endDate,
                "Monthly Sales Report");
        }

        private void btnAvailableInventory_Click(object sender, EventArgs e)
        {
            clsHTML.CreateInventoryReport(
        0,
        "Available Inventory Report");
        }

        private void btnRestockInventory_Click(object sender, EventArgs e)
        {
            clsHTML.CreateInventoryReport(
       1,
       "Items Needing Restock Report");
        }

        private void btnAllInventory_Click(object sender, EventArgs e)
        {
            clsHTML.CreateInventoryReport(
        2,
        "All Inventory Report");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string helpPath = Path.Combine(
                    Application.StartupPath,
                    "ReportsHelp.txt");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "The Reports help file could not be found.",
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
