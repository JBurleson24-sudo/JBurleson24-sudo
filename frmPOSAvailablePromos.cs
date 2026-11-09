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
    public partial class frmPOSAvailablePromos : Form
    {
        public frmPOSAvailablePromos()
        {
            InitializeComponent();
        }

        private void frmPOSAvailablePromos_Load(object sender, EventArgs e)
        {
            DataTable dt =
       clsDatabase.GetAvailableDiscounts();

            dgvPromos.DataSource = dt;

            dgvPromos.Columns["DiscountCode"].HeaderText =
                "Code";

            dgvPromos.Columns["Description"].HeaderText =
                "Description";

            dgvPromos.Columns["AppliesTo"].HeaderText =
                "Applies To";

            dgvPromos.Columns["DiscountTypeName"].HeaderText =
                "Type";

            dgvPromos.Columns["Amount"].HeaderText =
                "Amount";

            dgvPromos.Columns["ExpirationDate"].HeaderText =
                "Expires";

            dgvPromos.Columns["ExpirationDate"]
                .DefaultCellStyle.Format = "d";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
