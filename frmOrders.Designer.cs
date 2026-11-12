namespace GroceryOrderingManagementSystem
{
    partial class frmOrders
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrders));
            lblTitle = new Label();
            lblOrderingStatus = new Label();
            lblRules = new Label();
            grpOrderDetails = new GroupBox();
            lblMemberHelp = new Label();
            lblOrderDate = new Label();
            dtpOrderDate = new DateTimePicker();
            lblMember = new Label();
            cboMember = new ComboBox();
            lblItem = new Label();
            cboItem = new ComboBox();
            lblQuantity = new Label();
            nudQuantity = new NumericUpDown();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblLineTotal = new Label();
            txtLineTotal = new TextBox();
            btnAddToOrder = new Button();
            grpOrderList = new GroupBox();
            lblOrderMode = new Label();
            dgvOrder = new DataGridView();
            OrderID = new DataGridViewTextBoxColumn();
            ItemID = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            Total = new DataGridViewTextBoxColumn();
            lblGrandTotal = new Label();
            btnRemove = new Button();
            btnClear = new Button();
            btnLoadOrder = new Button();
            btnSave = new Button();
            btnClose = new Button();
            pnlActions = new Panel();
            errorProvider1 = new ErrorProvider(components);
            grpOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            grpOrderList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrder).BeginInit();
            pnlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top;
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.WhiteSmoke;
            lblTitle.Font = new Font("Rockwell", 27.75F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkGreen;
            lblTitle.Location = new Point(475, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(14, 6, 14, 6);
            lblTitle.Size = new Size(701, 69);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CREATE GROCERY ORDER";
            // 
            // lblOrderingStatus
            // 
            lblOrderingStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblOrderingStatus.BackColor = Color.WhiteSmoke;
            lblOrderingStatus.Font = new Font("Rockwell", 12F, FontStyle.Bold);
            lblOrderingStatus.Location = new Point(30, 98);
            lblOrderingStatus.Name = "lblOrderingStatus";
            lblOrderingStatus.Padding = new Padding(8);
            lblOrderingStatus.Size = new Size(1380, 42);
            lblOrderingStatus.TabIndex = 1;
            lblOrderingStatus.Text = "Ordering status";
            lblOrderingStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblRules
            // 
            lblRules.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblRules.BackColor = Color.WhiteSmoke;
            lblRules.Font = new Font("Rockwell", 10.5F, FontStyle.Bold);
            lblRules.ForeColor = Color.FromArgb(64, 64, 64);
            lblRules.Location = new Point(30, 142);
            lblRules.Name = "lblRules";
            lblRules.Padding = new Padding(8);
            lblRules.Size = new Size(1380, 40);
            lblRules.TabIndex = 2;
            lblRules.Text = "Rules: order Monday–Thursday before 18:00 • maximum 12 of the same item • unavailable items cannot be ordered";
            lblRules.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpOrderDetails
            // 
            grpOrderDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpOrderDetails.BackColor = Color.WhiteSmoke;
            grpOrderDetails.Controls.Add(lblMemberHelp);
            grpOrderDetails.Controls.Add(lblOrderDate);
            grpOrderDetails.Controls.Add(dtpOrderDate);
            grpOrderDetails.Controls.Add(lblMember);
            grpOrderDetails.Controls.Add(cboMember);
            grpOrderDetails.Controls.Add(lblItem);
            grpOrderDetails.Controls.Add(cboItem);
            grpOrderDetails.Controls.Add(lblQuantity);
            grpOrderDetails.Controls.Add(nudQuantity);
            grpOrderDetails.Controls.Add(lblPrice);
            grpOrderDetails.Controls.Add(txtPrice);
            grpOrderDetails.Controls.Add(lblLineTotal);
            grpOrderDetails.Controls.Add(txtLineTotal);
            grpOrderDetails.Controls.Add(btnAddToOrder);
            grpOrderDetails.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            grpOrderDetails.Location = new Point(30, 196);
            grpOrderDetails.Name = "grpOrderDetails";
            grpOrderDetails.Padding = new Padding(18);
            grpOrderDetails.Size = new Size(410, 520);
            grpOrderDetails.TabIndex = 3;
            grpOrderDetails.TabStop = false;
            grpOrderDetails.Text = "Order details";
            // 
            // lblMemberHelp
            // 
            lblMemberHelp.Font = new Font("Rockwell", 9F, FontStyle.Italic);
            lblMemberHelp.ForeColor = Color.DimGray;
            lblMemberHelp.Location = new Point(24, 114);
            lblMemberHelp.Name = "lblMemberHelp";
            lblMemberHelp.Size = new Size(348, 22);
            lblMemberHelp.TabIndex = 2;
            lblMemberHelp.Text = "Member information";
            // 
            // lblOrderDate
            // 
            lblOrderDate.AutoSize = true;
            lblOrderDate.Location = new Point(24, 145);
            lblOrderDate.Name = "lblOrderDate";
            lblOrderDate.Size = new Size(133, 28);
            lblOrderDate.TabIndex = 3;
            lblOrderDate.Text = "Order date";
            // 
            // dtpOrderDate
            // 
            dtpOrderDate.Enabled = false;
            dtpOrderDate.Location = new Point(24, 176);
            dtpOrderDate.Name = "dtpOrderDate";
            dtpOrderDate.Size = new Size(348, 35);
            dtpOrderDate.TabIndex = 4;
            // 
            // lblMember
            // 
            lblMember.AutoSize = true;
            lblMember.Location = new Point(24, 46);
            lblMember.Name = "lblMember";
            lblMember.Size = new Size(109, 28);
            lblMember.TabIndex = 0;
            lblMember.Text = "Member";
            // 
            // cboMember
            // 
            cboMember.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMember.FormattingEnabled = true;
            cboMember.Location = new Point(24, 77);
            cboMember.Name = "cboMember";
            cboMember.Size = new Size(348, 36);
            cboMember.TabIndex = 1;
            cboMember.SelectedIndexChanged += cboMember_SelectedIndexChanged;
            // 
            // lblItem
            // 
            lblItem.AutoSize = true;
            lblItem.Location = new Point(24, 221);
            lblItem.Name = "lblItem";
            lblItem.Size = new Size(162, 28);
            lblItem.TabIndex = 5;
            lblItem.Text = "Grocery item";
            // 
            // cboItem
            // 
            cboItem.DropDownStyle = ComboBoxStyle.DropDownList;
            cboItem.FormattingEnabled = true;
            cboItem.Location = new Point(24, 252);
            cboItem.Name = "cboItem";
            cboItem.Size = new Size(348, 36);
            cboItem.TabIndex = 6;
            cboItem.SelectedIndexChanged += cboItem_SelectedIndexChanged;
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(24, 300);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(184, 28);
            lblQuantity.TabIndex = 7;
            lblQuantity.Text = "Quantity (1–12)";
            // 
            // nudQuantity
            // 
            nudQuantity.Location = new Point(24, 331);
            nudQuantity.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(110, 35);
            nudQuantity.TabIndex = 8;
            nudQuantity.TextAlign = HorizontalAlignment.Center;
            nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudQuantity.ValueChanged += nudQuantity_ValueChanged;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(158, 300);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(71, 28);
            lblPrice.TabIndex = 9;
            lblPrice.Text = "Price";
            // 
            // txtPrice
            // 
            txtPrice.BackColor = Color.White;
            txtPrice.Location = new Point(158, 331);
            txtPrice.Name = "txtPrice";
            txtPrice.ReadOnly = true;
            txtPrice.Size = new Size(102, 35);
            txtPrice.TabIndex = 10;
            txtPrice.TabStop = false;
            txtPrice.TextAlign = HorizontalAlignment.Right;
            // 
            // lblLineTotal
            // 
            lblLineTotal.AutoSize = true;
            lblLineTotal.Location = new Point(278, 300);
            lblLineTotal.Name = "lblLineTotal";
            lblLineTotal.Size = new Size(70, 28);
            lblLineTotal.TabIndex = 11;
            lblLineTotal.Text = "Total";
            // 
            // txtLineTotal
            // 
            txtLineTotal.BackColor = Color.White;
            txtLineTotal.Location = new Point(278, 331);
            txtLineTotal.Name = "txtLineTotal";
            txtLineTotal.ReadOnly = true;
            txtLineTotal.Size = new Size(94, 35);
            txtLineTotal.TabIndex = 12;
            txtLineTotal.TabStop = false;
            txtLineTotal.TextAlign = HorizontalAlignment.Right;
            // 
            // btnAddToOrder
            // 
            btnAddToOrder.BackColor = Color.LimeGreen;
            btnAddToOrder.FlatStyle = FlatStyle.Flat;
            btnAddToOrder.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            btnAddToOrder.Location = new Point(24, 401);
            btnAddToOrder.Name = "btnAddToOrder";
            btnAddToOrder.Size = new Size(348, 62);
            btnAddToOrder.TabIndex = 13;
            btnAddToOrder.Text = "ADD ITEM TO ORDER";
            btnAddToOrder.UseVisualStyleBackColor = false;
            btnAddToOrder.Click += btnAddToOrder_Click;
            // 
            // grpOrderList
            // 
            grpOrderList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpOrderList.BackColor = Color.WhiteSmoke;
            grpOrderList.Controls.Add(lblOrderMode);
            grpOrderList.Controls.Add(dgvOrder);
            grpOrderList.Controls.Add(lblGrandTotal);
            grpOrderList.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            grpOrderList.Location = new Point(466, 196);
            grpOrderList.Name = "grpOrderList";
            grpOrderList.Padding = new Padding(16);
            grpOrderList.Size = new Size(944, 520);
            grpOrderList.TabIndex = 4;
            grpOrderList.TabStop = false;
            grpOrderList.Text = "My grocery list";
            // 
            // lblOrderMode
            // 
            lblOrderMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblOrderMode.Font = new Font("Rockwell", 10F, FontStyle.Bold);
            lblOrderMode.ForeColor = Color.DarkBlue;
            lblOrderMode.Location = new Point(19, 38);
            lblOrderMode.Name = "lblOrderMode";
            lblOrderMode.Size = new Size(906, 28);
            lblOrderMode.TabIndex = 0;
            lblOrderMode.Text = "Select a member to load an order.";
            lblOrderMode.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvOrder
            // 
            dgvOrder.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvOrder.BackgroundColor = Color.White;
            dgvOrder.ColumnHeadersHeight = 42;
            dgvOrder.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvOrder.Columns.AddRange(new DataGridViewColumn[] { OrderID, ItemID, Description, Price, Quantity, Total });
            dgvOrder.Location = new Point(19, 70);
            dgvOrder.Name = "dgvOrder";
            dgvOrder.RowHeadersWidth = 51;
            dgvOrder.RowTemplate.Height = 34;
            dgvOrder.Size = new Size(906, 379);
            dgvOrder.TabIndex = 1;
            dgvOrder.KeyDown += dgvOrder_KeyDown;
            // 
            // OrderID
            // 
            OrderID.HeaderText = "Order ID";
            OrderID.MinimumWidth = 6;
            OrderID.Name = "OrderID";
            OrderID.Visible = false;
            OrderID.Width = 125;
            // 
            // ItemID
            // 
            ItemID.FillWeight = 70F;
            ItemID.HeaderText = "Item ID";
            ItemID.MinimumWidth = 6;
            ItemID.Name = "ItemID";
            ItemID.Width = 125;
            // 
            // Description
            // 
            Description.FillWeight = 180F;
            Description.HeaderText = "Description";
            Description.MinimumWidth = 6;
            Description.Name = "Description";
            Description.Width = 125;
            // 
            // Price
            // 
            Price.FillWeight = 80F;
            Price.HeaderText = "Price";
            Price.MinimumWidth = 6;
            Price.Name = "Price";
            Price.Width = 125;
            // 
            // Quantity
            // 
            Quantity.FillWeight = 70F;
            Quantity.HeaderText = "Quantity";
            Quantity.MinimumWidth = 6;
            Quantity.Name = "Quantity";
            Quantity.Width = 125;
            // 
            // Total
            // 
            Total.FillWeight = 90F;
            Total.HeaderText = "Total";
            Total.MinimumWidth = 6;
            Total.Name = "Total";
            Total.Width = 125;
            // 
            // lblGrandTotal
            // 
            lblGrandTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblGrandTotal.Font = new Font("Rockwell", 15.75F, FontStyle.Bold);
            lblGrandTotal.ForeColor = Color.DarkGreen;
            lblGrandTotal.Location = new Point(563, 462);
            lblGrandTotal.Name = "lblGrandTotal";
            lblGrandTotal.Size = new Size(362, 38);
            lblGrandTotal.TabIndex = 2;
            lblGrandTotal.Text = "Grand total: R0,00";
            lblGrandTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.Gold;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Location = new Point(20, 10);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(180, 55);
            btnRemove.TabIndex = 5;
            btnRemove.Text = "REMOVE ITEM";
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.LightGray;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Location = new Point(214, 10);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(180, 55);
            btnClear.TabIndex = 6;
            btnClear.Text = "CLEAR ORDER";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnLoadOrder
            // 
            btnLoadOrder.BackColor = Color.LightSkyBlue;
            btnLoadOrder.FlatStyle = FlatStyle.Flat;
            btnLoadOrder.Location = new Point(408, 10);
            btnLoadOrder.Name = "btnLoadOrder";
            btnLoadOrder.Size = new Size(168, 55);
            btnLoadOrder.TabIndex = 7;
            btnLoadOrder.Text = "RELOAD ORDER";
            btnLoadOrder.UseVisualStyleBackColor = false;
            btnLoadOrder.Click += btnLoadOrder_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.BackColor = Color.LimeGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Location = new Point(1036, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(180, 55);
            btnSave.TabIndex = 8;
            btnSave.Text = "SAVE / UPDATE";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.IndianRed;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Location = new Point(1230, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(180, 55);
            btnClose.TabIndex = 9;
            btnClose.Text = "CLOSE";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // pnlActions
            // 
            pnlActions.BackColor = Color.WhiteSmoke;
            pnlActions.Controls.Add(btnRemove);
            pnlActions.Controls.Add(btnClear);
            pnlActions.Controls.Add(btnLoadOrder);
            pnlActions.Controls.Add(btnSave);
            pnlActions.Controls.Add(btnClose);
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Location = new Point(0, 744);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(1440, 76);
            pnlActions.TabIndex = 5;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // frmOrders
            // 
            AcceptButton = btnAddToOrder;
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            CancelButton = btnClose;
            ClientSize = new Size(1440, 820);
            Controls.Add(pnlActions);
            Controls.Add(grpOrderList);
            Controls.Add(grpOrderDetails);
            Controls.Add(lblRules);
            Controls.Add(lblOrderingStatus);
            Controls.Add(lblTitle);
            Font = new Font("Rockwell", 11F, FontStyle.Bold);
            MinimumSize = new Size(1180, 700);
            Name = "frmOrders";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Member Grocery Ordering";
            WindowState = FormWindowState.Maximized;
            FormClosing += frmOrders_FormClosing;
            Load += frmOrders_Load;
            grpOrderDetails.ResumeLayout(false);
            grpOrderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            grpOrderList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrder).EndInit();
            pnlActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblOrderingStatus;
        private Label lblRules;
        private GroupBox grpOrderDetails;
        private Label lblMember;
        private ComboBox cboMember;
        private Label lblMemberHelp;
        private Label lblOrderDate;
        private DateTimePicker dtpOrderDate;
        private Label lblItem;
        private ComboBox cboItem;
        private Label lblQuantity;
        private NumericUpDown nudQuantity;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblLineTotal;
        private TextBox txtLineTotal;
        private Button btnAddToOrder;
        private GroupBox grpOrderList;
        private Label lblOrderMode;
        private DataGridView dgvOrder;
        private DataGridViewTextBoxColumn OrderID;
        private DataGridViewTextBoxColumn ItemID;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Total;
        private Label lblGrandTotal;
        private Button btnRemove;
        private Button btnClear;
        private Button btnLoadOrder;
        private Button btnSave;
        private Button btnClose;
        private Panel pnlActions;
        private ErrorProvider errorProvider1;
    }
}
