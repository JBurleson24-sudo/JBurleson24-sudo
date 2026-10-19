using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BurlesonJLogonView
{
    public partial class frmForgotPassword : Form
    {
        private bool userValidated = false;

        public frmForgotPassword()
        {
            InitializeComponent();
            this.Text = "Forgot Password";
        }

        private void btnFindUser_Click(object sender, EventArgs e)
        {
            try
            {
                int[] questions = clsDatabase.GetChallengeQuestions(
                    txUsername.Text.Trim());

                if (questions == null)
                {
                    userValidated = false;
                    MessageBox.Show("User Not Found");
                    return;
                }

                lblQuestion1.Text = clsDatabase.GetQuestionText(questions[0]);
                lblQuestion2.Text = clsDatabase.GetQuestionText(questions[1]);
                lblQuestion3.Text = clsDatabase.GetQuestionText(questions[2]);

                userValidated = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (!userValidated)
            {
                MessageBox.Show(
                    "Please click Find User before resetting the password.");

                return;
            }
            try
            {
                bool success = clsDatabase.ResetPassword(
                    txUsername.Text.Trim(),
                    txAnswer1.Text.Trim(),
                    txAnswer2.Text.Trim(),
                    txAnswer3.Text.Trim(),
                    txNewPass.Text);

                if (!success)
                {
                    MessageBox.Show("Security Answers Incorrect");
                    return;
                }

                MessageBox.Show("Password Reset Successful");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "ForgotPasswordHelp.txt");

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
