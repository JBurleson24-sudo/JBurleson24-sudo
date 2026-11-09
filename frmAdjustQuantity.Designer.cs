namespace BurlesonJLogonView
{
    partial class frmAdjustQuantity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdjustQuantity));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblProductCaption = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblCurrentQuantity = new System.Windows.Forms.Label();
            this.lblAdjustment = new System.Windows.Forms.Label();
            this.nudAdjustment = new System.Windows.Forms.NumericUpDown();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdjustment)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(387, 33);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Adjust Inventory Quantity";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProductCaption
            // 
            this.lblProductCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductCaption.Location = new System.Drawing.Point(13, 42);
            this.lblProductCaption.Name = "lblProductCaption";
            this.lblProductCaption.Size = new System.Drawing.Size(69, 27);
            this.lblProductCaption.TabIndex = 5;
            this.lblProductCaption.Text = "Product:";
            // 
            // lblProduct
            // 
            this.lblProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(13, 69);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(139, 27);
            this.lblProduct.TabIndex = 6;
            this.lblProduct.Text = "Product Name";
            // 
            // lblCurrentQuantity
            // 
            this.lblCurrentQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentQuantity.Location = new System.Drawing.Point(12, 116);
            this.lblCurrentQuantity.Name = "lblCurrentQuantity";
            this.lblCurrentQuantity.Size = new System.Drawing.Size(165, 27);
            this.lblCurrentQuantity.TabIndex = 7;
            this.lblCurrentQuantity.Text = "Current Quantity: 0";
            // 
            // lblAdjustment
            // 
            this.lblAdjustment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustment.Location = new System.Drawing.Point(13, 143);
            this.lblAdjustment.Name = "lblAdjustment";
            this.lblAdjustment.Size = new System.Drawing.Size(139, 27);
            this.lblAdjustment.TabIndex = 8;
            this.lblAdjustment.Text = "Adjustment:";
            // 
            // nudAdjustment
            // 
            this.nudAdjustment.Location = new System.Drawing.Point(17, 173);
            this.nudAdjustment.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudAdjustment.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudAdjustment.Name = "nudAdjustment";
            this.nudAdjustment.Size = new System.Drawing.Size(343, 20);
            this.nudAdjustment.TabIndex = 9;
            this.nudAdjustment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblInstructions
            // 
            this.lblInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(13, 207);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(268, 27);
            this.lblInstructions.TabIndex = 10;
            this.lblInstructions.Text = "Positive = Add / Negative = Remove";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(78, 273);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(123, 31);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(207, 273);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(123, 31);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAdjustQuantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 311);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.nudAdjustment);
            this.Controls.Add(this.lblAdjustment);
            this.Controls.Add(this.lblCurrentQuantity);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lblProductCaption);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdjustQuantity";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adjust Quantity";
            this.Load += new System.EventHandler(this.frmAdjustQuantity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAdjustment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblProductCaption;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblCurrentQuantity;
        private System.Windows.Forms.Label lblAdjustment;
        private System.Windows.Forms.NumericUpDown nudAdjustment;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}