namespace BurlesonJLogonView
{
    partial class frmReturns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReturns));
            this.lblOrderID = new System.Windows.Forms.Label();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.btnFindOrder = new System.Windows.Forms.Button();
            this.dgvReturnItems = new System.Windows.Forms.DataGridView();
            this.lblReturnQuantity = new System.Windows.Forms.Label();
            this.lblReturnReason = new System.Windows.Forms.Label();
            this.nudReturnQuantity = new System.Windows.Forms.NumericUpDown();
            this.txtReturnReason = new System.Windows.Forms.TextBox();
            this.chkRestock = new System.Windows.Forms.CheckBox();
            this.btnProcessReturn = new System.Windows.Forms.Button();
            this.lblRefundTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReturnQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOrderID
            // 
            this.lblOrderID.AutoSize = true;
            this.lblOrderID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderID.Location = new System.Drawing.Point(33, 30);
            this.lblOrderID.Name = "lblOrderID";
            this.lblOrderID.Size = new System.Drawing.Size(153, 25);
            this.lblOrderID.TabIndex = 0;
            this.lblOrderID.Text = "Order Number:";
            // 
            // txtOrderID
            // 
            this.txtOrderID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderID.Location = new System.Drawing.Point(290, 30);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(241, 31);
            this.txtOrderID.TabIndex = 1;
            // 
            // btnFindOrder
            // 
            this.btnFindOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindOrder.Location = new System.Drawing.Point(630, 25);
            this.btnFindOrder.Name = "btnFindOrder";
            this.btnFindOrder.Size = new System.Drawing.Size(140, 40);
            this.btnFindOrder.TabIndex = 2;
            this.btnFindOrder.Text = "Find Order";
            this.btnFindOrder.UseVisualStyleBackColor = true;
            this.btnFindOrder.Click += new System.EventHandler(this.btnFindOrder_Click);
            // 
            // dgvReturnItems
            // 
            this.dgvReturnItems.AllowUserToAddRows = false;
            this.dgvReturnItems.AllowUserToDeleteRows = false;
            this.dgvReturnItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReturnItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReturnItems.Location = new System.Drawing.Point(20, 80);
            this.dgvReturnItems.MultiSelect = false;
            this.dgvReturnItems.Name = "dgvReturnItems";
            this.dgvReturnItems.ReadOnly = true;
            this.dgvReturnItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReturnItems.Size = new System.Drawing.Size(750, 280);
            this.dgvReturnItems.TabIndex = 3;
            this.dgvReturnItems.SelectionChanged += new System.EventHandler(this.dgvReturnItems_SelectionChanged);
            // 
            // lblReturnQuantity
            // 
            this.lblReturnQuantity.AutoSize = true;
            this.lblReturnQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnQuantity.Location = new System.Drawing.Point(20, 390);
            this.lblReturnQuantity.Name = "lblReturnQuantity";
            this.lblReturnQuantity.Size = new System.Drawing.Size(168, 25);
            this.lblReturnQuantity.TabIndex = 4;
            this.lblReturnQuantity.Text = "Return Quantity:";
            // 
            // lblReturnReason
            // 
            this.lblReturnReason.AutoSize = true;
            this.lblReturnReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnReason.Location = new System.Drawing.Point(20, 440);
            this.lblReturnReason.Name = "lblReturnReason";
            this.lblReturnReason.Size = new System.Drawing.Size(92, 25);
            this.lblReturnReason.TabIndex = 5;
            this.lblReturnReason.Text = "Reason:";
            this.lblReturnReason.Click += new System.EventHandler(this.lblReturnReason_Click);
            // 
            // nudReturnQuantity
            // 
            this.nudReturnQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudReturnQuantity.Location = new System.Drawing.Point(194, 389);
            this.nudReturnQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudReturnQuantity.Name = "nudReturnQuantity";
            this.nudReturnQuantity.Size = new System.Drawing.Size(90, 26);
            this.nudReturnQuantity.TabIndex = 6;
            this.nudReturnQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtReturnReason
            // 
            this.txtReturnReason.Location = new System.Drawing.Point(118, 445);
            this.txtReturnReason.Name = "txtReturnReason";
            this.txtReturnReason.Size = new System.Drawing.Size(550, 20);
            this.txtReturnReason.TabIndex = 7;
            // 
            // chkRestock
            // 
            this.chkRestock.AutoSize = true;
            this.chkRestock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRestock.Location = new System.Drawing.Point(190, 480);
            this.chkRestock.Name = "chkRestock";
            this.chkRestock.Size = new System.Drawing.Size(259, 29);
            this.chkRestock.TabIndex = 8;
            this.chkRestock.Text = "Return item to inventory";
            this.chkRestock.UseVisualStyleBackColor = true;
            // 
            // btnProcessReturn
            // 
            this.btnProcessReturn.Enabled = false;
            this.btnProcessReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessReturn.Location = new System.Drawing.Point(540, 540);
            this.btnProcessReturn.Name = "btnProcessReturn";
            this.btnProcessReturn.Size = new System.Drawing.Size(200, 45);
            this.btnProcessReturn.TabIndex = 9;
            this.btnProcessReturn.Text = "Process Return";
            this.btnProcessReturn.UseVisualStyleBackColor = true;
            this.btnProcessReturn.Click += new System.EventHandler(this.btnProcessReturn_Click);
            // 
            // lblRefundTotal
            // 
            this.lblRefundTotal.AutoSize = true;
            this.lblRefundTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefundTotal.Location = new System.Drawing.Point(20, 550);
            this.lblRefundTotal.Name = "lblRefundTotal";
            this.lblRefundTotal.Size = new System.Drawing.Size(159, 25);
            this.lblRefundTotal.TabIndex = 10;
            this.lblRefundTotal.Text = "Refund Total: —";
            // 
            // frmReturns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 614);
            this.Controls.Add(this.lblRefundTotal);
            this.Controls.Add(this.btnProcessReturn);
            this.Controls.Add(this.chkRestock);
            this.Controls.Add(this.txtReturnReason);
            this.Controls.Add(this.nudReturnQuantity);
            this.Controls.Add(this.lblReturnReason);
            this.Controls.Add(this.lblReturnQuantity);
            this.Controls.Add(this.dgvReturnItems);
            this.Controls.Add(this.btnFindOrder);
            this.Controls.Add(this.txtOrderID);
            this.Controls.Add(this.lblOrderID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReturns";
            this.Text = "Returns and Refunds";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReturnItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReturnQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOrderID;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.Button btnFindOrder;
        private System.Windows.Forms.DataGridView dgvReturnItems;
        private System.Windows.Forms.Label lblReturnQuantity;
        private System.Windows.Forms.Label lblReturnReason;
        private System.Windows.Forms.NumericUpDown nudReturnQuantity;
        private System.Windows.Forms.TextBox txtReturnReason;
        private System.Windows.Forms.CheckBox chkRestock;
        private System.Windows.Forms.Button btnProcessReturn;
        private System.Windows.Forms.Label lblRefundTotal;
    }
}