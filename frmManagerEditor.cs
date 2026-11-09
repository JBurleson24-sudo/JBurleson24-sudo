using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;


namespace BurlesonJLogonView
{

    public partial class frmManagerEditor : Form
    {
        public frmManagerEditor()
        {
            InitializeComponent();

            txUser.TextChanged += txUser_TextChanged;
            txPassword.TextChanged += txPassword_TextChanged;

            txFirst.TextChanged += txFirst_TextChanged;
            txLast.TextChanged += txLast_TextChanged;

            txEmail.TextChanged += txEmail_TextChanged;
            txPhone.TextChanged += txPhone_TextChanged;
            txPhoneSecondary.TextChanged += txPhoneSecondary_TextChanged;
            txZip.TextChanged += txZip_TextChanged;

            txAnswer1.TextChanged += txAnswer1_TextChanged;
            txAnswer2.TextChanged += txAnswer2_TextChanged;
            txAnswer3.TextChanged += txAnswer3_TextChanged;

            cboState.SelectedIndexChanged += cboState_SelectedIndexChanged;
            cboQuestion1.SelectedIndexChanged += cboQuestion1_SelectedIndexChanged;
            cboQuestion2.SelectedIndexChanged += cboQuestion2_SelectedIndexChanged;
            cboQuestion3.SelectedIndexChanged += cboQuestion3_SelectedIndexChanged;
        }
        private int managerID = 0;
        private bool editMode = false;
        public frmManagerEditor(int managerID)
        {
            InitializeComponent();

            this.managerID = managerID;
            editMode = true;

            txUser.TextChanged += txUser_TextChanged;
            txPassword.TextChanged += txPassword_TextChanged;

            txFirst.TextChanged += txFirst_TextChanged;
            txLast.TextChanged += txLast_TextChanged;

            txEmail.TextChanged += txEmail_TextChanged;
            txPhone.TextChanged += txPhone_TextChanged;
            txPhoneSecondary.TextChanged += txPhoneSecondary_TextChanged;
            txZip.TextChanged += txZip_TextChanged;

            txAnswer1.TextChanged += txAnswer1_TextChanged;
            txAnswer2.TextChanged += txAnswer2_TextChanged;
            txAnswer3.TextChanged += txAnswer3_TextChanged;

            cboState.SelectedIndexChanged += cboState_SelectedIndexChanged;
            cboQuestion1.SelectedIndexChanged += cboQuestion1_SelectedIndexChanged;
            cboQuestion2.SelectedIndexChanged += cboQuestion2_SelectedIndexChanged;
            cboQuestion3.SelectedIndexChanged += cboQuestion3_SelectedIndexChanged;
        }
        private void LoadManager()
        {
            DataRow row =
                clsDatabase.GetManagerByID(
                    managerID);

            if (row == null)
            {
                MessageBox.Show(
                    "The selected manager could not be found.");

                Close();
                return;
            }

            cboTitle.Text =
                row["Title"].ToString();

            txFirst.Text =
                row["NameFirst"].ToString();

            txLast.Text =
                row["NameLast"].ToString();

            txSuffix.Text =
                row["Suffix"].ToString();

            txAddress1.Text =
                row["Address1"].ToString();

            txAddress2.Text =
                row["Address2"].ToString();

            txAddress3.Text =
                row["Address3"].ToString();

            txCity.Text =
                row["City"].ToString();

            txZip.Text =
                row["Zipcode"].ToString();

            cboState.Text =
                row["State"].ToString();

            txEmail.Text =
                row["Email"].ToString();

            txPhone.Text =
                row["PhonePrimary"].ToString();

            txPhoneSecondary.Text =
                row["PhoneSecondary"].ToString();

            txUser.Text =
                row["LogonName"].ToString();
        }

