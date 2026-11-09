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
    public partial class frmPOSTransactionHistory : Form
    {
        private int customerPersonID;
        private string customerName;

        public frmPOSTransactionHistory(
            int customerPersonID,
            string customerName)
        {
            InitializeComponent();

            this.customerPersonID =
                customerPersonID;

            this.customerName =
                customerName;
        }
        private void LoadTransactions()
        {
            DataTable dt =
                clsDatabase.GetCustomerTransactions(
                    customerPersonID);

            dgvTransactions.DataSource = dt;

            dgvTransactions.Columns["OrderID"].HeaderText =
                "Order ID";

            dgvTransactions.Columns["OrderDate"].HeaderText =
                "Order Date";

            dgvTransactions.Columns["ItemName"].HeaderText =
                "Item";

            dgvTransactions.Columns["Quantity"].HeaderText =
                "Qty";

            dgvTransactions.Columns["RetailPrice"].HeaderText =
                "Price";

            dgvTransactions.Columns["DiscountID"].HeaderText =
                "Discount ID";

            dgvTransactions.Columns["RetailPrice"]
                .DefaultCellStyle.Format = "C";
        }

        private void frmPOSTransactionHistory_Load(object sender, EventArgs e)
        {
            lblCustomer.Text =
        "Customer: " +
        customerName;

            LoadTransactions();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
