namespace BurlesonJLogonView
{
    partial class frmCustomerEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomerEditor));
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbPersonal = new System.Windows.Forms.GroupBox();
            this.lblPtitle = new System.Windows.Forms.Label();
            this.cboTitle = new System.Windows.Forms.ComboBox();
            this.lblFirst = new System.Windows.Forms.Label();
            this.lblLast = new System.Windows.Forms.Label();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.gbContact = new System.Windows.Forms.GroupBox();
            this.txtPhoneSecondary = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lbl2Phone = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.gbAddress = new System.Windows.Forms.GroupBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblAddress1 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.lblAddress2 = new System.Windows.Forms.Label();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.lblAddress3 = new System.Windows.Forms.Label();
            this.cboState = new System.Windows.Forms.ComboBox();
            this.lblZip = new System.Windows.Forms.Label();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.gbAccount = new System.Windows.Forms.GroupBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.gbPersonal.SuspendLayout();
            this.gbContact.SuspendLayout();
            this.gbAddress.SuspendLayout();
            this.gbAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(125, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(475, 27);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Edit Customer";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbPersonal
            // 
            this.gbPersonal.Controls.Add(this.txtSuffix);
            this.gbPersonal.Controls.Add(this.txtLastName);
            this.gbPersonal.Controls.Add(this.txtFirstName);
            this.gbPersonal.Controls.Add(this.lblSuffix);
            this.gbPersonal.Controls.Add(this.lblLast);
            this.gbPersonal.Controls.Add(this.lblFirst);
            this.gbPersonal.Controls.Add(this.cboTitle);
            this.gbPersonal.Controls.Add(this.lblPtitle);
            this.gbPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPersonal.Location = new System.Drawing.Point(0, 39);
            this.gbPersonal.Name = "gbPersonal";
            this.gbPersonal.Size = new System.Drawing.Size(686, 165);
            this.gbPersonal.TabIndex = 2;
            this.gbPersonal.TabStop = false;
            this.gbPersonal.Text = "Personal Information";
            // 
            // lblPtitle
            // 
            this.lblPtitle.AutoSize = true;
            this.lblPtitle.Location = new System.Drawing.Point(10, 30);
            this.lblPtitle.Name = "lblPtitle";
            this.lblPtitle.Size = new System.Drawing.Size(46, 20);
            this.lblPtitle.TabIndex = 0;
            this.lblPtitle.Text = "Title: ";
            // 
            // cboTitle
            // 
            this.cboTitle.FormattingEnabled = true;
            this.cboTitle.Location = new System.Drawing.Point(153, 27);
            this.cboTitle.Name = "cboTitle";
            this.cboTitle.Size = new System.Drawing.Size(158, 28);
            this.cboTitle.TabIndex = 1;
            // 
            // lblFirst
            // 
            this.lblFirst.AutoSize = true;
            this.lblFirst.Location = new System.Drawing.Point(10, 67);
            this.lblFirst.Name = "lblFirst";
            this.lblFirst.Size = new System.Drawing.Size(90, 20);
            this.lblFirst.TabIndex = 2;
            this.lblFirst.Text = "First Name:";
            // 
            // lblLast
            // 
            this.lblLast.AutoSize = true;
            this.lblLast.Location = new System.Drawing.Point(10, 100);
            this.lblLast.Name = "lblLast";
            this.lblLast.Size = new System.Drawing.Size(90, 20);
            this.lblLast.TabIndex = 3;
            this.lblLast.Text = "Last Name:";
            // 
            // lblSuffix
            // 
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Location = new System.Drawing.Point(10, 132);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(57, 20);
            this.lblSuffix.TabIndex = 4;
            this.lblSuffix.Text = "Suffix: ";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(153, 68);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(493, 26);
            this.txtFirstName.TabIndex = 5;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(153, 100);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(493, 26);
            this.txtLastName.TabIndex = 6;
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(153, 133);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(131, 26);
            this.txtSuffix.TabIndex = 7;
            // 
            // gbContact
            // 
            this.gbContact.Controls.Add(this.txtPhone);
            this.gbContact.Controls.Add(this.txtPhoneSecondary);
            this.gbContact.Controls.Add(this.txtEmail);
            this.gbContact.Controls.Add(this.lbl2Phone);
            this.gbContact.Controls.Add(this.lblPhone);
            this.gbContact.Controls.Add(this.lblEmail);
            this.gbContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbContact.Location = new System.Drawing.Point(0, 204);
            this.gbContact.Name = "gbContact";
            this.gbContact.Size = new System.Drawing.Size(686, 130);
            this.gbContact.TabIndex = 8;
            this.gbContact.TabStop = false;
            this.gbContact.Text = "Contact Information";
            // 
            // txtPhoneSecondary
            // 
            this.txtPhoneSecondary.Location = new System.Drawing.Point(153, 100);
            this.txtPhoneSecondary.Name = "txtPhoneSecondary";
            this.txtPhoneSecondary.Size = new System.Drawing.Size(493, 26);
            this.txtPhoneSecondary.TabIndex = 6;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(153, 25);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(493, 26);
            this.txtEmail.TabIndex = 5;
            // 
            // lbl2Phone
            // 
            this.lbl2Phone.AutoSize = true;
            this.lbl2Phone.Location = new System.Drawing.Point(10, 100);
            this.lbl2Phone.Name = "lbl2Phone";
            this.lbl2Phone.Size = new System.Drawing.Size(90, 20);
            this.lbl2Phone.TabIndex = 3;
            this.lbl2Phone.Text = "2nd Phone:";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(10, 67);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(63, 20);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "Phone: ";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(10, 30);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(52, 20);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email:";
            // 
            // gbAddress
            // 
            this.gbAddress.Controls.Add(this.txtZip);
            this.gbAddress.Controls.Add(this.lblZip);
            this.gbAddress.Controls.Add(this.cboState);
            this.gbAddress.Controls.Add(this.txtAddress3);
            this.gbAddress.Controls.Add(this.lblAddress3);
            this.gbAddress.Controls.Add(this.txtAddress2);
            this.gbAddress.Controls.Add(this.lblAddress2);
            this.gbAddress.Controls.Add(this.txtCity);
            this.gbAddress.Controls.Add(this.txtAddress1);
            this.gbAddress.Controls.Add(this.lblState);
            this.gbAddress.Controls.Add(this.lblCity);
            this.gbAddress.Controls.Add(this.lblAddress1);
            this.gbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAddress.Location = new System.Drawing.Point(0, 336);
            this.gbAddress.Name = "gbAddress";
            this.gbAddress.Size = new System.Drawing.Size(686, 242);
            this.gbAddress.TabIndex = 8;
            this.gbAddress.TabStop = false;
            this.gbAddress.Text = "Address";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(153, 143);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(493, 26);
            this.txtCity.TabIndex = 6;
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(153, 27);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(493, 26);
            this.txtAddress1.TabIndex = 5;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(10, 183);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(56, 20);
            this.lblState.TabIndex = 4;
            this.lblState.Text = "State: ";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(10, 149);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(39, 20);
            this.lblCity.TabIndex = 3;
            this.lblCity.Text = "City:";
            // 
            // lblAddress1
            // 
            this.lblAddress1.AutoSize = true;
            this.lblAddress1.Location = new System.Drawing.Point(10, 30);
            this.lblAddress1.Name = "lblAddress1";
            this.lblAddress1.Size = new System.Drawing.Size(89, 20);
            this.lblAddress1.TabIndex = 0;
            this.lblAddress1.Text = "Address 1: ";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(153, 64);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(493, 26);
            this.txtPhone.TabIndex = 7;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(153, 59);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(493, 26);
            this.txtAddress2.TabIndex = 9;
            // 
            // lblAddress2
            // 
            this.lblAddress2.AutoSize = true;
            this.lblAddress2.Location = new System.Drawing.Point(10, 62);
            this.lblAddress2.Name = "lblAddress2";
            this.lblAddress2.Size = new System.Drawing.Size(89, 20);
            this.lblAddress2.TabIndex = 8;
            this.lblAddress2.Text = "Address 2: ";
            // 
            // txtAddress3
            // 
            this.txtAddress3.Location = new System.Drawing.Point(153, 91);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(493, 26);
            this.txtAddress3.TabIndex = 11;
            // 
            // lblAddress3
            // 
            this.lblAddress3.AutoSize = true;
            this.lblAddress3.Location = new System.Drawing.Point(10, 94);
            this.lblAddress3.Name = "lblAddress3";
            this.lblAddress3.Size = new System.Drawing.Size(89, 20);
            this.lblAddress3.TabIndex = 10;
            this.lblAddress3.Text = "Address 3: ";
            // 
            // cboState
            // 
            this.cboState.FormattingEnabled = true;
            this.cboState.Location = new System.Drawing.Point(153, 175);
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(158, 28);
            this.cboState.TabIndex = 8;
            // 
            // lblZip
            // 
            this.lblZip.AutoSize = true;
            this.lblZip.Location = new System.Drawing.Point(10, 219);
            this.lblZip.Name = "lblZip";
            this.lblZip.Size = new System.Drawing.Size(39, 20);
            this.lblZip.TabIndex = 12;
            this.lblZip.Text = "Zip: ";
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(153, 210);
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(158, 26);
            this.txtZip.TabIndex = 13;
            // 
            // gbAccount
            // 
            this.gbAccount.Controls.Add(this.txtUsername);
            this.gbAccount.Controls.Add(this.lblUsername);
            this.gbAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAccount.Location = new System.Drawing.Point(0, 578);
            this.gbAccount.Name = "gbAccount";
            this.gbAccount.Size = new System.Drawing.Size(686, 62);
            this.gbAccount.TabIndex = 9;
            this.gbAccount.TabStop = false;
            this.gbAccount.Text = "Account Information";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(153, 25);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(493, 26);
            this.txtUsername.TabIndex = 5;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(10, 30);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(87, 20);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(70, 676);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(161, 37);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(284, 676);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(161, 37);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(580, 688);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(106, 37);
            this.btnHelp.TabIndex = 12;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // frmCustomerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 728);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbAccount);
            this.Controls.Add(this.gbAddress);
            this.Controls.Add(this.gbContact);
            this.Controls.Add(this.gbPersonal);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCustomerEditor";
            this.ShowInTaskbar = false;
            this.Text = "Edit Customer";
            this.Load += new System.EventHandler(this.frmCustomerEditor_Load);
            this.gbPersonal.ResumeLayout(false);
            this.gbPersonal.PerformLayout();
            this.gbContact.ResumeLayout(false);
            this.gbContact.PerformLayout();
            this.gbAddress.ResumeLayout(false);
            this.gbAddress.PerformLayout();
            this.gbAccount.ResumeLayout(false);
            this.gbAccount.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox gbPersonal;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.Label lblLast;
        private System.Windows.Forms.Label lblFirst;
        private System.Windows.Forms.ComboBox cboTitle;
        private System.Windows.Forms.Label lblPtitle;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.GroupBox gbContact;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtPhoneSecondary;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lbl2Phone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.GroupBox gbAddress;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblAddress1;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.Label lblAddress3;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.Label lblAddress2;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.Label lblZip;
        private System.Windows.Forms.ComboBox cboState;
        private System.Windows.Forms.GroupBox gbAccount;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnHelp;
    }
}