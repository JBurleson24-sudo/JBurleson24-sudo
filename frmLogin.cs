using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BurlesonJLogonView
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            txUser.Clear();
            txPassword.Clear();

            txPassword.PasswordChar = '*';

            txUser.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //clsPasswordRules.AllowedKeys(e, tssLabel);
        }

        private void Status1_Click(object sender, EventArgs e)
        {

        }

        private void txPassword_KeyUp(object sender, KeyEventArgs e)
        {
            //if (clsPasswordRules.HasComplexity(txPassword.Text))
            //{
            //    clsUpdateControls.UpdateStatusBar(tssLabel);
            //}
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Hide password characters
            txPassword.PasswordChar = '*';

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username =
                txUser.Text.Trim();

            string password =
                txPassword.Text;

            string errorMessage;

            if (!clsLogon.ValidateLogin(
                username,
                password,
                out errorMessage))
            {
                MessageBox.Show(
                    errorMessage,
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                string position =
                    clsDatabase.UserLogin(
                        username,
                        password);

                if (position == "")
                {
                    MessageBox.Show(
                        "Invalid Username or Password");

                    return;
                }

                switch (position)
                {
                    case "Customer":
                        MessageBox.Show(
                            "Customer Login Successful");

                        DialogResult =
                            DialogResult.OK;

                        Close();
                        break;

                    case "Employee":
                        MessageBox.Show(
                            "Employee Login Successful");
                        break;

                    case "Manager":
                        MessageBox.Show(
                            "Manager Login Successful");

                        int managerPersonID =
                            clsDatabase.GetPersonIDByUsername(
                                username);

                        Hide();

                        using (frmManagerMain managerForm =
                            new frmManagerMain(managerPersonID))
                        {
                            managerForm.ShowDialog();
                        }

                        Show();

                        txPassword.Clear();
                        txUser.Focus();
                        break;

                    default:
                        MessageBox.Show(
                            "Unknown Access Level");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateAccount frm = new CreateAccount();
            frm.ShowDialog();
        }

        private void btnForget_Click(object sender, EventArgs e)
        {
            frmForgotPassword frm = new frmForgotPassword();
            frm.ShowDialog();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "LoginHelp.txt");

            if (File.Exists(filePath))
            {
                Process.Start("notepad.exe", filePath);
            }
            else
            {
                MessageBox.Show("Help file not found.");
            }
        }
    }
}
