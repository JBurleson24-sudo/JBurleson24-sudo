namespace BurlesonJLogonView
{
    partial class frmReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReports));
            this.grpSales = new System.Windows.Forms.GroupBox();
            this.grpInventory = new System.Windows.Forms.GroupBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSalesDate = new System.Windows.Forms.Label();
            this.dtpSalesDate = new System.Windows.Forms.DateTimePicker();
            this.btnDailySales = new System.Windows.Forms.Button();
            this.btnWeeklySales = new System.Windows.Forms.Button();
            this.btnMonthlySales = new System.Windows.Forms.Button();
            this.btnAvailableInventory = new System.Windows.Forms.Button();
            this.btnRestockInventory = new System.Windows.Forms.Button();
            this.btnAllInventory = new System.Windows.Forms.Button();
            this.grpSales.SuspendLayout();
            this.grpInventory.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSales
            // 
            this.grpSales.Controls.Add(this.btnMonthlySales);
            this.grpSales.Controls.Add(this.btnWeeklySales);
            this.grpSales.Controls.Add(this.btnDailySales);
            this.grpSales.Controls.Add(this.dtpSalesDate);
            this.grpSales.Controls.Add(this.lblSalesDate);
            this.grpSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSales.Location = new System.Drawing.Point(5, 61);
            this.grpSales.Name = "grpSales";
            this.grpSales.Size = new System.Drawing.Size(817, 194);
            this.grpSales.TabIndex = 0;
            this.grpSales.TabStop = false;
            this.grpSales.Text = "Sales Reports";
            // 
            // grpInventory
            // 
            this.grpInventory.Controls.Add(this.btnAllInventory);
            this.grpInventory.Controls.Add(this.btnRestockInventory);
            this.grpInventory.Controls.Add(this.btnAvailableInventory);
            this.grpInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInventory.Location = new System.Drawing.Point(5, 285);
            this.grpInventory.Name = "grpInventory";
            this.grpInventory.Size = new System.Drawing.Size(462, 194);
            this.grpInventory.TabIndex = 1;
            this.grpInventory.TabStop = false;
            this.grpInventory.Text = "Inventory Reports";
            // 
            // btnHelp
            // 
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(219, 511);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(188, 40);
            this.btnHelp.TabIndex = 2;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(453, 511);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(188, 40);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(178, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(475, 27);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "Manager Reports";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSalesDate
            // 
            this.lblSalesDate.AutoSize = true;
            this.lblSalesDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesDate.Location = new System.Drawing.Point(7, 84);
            this.lblSalesDate.Name = "lblSalesDate";
            this.lblSalesDate.Size = new System.Drawing.Size(129, 25);
            this.lblSalesDate.TabIndex = 0;
            this.lblSalesDate.Text = "Select Date:";
            // 
            // dtpSalesDate
            // 
            this.dtpSalesDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSalesDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSalesDate.Location = new System.Drawing.Point(162, 83);
            this.dtpSalesDate.Name = "dtpSalesDate";
            this.dtpSalesDate.Size = new System.Drawing.Size(628, 26);
            this.dtpSalesDate.TabIndex = 1;
            // 
            // btnDailySales
            // 
            this.btnDailySales.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDailySales.Location = new System.Drawing.Point(6, 141);
            this.btnDailySales.Name = "btnDailySales";
            this.btnDailySales.Size = new System.Drawing.Size(221, 53);
            this.btnDailySales.TabIndex = 2;
            this.btnDailySales.Text = "Daily Sales";
            this.btnDailySales.UseVisualStyleBackColor = true;
            this.btnDailySales.Click += new System.EventHandler(this.btnDailySales_Click);
            // 
            // btnWeeklySales
            // 
            this.btnWeeklySales.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeeklySales.Location = new System.Drawing.Point(308, 141);
            this.btnWeeklySales.Name = "btnWeeklySales";
            this.btnWeeklySales.Size = new System.Drawing.Size(221, 53);
            this.btnWeeklySales.TabIndex = 3;
            this.btnWeeklySales.Text = "Weekly Sales";
            this.btnWeeklySales.UseVisualStyleBackColor = true;
            this.btnWeeklySales.Click += new System.EventHandler(this.btnWeeklySales_Click);
            // 
            // btnMonthlySales
            // 
            this.btnMonthlySales.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonthlySales.Location = new System.Drawing.Point(596, 141);
            this.btnMonthlySales.Name = "btnMonthlySales";
            this.btnMonthlySales.Size = new System.Drawing.Size(221, 53);
            this.btnMonthlySales.TabIndex = 4;
            this.btnMonthlySales.Text = "Monthly Sales";
            this.btnMonthlySales.UseVisualStyleBackColor = true;
            this.btnMonthlySales.Click += new System.EventHandler(this.btnMonthlySales_Click);
            // 
            // btnAvailableInventory
            // 
            this.btnAvailableInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAvailableInventory.Location = new System.Drawing.Point(15, 33);
            this.btnAvailableInventory.Name = "btnAvailableInventory";
            this.btnAvailableInventory.Size = new System.Drawing.Size(398, 44);
            this.btnAvailableInventory.TabIndex = 0;
            this.btnAvailableInventory.Text = "Available Inventory";
            this.btnAvailableInventory.UseVisualStyleBackColor = true;
            this.btnAvailableInventory.Click += new System.EventHandler(this.btnAvailableInventory_Click);
            // 
            // btnRestockInventory
            // 
            this.btnRestockInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestockInventory.Location = new System.Drawing.Point(15, 83);
            this.btnRestockInventory.Name = "btnRestockInventory";
            this.btnRestockInventory.Size = new System.Drawing.Size(398, 44);
            this.btnRestockInventory.TabIndex = 1;
            this.btnRestockInventory.Text = "Items Needing Restock";
            this.btnRestockInventory.UseVisualStyleBackColor = true;
            this.btnRestockInventory.Click += new System.EventHandler(this.btnRestockInventory_Click);
            // 
            // btnAllInventory
            // 
            this.btnAllInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllInventory.Location = new System.Drawing.Point(15, 133);
            this.btnAllInventory.Name = "btnAllInventory";
            this.btnAllInventory.Size = new System.Drawing.Size(398, 44);
            this.btnAllInventory.TabIndex = 2;
            this.btnAllInventory.Text = "All Inventory";
            this.btnAllInventory.UseVisualStyleBackColor = true;
            this.btnAllInventory.Click += new System.EventHandler(this.btnAllInventory_Click);
            // 
            // frmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 561);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.grpInventory);
            this.Controls.Add(this.grpSales);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manager Reports";
            this.grpSales.ResumeLayout(false);
            this.grpSales.PerformLayout();
            this.grpInventory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSales;
        private System.Windows.Forms.GroupBox grpInventory;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnMonthlySales;
        private System.Windows.Forms.Button btnWeeklySales;
        private System.Windows.Forms.Button btnDailySales;
        private System.Windows.Forms.DateTimePicker dtpSalesDate;
        private System.Windows.Forms.Label lblSalesDate;
        private System.Windows.Forms.Button btnAllInventory;
        private System.Windows.Forms.Button btnRestockInventory;
        private System.Windows.Forms.Button btnAvailableInventory;
    }
}