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
    public partial class frmPOSCheckout : Form
    {
        public frmPOSCheckout()
        {
            InitializeComponent();
        }
        public string CardNumber
        {
            get { return txtCardNumber.Text.Trim(); }
        }

        public string Expiration
        {
            get { return txtExpiration.Text.Trim(); }
        }

        public string CCV
        {
            get { return txtCCV.Text.Trim(); }
        }

        private void frmPOSCheckout_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCompleteSale_Click(object sender, EventArgs e)
        {
            string cardNumber =
                txtCardNumber.Text.Trim();

            string expiration =
                txtExpiration.Text.Trim();

            string ccv =
                txtCCV.Text.Trim();

            if (!clsValidation.ValidCreditCard(
                cardNumber))
            {
                MessageBox.Show(
                    "Please enter a valid 16-digit card number.");

                txtCardNumber.Focus();
                return;
            }

            if (!clsValidation.ValidExpirationDate(
                expiration))
            {
                MessageBox.Show(
                    "Please enter a valid expiration date.\n" +
                    "Expiration cannot be expired or more than 5 years in the future.");

                txtExpiration.Focus();
                return;
            }

            if (!clsValidation.ValidCCV(
                ccv))
            {
                MessageBox.Show(
                    "Please enter a valid 3-digit CCV.");

                txtCCV.Focus();
                return;
            }

            DialogResult =
                DialogResult.OK;

            Close();
        }

        private void txtCardNumber_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = txtCardNumber.SelectionStart;

            string digits = new string(
                txtCardNumber.Text
                    .Where(char.IsDigit)
                    .Take(16)
                    .ToArray());

            string formatted = "";

            for (int i = 0; i < digits.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                {
                    formatted += "-";
                }

                formatted += digits[i];
            }

            if (txtCardNumber.Text != formatted)
            {
                txtCardNumber.Text = formatted;
                txtCardNumber.SelectionStart = txtCardNumber.Text.Length;
            }
        }

        private void txtExpiration_TextChanged(object sender, EventArgs e)
        {
            string digits = new string(
                txtExpiration.Text
                    .Where(char.IsDigit)
                    .Take(4)
                    .ToArray());

            // Validate the month once two digits are entered
            if (digits.Length >= 2)
            {
                int month =
                    Convert.ToInt32(
                        digits.Substring(0, 2));

                if (month < 1 || month > 12)
                {
                    MessageBox.Show(
                        "Expiration month must be between 01 and 12.");

                    txtExpiration.Clear();
                    return;
                }
            }

            string formatted = digits;

            if (digits.Length > 2)
            {
                formatted =
                    digits.Substring(0, 2) +
                    "/" +
                    digits.Substring(2);
            }

            if (txtExpiration.Text != formatted)
            {
                txtExpiration.Text = formatted;

                txtExpiration.SelectionStart =
                    txtExpiration.Text.Length;
            }
        }
    }
}
