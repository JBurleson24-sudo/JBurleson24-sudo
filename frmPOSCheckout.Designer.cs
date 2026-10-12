namespace BurlesonJLogonView
{
    partial class frmPOSCheckout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPOSCheckout));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCompleteSale = new System.Windows.Forms.Button();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblExpiration = new System.Windows.Forms.Label();
            this.lblCCV = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.txtExpiration = new System.Windows.Forms.TextBox();
            this.txtCCV = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(403, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(191, 43);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCompleteSale
            // 
            this.btnCompleteSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompleteSale.Location = new System.Drawing.Point(180, 385);
            this.btnCompleteSale.Name = "btnCompleteSale";
            this.btnCompleteSale.Size = new System.Drawing.Size(191, 43);
            this.btnCompleteSale.TabIndex = 19;
            this.btnCompleteSale.Text = "Complete Sale";
            this.btnCompleteSale.UseVisualStyleBackColor = true;
            this.btnCompleteSale.Click += new System.EventHandler(this.btnCompleteSale_Click);
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardNumber.Location = new System.Drawing.Point(11, 80);
            this.lblCardNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(145, 25);
            this.lblCardNumber.TabIndex = 14;
            this.lblCardNumber.Text = "Card Number:";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(175, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(475, 27);
            this.lblTitle.TabIndex = 23;
            this.lblTitle.Text = " Payment Information";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExpiration
            // 
            this.lblExpiration.AutoSize = true;
            this.lblExpiration.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiration.Location = new System.Drawing.Point(11, 179);
            this.lblExpiration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExpiration.Name = "lblExpiration";
            this.lblExpiration.Size = new System.Drawing.Size(165, 25);
            this.lblExpiration.TabIndex = 24;
            this.lblExpiration.Text = "Expiration Date:";
            // 
            // lblCCV
            // 
            this.lblCCV.AutoSize = true;
            this.lblCCV.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCCV.Location = new System.Drawing.Point(11, 271);
            this.lblCCV.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCCV.Name = "lblCCV";
            this.lblCCV.Size = new System.Drawing.Size(62, 25);
            this.lblCCV.TabIndex = 25;
            this.lblCCV.Text = "CCV:";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardNumber.Location = new System.Drawing.Point(172, 77);
            this.txtCardNumber.MaxLength = 19;
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(307, 29);
            this.txtCardNumber.TabIndex = 26;
            this.txtCardNumber.TextChanged += new System.EventHandler(this.txtCardNumber_TextChanged);
            // 
            // txtExpiration
            // 
            this.txtExpiration.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpiration.Location = new System.Drawing.Point(180, 177);
            this.txtExpiration.MaxLength = 5;
            this.txtExpiration.Name = "txtExpiration";
            this.txtExpiration.Size = new System.Drawing.Size(175, 29);
            this.txtExpiration.TabIndex = 27;
            this.txtExpiration.TextChanged += new System.EventHandler(this.txtExpiration_TextChanged);
            // 
            // txtCCV
            // 
            this.txtCCV.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCV.Location = new System.Drawing.Point(172, 271);
            this.txtCCV.MaxLength = 3;
            this.txtCCV.Name = "txtCCV";
            this.txtCCV.Size = new System.Drawing.Size(175, 29);
            this.txtCCV.TabIndex = 28;
            // 
            // frmPOSCheckout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtCCV);
            this.Controls.Add(this.txtExpiration);
            this.Controls.Add(this.txtCardNumber);
            this.Controls.Add(this.lblCCV);
            this.Controls.Add(this.lblExpiration);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCompleteSale);
            this.Controls.Add(this.lblCardNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPOSCheckout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Checkout";
            this.Load += new System.EventHandler(this.frmPOSCheckout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCompleteSale;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblExpiration;
        private System.Windows.Forms.Label lblCCV;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.TextBox txtExpiration;
        private System.Windows.Forms.TextBox txtCCV;
    }
}