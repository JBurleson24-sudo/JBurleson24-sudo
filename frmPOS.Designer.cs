namespace BurlesonJLogonView
{
    partial class frmPOS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPOS));
            this.lblTitle = new System.Windows.Forms.Label();
            this.gbCustomer = new System.Windows.Forms.GroupBox();
            this.btnTransactionHistory = new System.Windows.Forms.Button();
            this.btnLookupCustomer = new System.Windows.Forms.Button();
            this.lblSelectedCustomer = new System.Windows.Forms.Label();
            this.gbProducts = new System.Windows.Forms.GroupBox();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQunatity = new System.Windows.Forms.Label();
            this.lblProductPrice = new System.Windows.Forms.Label();
            this.lblProductDescription = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.picProduct = new System.Windows.Forms.PictureBox();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.btnProductClear = new System.Windows.Forms.Button();
            this.btnProductSearch = new System.Windows.Forms.Button();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtProductSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.gbCart = new System.Windows.Forms.GroupBox();
            this.btnAvailablePromo = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnApplyPromo = new System.Windows.Forms.Button();
            this.txtPromoCode = new System.Windows.Forms.TextBox();
            this.lblPromo = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.gbCustomer.SuspendLayout();
            this.gbProducts.SuspendLayout();
            this.gbDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.gbCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(396, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(422, 45);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Manager Point of Sale";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbCustomer
            // 
            this.gbCustomer.Controls.Add(this.btnTransactionHistory);
            this.gbCustomer.Controls.Add(this.btnLookupCustomer);
            this.gbCustomer.Controls.Add(this.lblSelectedCustomer);
            this.gbCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCustomer.Location = new System.Drawing.Point(2, 45);
            this.gbCustomer.Name = "gbCustomer";
            this.gbCustomer.Size = new System.Drawing.Size(1232, 142);
            this.gbCustomer.TabIndex = 2;
            this.gbCustomer.TabStop = false;
            this.gbCustomer.Text = "Customer";
            // 
            // btnTransactionHistory
            // 
            this.btnTransactionHistory.Location = new System.Drawing.Point(551, 91);
            this.btnTransactionHistory.Name = "btnTransactionHistory";
            this.btnTransactionHistory.Size = new System.Drawing.Size(451, 50);
            this.btnTransactionHistory.TabIndex = 3;
            this.btnTransactionHistory.Text = "Transaction History";
            this.btnTransactionHistory.UseVisualStyleBackColor = true;
            this.btnTransactionHistory.Click += new System.EventHandler(this.btnTransactionHistory_Click);
            // 
            // btnLookupCustomer
            // 
            this.btnLookupCustomer.Location = new System.Drawing.Point(58, 91);
            this.btnLookupCustomer.Name = "btnLookupCustomer";
            this.btnLookupCustomer.Size = new System.Drawing.Size(451, 50);
            this.btnLookupCustomer.TabIndex = 2;
            this.btnLookupCustomer.Text = "Lookup Customer";
            this.btnLookupCustomer.UseVisualStyleBackColor = true;
            this.btnLookupCustomer.Click += new System.EventHandler(this.btnLookupCustomer_Click);
            // 
            // lblSelectedCustomer
            // 
            this.lblSelectedCustomer.AutoSize = true;
            this.lblSelectedCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedCustomer.Location = new System.Drawing.Point(6, 52);
            this.lblSelectedCustomer.Name = "lblSelectedCustomer";
            this.lblSelectedCustomer.Size = new System.Drawing.Size(257, 25);
            this.lblSelectedCustomer.TabIndex = 1;
            this.lblSelectedCustomer.Text = "Selected Customer: None";
            // 
            // gbProducts
            // 
            this.gbProducts.Controls.Add(this.gbDetails);
            this.gbProducts.Controls.Add(this.dgvProducts);
            this.gbProducts.Controls.Add(this.btnProductClear);
            this.gbProducts.Controls.Add(this.btnProductSearch);
            this.gbProducts.Controls.Add(this.cboCategory);
            this.gbProducts.Controls.Add(this.lblCategory);
            this.gbProducts.Controls.Add(this.txtProductSearch);
            this.gbProducts.Controls.Add(this.lblSearch);
            this.gbProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbProducts.Location = new System.Drawing.Point(3, 190);
            this.gbProducts.Name = "gbProducts";
            this.gbProducts.Size = new System.Drawing.Size(1231, 287);
            this.gbProducts.TabIndex = 3;
            this.gbProducts.TabStop = false;
            this.gbProducts.Text = "Products";
            // 
            // gbDetails
            // 
            this.gbDetails.Controls.Add(this.btnAddToCart);
            this.gbDetails.Controls.Add(this.nudQuantity);
            this.gbDetails.Controls.Add(this.lblQunatity);
            this.gbDetails.Controls.Add(this.lblProductPrice);
            this.gbDetails.Controls.Add(this.lblProductDescription);
            this.gbDetails.Controls.Add(this.lblProductName);
            this.gbDetails.Controls.Add(this.picProduct);
            this.gbDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDetails.Location = new System.Drawing.Point(555, 60);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(675, 227);
            this.gbDetails.TabIndex = 11;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Product Details";
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Location = new System.Drawing.Point(369, 168);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(139, 43);
            this.btnAddToCart.TabIndex = 12;
            this.btnAddToCart.Text = "Add to Cart";
            this.btnAddToCart.UseVisualStyleBackColor = true;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(97, 189);
            this.nudQuantity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(223, 22);
            this.nudQuantity.TabIndex = 5;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQunatity
            // 
            this.lblQunatity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQunatity.Location = new System.Drawing.Point(8, 188);
            this.lblQunatity.Name = "lblQunatity";
            this.lblQunatity.Size = new System.Drawing.Size(76, 33);
            this.lblQunatity.TabIndex = 4;
            this.lblQunatity.Text = "Quantity:";
            // 
            // lblProductPrice
            // 
            this.lblProductPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductPrice.Location = new System.Drawing.Point(259, 140);
            this.lblProductPrice.Name = "lblProductPrice";
            this.lblProductPrice.Size = new System.Drawing.Size(312, 33);
            this.lblProductPrice.TabIndex = 3;
            this.lblProductPrice.Text = "Price:";
            // 
            // lblProductDescription
            // 
            this.lblProductDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductDescription.Location = new System.Drawing.Point(259, 59);
            this.lblProductDescription.Name = "lblProductDescription";
            this.lblProductDescription.Size = new System.Drawing.Size(387, 81);
            this.lblProductDescription.TabIndex = 2;
            this.lblProductDescription.Text = "Description:";
            // 
            // lblProductName
            // 
            this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Location = new System.Drawing.Point(259, 26);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(312, 33);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "Product Name:";
            // 
            // picProduct
            // 
            this.picProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProduct.Location = new System.Drawing.Point(12, 23);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(217, 150);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProduct.TabIndex = 0;
            this.picProduct.TabStop = false;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(15, 71);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(500, 162);
            this.dgvProducts.TabIndex = 10;
            this.dgvProducts.SelectionChanged += new System.EventHandler(this.dgvProducts_SelectionChanged);
            // 
            // btnProductClear
            // 
            this.btnProductClear.Location = new System.Drawing.Point(1080, 17);
            this.btnProductClear.Name = "btnProductClear";
            this.btnProductClear.Size = new System.Drawing.Size(139, 43);
            this.btnProductClear.TabIndex = 9;
            this.btnProductClear.Text = "Clear";
            this.btnProductClear.UseVisualStyleBackColor = true;
            this.btnProductClear.Click += new System.EventHandler(this.btnProductClear_Click);
            // 
            // btnProductSearch
            // 
            this.btnProductSearch.Location = new System.Drawing.Point(924, 17);
            this.btnProductSearch.Name = "btnProductSearch";
            this.btnProductSearch.Size = new System.Drawing.Size(139, 43);
            this.btnProductSearch.TabIndex = 8;
            this.btnProductSearch.Text = "Search";
            this.btnProductSearch.UseVisualStyleBackColor = true;
            this.btnProductSearch.Click += new System.EventHandler(this.btnProductSearch_Click);
            // 
            // cboCategory
            // 
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(709, 23);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(188, 32);
            this.cboCategory.TabIndex = 7;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            this.cboCategory.SelectionChangeCommitted += new System.EventHandler(this.cboCategory_SelectionChangeCommitted);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(598, 27);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(105, 25);
            this.lblCategory.TabIndex = 6;
            this.lblCategory.Text = "Category:";
            // 
            // txtProductSearch
            // 
            this.txtProductSearch.Location = new System.Drawing.Point(108, 21);
            this.txtProductSearch.Name = "txtProductSearch";
            this.txtProductSearch.Size = new System.Drawing.Size(407, 29);
            this.txtProductSearch.TabIndex = 5;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(5, 25);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(86, 25);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Search:";
            // 
            // gbCart
            // 
            this.gbCart.Controls.Add(this.btnAvailablePromo);
            this.gbCart.Controls.Add(this.btnClose);
            this.gbCart.Controls.Add(this.btnHelp);
            this.gbCart.Controls.Add(this.btnCheckout);
            this.gbCart.Controls.Add(this.lblTotal);
            this.gbCart.Controls.Add(this.lblTax);
            this.gbCart.Controls.Add(this.lblDiscount);
            this.gbCart.Controls.Add(this.lblSubtotal);
            this.gbCart.Controls.Add(this.btnRemoveItem);
            this.gbCart.Controls.Add(this.btnApplyPromo);
            this.gbCart.Controls.Add(this.txtPromoCode);
            this.gbCart.Controls.Add(this.lblPromo);
            this.gbCart.Controls.Add(this.dgvCart);
            this.gbCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCart.Location = new System.Drawing.Point(5, 474);
            this.gbCart.Name = "gbCart";
            this.gbCart.Size = new System.Drawing.Size(1229, 242);
            this.gbCart.TabIndex = 4;
            this.gbCart.TabStop = false;
            this.gbCart.Text = "Cart";
            this.gbCart.Enter += new System.EventHandler(this.gbCart_Enter);
            // 
            // btnAvailablePromo
            // 
            this.btnAvailablePromo.Location = new System.Drawing.Point(404, 100);
            this.btnAvailablePromo.Name = "btnAvailablePromo";
            this.btnAvailablePromo.Size = new System.Drawing.Size(139, 43);
            this.btnAvailablePromo.TabIndex = 22;
            this.btnAvailablePromo.Text = "Available Promos";
            this.btnAvailablePromo.UseVisualStyleBackColor = true;
            this.btnAvailablePromo.Click += new System.EventHandler(this.btnAvailablePromo_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(691, 182);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(139, 43);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(498, 182);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(139, 43);
            this.btnHelp.TabIndex = 20;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnCheckout
            // 
            this.btnCheckout.Location = new System.Drawing.Point(298, 182);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(139, 43);
            this.btnCheckout.TabIndex = 19;
            this.btnCheckout.Text = "Checkout";
            this.btnCheckout.UseVisualStyleBackColor = true;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(918, 129);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(303, 33);
            this.lblTotal.TabIndex = 18;
            this.lblTotal.Text = "Total:";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // lblTax
            // 
            this.lblTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTax.Location = new System.Drawing.Point(616, 129);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(303, 33);
            this.lblTax.TabIndex = 17;
            this.lblTax.Text = "Tax:";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.Location = new System.Drawing.Point(918, 96);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(303, 33);
            this.lblDiscount.TabIndex = 16;
            this.lblDiscount.Text = "Discount:";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotal.Location = new System.Drawing.Point(616, 96);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(303, 33);
            this.lblSubtotal.TabIndex = 13;
            this.lblSubtotal.Text = "SubTotal:";
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(8, 146);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(139, 43);
            this.btnRemoveItem.TabIndex = 15;
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // btnApplyPromo
            // 
            this.btnApplyPromo.Location = new System.Drawing.Point(259, 99);
            this.btnApplyPromo.Name = "btnApplyPromo";
            this.btnApplyPromo.Size = new System.Drawing.Size(139, 43);
            this.btnApplyPromo.TabIndex = 13;
            this.btnApplyPromo.Text = "Apply";
            this.btnApplyPromo.UseVisualStyleBackColor = true;
            this.btnApplyPromo.Click += new System.EventHandler(this.btnApplyPromo_Click);
            // 
            // txtPromoCode
            // 
            this.txtPromoCode.Location = new System.Drawing.Point(72, 110);
            this.txtPromoCode.Name = "txtPromoCode";
            this.txtPromoCode.Size = new System.Drawing.Size(181, 26);
            this.txtPromoCode.TabIndex = 14;
            // 
            // lblPromo
            // 
            this.lblPromo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPromo.Location = new System.Drawing.Point(4, 110);
            this.lblPromo.Name = "lblPromo";
            this.lblPromo.Size = new System.Drawing.Size(62, 33);
            this.lblPromo.TabIndex = 13;
            this.lblPromo.Text = "Promo:";
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Location = new System.Drawing.Point(3, 22);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(1220, 71);
            this.dgvCart.TabIndex = 0;
            // 
            // frmPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 711);
            this.Controls.Add(this.gbCart);
            this.Controls.Add(this.gbProducts);
            this.Controls.Add(this.gbCustomer);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1250, 750);
            this.Name = "frmPOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manager Point of Sale";
            this.Load += new System.EventHandler(this.frmPOS_Load);
            this.gbCustomer.ResumeLayout(false);
            this.gbCustomer.PerformLayout();
            this.gbProducts.ResumeLayout(false);
            this.gbProducts.PerformLayout();
            this.gbDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.gbCart.ResumeLayout(false);
            this.gbCart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox gbCustomer;
        private System.Windows.Forms.Label lblSelectedCustomer;
        private System.Windows.Forms.Button btnTransactionHistory;
        private System.Windows.Forms.Button btnLookupCustomer;
        private System.Windows.Forms.GroupBox gbProducts;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Button btnProductClear;
        private System.Windows.Forms.Button btnProductSearch;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.PictureBox picProduct;
        private System.Windows.Forms.Label lblQunatity;
        private System.Windows.Forms.Label lblProductPrice;
        private System.Windows.Forms.Label lblProductDescription;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.GroupBox gbCart;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnApplyPromo;
        private System.Windows.Forms.TextBox txtPromoCode;
        private System.Windows.Forms.Label lblPromo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnAvailablePromo;
    }
}