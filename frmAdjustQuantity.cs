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
    public partial class frmAdjustQuantity : Form
    {
        private int inventoryID;

        public frmAdjustQuantity(
            int inventoryID,
            string productName,
            int currentQuantity)
        {
            InitializeComponent();

            this.inventoryID = inventoryID;

            lblProduct.Text = productName;

            lblCurrentQuantity.Text =
                "Current Quantity: " + currentQuantity;
        }

        private void frmAdjustQuantity_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int adjustment =
        (int)nudAdjustment.Value;

            if (adjustment == 0)
            {
                MessageBox.Show(
                    "Enter a quantity adjustment.");

                return;
            }

            bool success =
                clsDatabase.AdjustInventoryQuantity(
                    inventoryID,
                    adjustment);

            if (!success)
            {
                MessageBox.Show(
                    "The quantity could not be updated. " +
                    "Inventory cannot go below zero.");

                return;
            }

            MessageBox.Show(
                "Inventory quantity updated successfully.");

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