        private void txUser_TextChanged(object sender, EventArgs e)
        {
            if (clsValidation.ValidUsername(txUser.Text.Trim()))
            {
                lblUsernameStatus.Text = "✔ Username meets requirements";
                lblUsernameStatus.ForeColor = Color.Green;
            }
            else
            {
                lblUsernameStatus.Text = "✘ Username must be 8-20 characters with at least one letter and one number.";
                lblUsernameStatus.ForeColor = Color.Red;
            }
        }
        private void txPassword_TextChanged(object sender, EventArgs e)
        {
            if (clsPasswordRules.HasComplexity(txPassword.Text))
            {
                lblPasswordStatus.Text = "✔ Password meets requirements";
                lblPasswordStatus.ForeColor = Color.Green;
            }
            else
            {
                lblPasswordStatus.Text = "✘ Password does not meet requirements";
                lblPasswordStatus.ForeColor = Color.Red;
            }
        }
        //private void CboTitle_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboTitle.SelectedIndex >= 0)
        //    {
        //        lblTitleStatus.Text = "✔ Title selected";
        //        lblTitleStatus.ForeColor = Color.Green;
        //    }
        //    else
        //    {
        //        lblTitleStatus.Text = "✘ Please select a title.";
        //        lblTitleStatus.ForeColor = Color.Red;
        //    }
        //}
        private void txFirst_TextChanged(object sender, EventArgs e)
        {
            if (clsValidation.ValidName(txFirst.Text.Trim()))
            {
                lblFirstStatus.Text = "✔ Valid first name";
                lblFirstStatus.ForeColor = Color.Green;
            }
            else
            {
                lblFirstStatus.Text = "✘ Enter a valid first name.";
                lblFirstStatus.ForeColor = Color.Red;
            }
        }
        private void txLast_TextChanged(object sender, EventArgs e)
        {
            if (clsValidation.ValidName(txLast.Text.Trim()))
            {
                lblLastStatus.Text = "✔ Valid last name";
                lblLastStatus.ForeColor = Color.Green;
            }
            else
            {
                lblLastStatus.Text = "✘ Enter a valid last name.";
                lblLastStatus.ForeColor = Color.Red;
            }
        }
        private void txEmail_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txEmail.Text))
            {
                lblEmailStatus.Text = "";
            }
            else if (clsValidation.ValidEmail(txEmail.Text.Trim()))
            {
                lblEmailStatus.Text = "✔ Valid email address";
                lblEmailStatus.ForeColor = Color.Green;
            }
            else
            {
                lblEmailStatus.Text = "✘ Enter a valid email address.";
                lblEmailStatus.ForeColor = Color.Red;
            }
        }
        private void txPhone_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txPhone.Text))
            {
                lblPhoneStatus.Text = "";
            }
            else if (clsValidation.ValidPhone(txPhone.Text.Trim()))
            {
                lblPhoneStatus.Text = "✔ Valid phone number";
                lblPhoneStatus.ForeColor = Color.Green;
            }
            else
            {
                lblPhoneStatus.Text = "✘ Phone number must contain 10 digits.";
                lblPhoneStatus.ForeColor = Color.Red;
            }
        }
        private void txPhoneSecondary_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txPhoneSecondary.Text))
            {
                lblPhoneSecondaryStatus.Text = "";
            }
            else if (clsValidation.ValidPhone(txPhoneSecondary.Text.Trim()))
            {
                lblPhoneSecondaryStatus.Text = "✔ Valid secondary phone";
                lblPhoneSecondaryStatus.ForeColor = Color.Green;
            }
            else
            {
                lblPhoneSecondaryStatus.Text = "✘ Phone number must contain 10 digits.";
                lblPhoneSecondaryStatus.ForeColor = Color.Red;
            }
        }
        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clsValidation.ValidState(cboState.Text))
            {
                lblStateStatus.Text = "✔ State selected";
                lblStateStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStateStatus.Text = "✘ Please select a state.";
                lblStateStatus.ForeColor = Color.Red;
            }
        }
        private void txZip_TextChanged(object sender, EventArgs e)
        {
            if (clsValidation.ValidZip(txZip.Text.Trim()))
            {
                lblZipStatus.Text = "✔ Valid ZIP code";
                lblZipStatus.ForeColor = Color.Green;
            }
            else
            {
                lblZipStatus.Text = "✘ ZIP code must be 5 digits.";
                lblZipStatus.ForeColor = Color.Red;
            }
        }
        private void cboQuestion1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQuestion1.SelectedIndex >= 0)
            {
                lblQuestion1Status.Text = "✔ Question selected";
                lblQuestion1Status.ForeColor = Color.Green;
            }
            else
            {
                lblQuestion1Status.Text = "✘ Please select a question.";
                lblQuestion1Status.ForeColor = Color.Red;
            }
        }
        private void txAnswer1_TextChanged(object sender, EventArgs e)
        {
            if (clsValidation.HasAnswer(txAnswer1.Text.Trim()))
            {
                lblAnswer1Status.Text = "✔ Answer entered";
                lblAnswer1Status.ForeColor = Color.Green;
            }
            else
            {
                lblAnswer1Status.Text = "✘ Please enter an answer.";
                lblAnswer1Status.ForeColor = Color.Red;
            }
        }
        private void cboQuestion2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQuestion2.SelectedIndex >= 0)
            {
                lblQuestion2Status.Text = "✔ Question selected";
                lblQuestion2Status.ForeColor = Color.Green;
            }
            else
            {
                lblQuestion2Status.Text = "✘ Please select a question.";
                lblQuestion2Status.ForeColor = Color.Red;
            }
        }
        private void txAnswer2_TextChanged(object sender, EventArgs e)
        {
            if (clsValidation.HasAnswer(txAnswer2.Text.Trim()))
            {
                lblAnswer2Status.Text = "✔ Answer entered";
                lblAnswer2Status.ForeColor = Color.Green;
            }
            else
            {
                lblAnswer2Status.Text = "✘ Please enter an answer.";
                lblAnswer2Status.ForeColor = Color.Red;
            }
        }
        private void cboQuestion3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboQuestion3.SelectedIndex >= 0)
            {
                lblQuestion3Status.Text = "✔ Question selected";
                lblQuestion3Status.ForeColor = Color.Green;
            }
            else
            {
                lblQuestion3Status.Text = "✘ Please select a question.";
                lblQuestion3Status.ForeColor = Color.Red;
            }
        }
        private void txAnswer3_TextChanged(object sender, EventArgs e)
        {
            if (clsValidation.HasAnswer(txAnswer3.Text.Trim()))
            {
                lblAnswer3Status.Text = "✔ Answer entered";
                lblAnswer3Status.ForeColor = Color.Green;
            }
            else
            {
                lblAnswer3Status.Text = "✘ Please enter an answer.";
                lblAnswer3Status.ForeColor = Color.Red;
            }
        }
        private void LoadSecurityQuestions(
    int setID,
    ComboBox comboBox)
        {
            comboBox.Items.Clear();

            foreach (string question in
                clsDatabase.GetSecurityQuestions(setID))
            {
                comboBox.Items.Add(question);
            }
        }

        private void frmManagerEditor_Load(object sender, EventArgs e)
        {
            cboTitle.DropDownStyle =
        ComboBoxStyle.DropDownList;

            cboTitle.Items.Clear();

            cboTitle.Items.AddRange(
                new string[]
                {
            "Mr.",
            "Mrs.",
            "Ms.",
            "Miss",
            "Dr."
                });

            cboState.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboState.Items.Clear();

            cboState.Items.AddRange(
                new string[]
                {
            "AA","AE","AP",
            "AL","AK","AZ","AR","CA",
            "CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA",
            "KS","KY","LA","ME","MD",
            "MA","MI","MN","MS","MO",
            "MT","NE","NV","NH","NJ",
            "NM","NY","NC","ND","OH",
            "OK","OR","PA","RI","SC",
            "SD","TN","TX","UT","VT",
            "VA","WA","WV","WI","WY"
                });

            LoadSecurityQuestions(1, cboQuestion1);
            LoadSecurityQuestions(2, cboQuestion2);
            LoadSecurityQuestions(3, cboQuestion3);
            if (editMode)
            {
                Text = "Edit Manager";

                btnCCreate.Text = "Save Changes";

                txPassword.Enabled = false;

                cboQuestion1.Enabled = false;
                txAnswer1.Enabled = false;

                cboQuestion2.Enabled = false;
                txAnswer2.Enabled = false;

                cboQuestion3.Enabled = false;
                txAnswer3.Enabled = false;

                LoadManager();
            }
        }
        private bool QuestionsAreUnique()
        {
            return new HashSet<string>
    {
        cboQuestion1.Text,
        cboQuestion2.Text,
        cboQuestion3.Text
    }.Count == 3;
        }

        private void btnCCreate_Click(object sender, EventArgs e)
        {
            if (!clsValidation.ValidName(
                txFirst.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid first name.");

                txFirst.Focus();
                return;
            }

            if (!clsValidation.ValidName(
                txLast.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid last name.");

                txLast.Focus();
                return;
            }

            if (!clsValidation.ValidUsername(
                txUser.Text.Trim()))
            {
                MessageBox.Show(
                    "Username must be 8-20 characters and contain at least one letter and one number.");

                txUser.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(
                    txEmail.Text) &&
                !clsValidation.ValidEmail(
                    txEmail.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid email address.");

                txEmail.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(
                    txPhone.Text) &&
                !clsValidation.ValidPhone(
                    txPhone.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid primary phone number.");

                txPhone.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(
                    txPhoneSecondary.Text) &&
                !clsValidation.ValidPhone(
                    txPhoneSecondary.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid secondary phone number.");

                txPhoneSecondary.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txAddress1.Text))
            {
                MessageBox.Show(
                    "Please enter Address 1.");

                txAddress1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txCity.Text))
            {
                MessageBox.Show(
                    "Please enter a city.");

                txCity.Focus();
                return;
            }

            if (!clsValidation.ValidState(
                cboState.Text))
            {
                MessageBox.Show(
                    "Please select a valid state.");

                cboState.Focus();
                return;
            }

            if (!clsValidation.ValidZip(
                txZip.Text.Trim()))
            {
                MessageBox.Show(
                    "Please enter a valid ZIP code.");

                txZip.Focus();
                return;
            }

            // Only required when creating a NEW manager.
            if (!editMode)
            {
                if (string.IsNullOrWhiteSpace(
                    txPassword.Text))
                {
                    MessageBox.Show(
                        "Password cannot be blank.");

                    txPassword.Focus();
                    return;
                }

                if (!clsPasswordRules.HasComplexity(
                    txPassword.Text))
                {
                    MessageBox.Show(
                        "Password does not meet complexity requirements.");

                    txPassword.Focus();
                    return;
                }

                if (cboQuestion1.SelectedIndex == -1 ||
                    !clsValidation.HasAnswer(
                        txAnswer1.Text.Trim()))
                {
                    MessageBox.Show(
                        "Please complete Security Question 1.");

                    cboQuestion1.Focus();
                    return;
                }

                if (cboQuestion2.SelectedIndex == -1 ||
                    !clsValidation.HasAnswer(
                        txAnswer2.Text.Trim()))
                {
                    MessageBox.Show(
                        "Please complete Security Question 2.");

                    cboQuestion2.Focus();
                    return;
                }

                if (cboQuestion3.SelectedIndex == -1 ||
                    !clsValidation.HasAnswer(
                        txAnswer3.Text.Trim()))
                {
                    MessageBox.Show(
                        "Please complete Security Question 3.");

                    cboQuestion3.Focus();
                    return;
                }

                if (!QuestionsAreUnique())
                {
                    MessageBox.Show(
                        "Security Questions must be unique.");

                    cboQuestion1.Focus();
                    return;
                }
            }

            try
            {
                // =========================
                // EDIT EXISTING MANAGER
                // =========================
                if (editMode)
                {
                    bool success =
                        clsDatabase.UpdateManager(
                            managerID,
                            cboTitle.Text,
                            txFirst.Text.Trim(),
                            txLast.Text.Trim(),
                            txSuffix.Text.Trim(),
                            txAddress1.Text.Trim(),
                            txAddress2.Text.Trim(),
                            txAddress3.Text.Trim(),
                            txCity.Text.Trim(),
                            txZip.Text.Trim(),
                            cboState.Text,
                            txEmail.Text.Trim(),
                            txPhone.Text.Trim(),
                            txPhoneSecondary.Text.Trim(),
                            txUser.Text.Trim());

                    if (success)
                    {
                        MessageBox.Show(
                            "Manager updated successfully.",
                            "Edit Manager",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        DialogResult =
                            DialogResult.OK;

                        Close();
                    }

                    return;
                }

                // =========================
                // CREATE NEW MANAGER
                // =========================
                int q1 =
                    clsDatabase.GetQuestionID(
                        cboQuestion1.Text);

                int q2 =
                    clsDatabase.GetQuestionID(
                        cboQuestion2.Text);

                int q3 =
                    clsDatabase.GetQuestionID(
                        cboQuestion3.Text);

                bool createSuccess =
                    clsDatabase.CreateManagerAccount(
                        cboTitle.Text,
                        txFirst.Text.Trim(),
                        txLast.Text.Trim(),
                        txSuffix.Text.Trim(),
                        txAddress1.Text.Trim(),
                        txAddress2.Text.Trim(),
                        txAddress3.Text.Trim(),
                        txCity.Text.Trim(),
                        txZip.Text.Trim(),
                        cboState.Text,
                        txEmail.Text.Trim(),
                        txPhone.Text.Trim(),
                        txPhoneSecondary.Text.Trim(),
                        txUser.Text.Trim(),
                        txPassword.Text.Trim(),
                        q1,
                        txAnswer1.Text.Trim(),
                        q2,
                        txAnswer2.Text.Trim(),
                        q3,
                        txAnswer3.Text.Trim());

                if (createSuccess)
                {
                    MessageBox.Show(
                        "Manager account created successfully.");

                    DialogResult =
                        DialogResult.OK;

                    Close();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 ||
                    ex.Number == 2601)
                {
                    MessageBox.Show(
                        "That username already exists.\n\nPlease choose another username.",
                        "Duplicate Username",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        editMode
                            ? "Unable to update the manager account.\n\n" + ex.Message
                            : "Unable to create the manager account.\n\n" + ex.Message,
                        "Database Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An unexpected error occurred.\n\n" +
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
                    "ManagerEditorHelp.txt");

                if (!File.Exists(helpPath))
                {
                    MessageBox.Show(
                        "The Manager Editor help file could not be found.",
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
