namespace GroceryOrderingManagementSystem
{
    partial class frmGroceryItems
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlHeader = new Panel();
            lblTitle = new Label();
            errorProvider1 = new ErrorProvider(components);
            pnlContent = new Panel();
            splitMain = new SplitContainer();
            grpItemDetails = new GroupBox();
            detailsLayout = new TableLayoutPanel();
            lblItemID = new Label();
            txtItemID = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblAvailable = new Label();
            cboAvailable = new ComboBox();
            lblImagePath = new Label();
            imagePathLayout = new TableLayoutPanel();
            txtImage = new TextBox();
            btnBrowse = new Button();
            picItem = new PictureBox();
            lblImageState = new Label();
            lblStatus = new Label();
            lblSelectionHint = new Label();
            actionLayout = new TableLayoutPanel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnClear = new Button();
            btnClose = new Button();
            pnlList = new Panel();
            dgvItems = new DataGridView();
            pnlSearch = new Panel();
            btnRefresh = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            lblRecordCount = new Label();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            grpItemDetails.SuspendLayout();
            detailsLayout.SuspendLayout();
            imagePathLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picItem).BeginInit();
            actionLayout.SuspendLayout();
            pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            pnlSearch.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.Transparent;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(24, 12, 24, 10);
            pnlHeader.Size = new Size(1653, 79);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Rockwell Condensed", 25.8F, FontStyle.Bold | FontStyle.Underline);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(24, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1605, 57);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "MAINTAIN GROCERY ITEMS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(236, 244, 238);
            pnlContent.Controls.Add(splitMain);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 79);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(18);
            pnlContent.Size = new Size(1653, 846);
            pnlContent.TabIndex = 1;
            // 
            // splitMain
            // 
            splitMain.BackColor = Color.FromArgb(236, 244, 238);
            splitMain.Dock = DockStyle.Fill;
            splitMain.FixedPanel = FixedPanel.Panel1;
            splitMain.Location = new Point(18, 18);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(grpItemDetails);
            splitMain.Panel1.Padding = new Padding(0, 0, 9, 0);
            splitMain.Panel1MinSize = 430;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(pnlList);
            splitMain.Panel2.Padding = new Padding(9, 0, 0, 0);
            splitMain.Panel2MinSize = 520;
            splitMain.Size = new Size(1617, 810);
            splitMain.SplitterDistance = 480;
            splitMain.SplitterWidth = 2;
            splitMain.TabIndex = 0;
            // 
            // grpItemDetails
            // 
            grpItemDetails.BackColor = Color.White;
            grpItemDetails.Controls.Add(detailsLayout);
            grpItemDetails.Dock = DockStyle.Fill;
            grpItemDetails.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            grpItemDetails.ForeColor = Color.FromArgb(35, 35, 35);
            grpItemDetails.Location = new Point(0, 0);
            grpItemDetails.Name = "grpItemDetails";
            grpItemDetails.Padding = new Padding(16, 14, 16, 16);
            grpItemDetails.Size = new Size(471, 810);
            grpItemDetails.TabIndex = 0;
            grpItemDetails.TabStop = false;
            grpItemDetails.Text = "Grocery item details";
            // 
            // detailsLayout
            // 
            detailsLayout.ColumnCount = 1;
            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailsLayout.Controls.Add(lblItemID, 0, 0);
            detailsLayout.Controls.Add(txtItemID, 0, 1);
            detailsLayout.Controls.Add(lblDescription, 0, 2);
            detailsLayout.Controls.Add(txtDescription, 0, 3);
            detailsLayout.Controls.Add(lblPrice, 0, 4);
            detailsLayout.Controls.Add(txtPrice, 0, 5);
            detailsLayout.Controls.Add(lblAvailable, 0, 6);
            detailsLayout.Controls.Add(cboAvailable, 0, 7);
            detailsLayout.Controls.Add(lblImagePath, 0, 8);
            detailsLayout.Controls.Add(imagePathLayout, 0, 9);
            detailsLayout.Controls.Add(picItem, 0, 10);
            detailsLayout.Controls.Add(lblImageState, 0, 11);
            detailsLayout.Controls.Add(lblStatus, 0, 12);
            detailsLayout.Controls.Add(lblSelectionHint, 0, 13);
            detailsLayout.Controls.Add(actionLayout, 0, 14);
            detailsLayout.Dock = DockStyle.Fill;
            detailsLayout.Location = new Point(16, 42);
            detailsLayout.Name = "detailsLayout";
            detailsLayout.RowCount = 15;
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detailsLayout.Size = new Size(439, 752);
            detailsLayout.TabIndex = 0;
            // 
            // lblItemID
            // 
            lblItemID.Dock = DockStyle.Fill;
            lblItemID.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            lblItemID.Location = new Point(3, 0);
            lblItemID.Name = "lblItemID";
            lblItemID.Size = new Size(433, 25);
            lblItemID.TabIndex = 0;
            lblItemID.Text = "Item ID *";
            lblItemID.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtItemID
            // 
            txtItemID.CharacterCasing = CharacterCasing.Upper;
            txtItemID.Dock = DockStyle.Fill;
            txtItemID.Font = new Font("Rockwell", 13.8F);
            txtItemID.Location = new Point(0, 27);
            txtItemID.Margin = new Padding(0, 2, 18, 5);
            txtItemID.MaxLength = 20;
            txtItemID.Name = "txtItemID";
            txtItemID.Size = new Size(421, 35);
            txtItemID.TabIndex = 0;
            // 
            // lblDescription
            // 
            lblDescription.Dock = DockStyle.Fill;
            lblDescription.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            lblDescription.Location = new Point(3, 67);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(433, 25);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "Description *";
            lblDescription.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Font = new Font("Rockwell", 13.8F);
            txtDescription.Location = new Point(0, 94);
            txtDescription.Margin = new Padding(0, 2, 18, 5);
            txtDescription.MaxLength = 150;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(421, 35);
            txtDescription.TabIndex = 1;
            txtDescription.KeyPress += txtDescription_KeyPress;
            // 
            // lblPrice
            // 
            lblPrice.Dock = DockStyle.Fill;
            lblPrice.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            lblPrice.Location = new Point(3, 134);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(433, 25);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "Price (R) *";
            lblPrice.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtPrice
            // 
            txtPrice.Dock = DockStyle.Fill;
            txtPrice.Font = new Font("Rockwell", 13.8F);
            txtPrice.Location = new Point(0, 161);
            txtPrice.Margin = new Padding(0, 2, 18, 5);
            txtPrice.MaxLength = 12;
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(421, 35);
            txtPrice.TabIndex = 2;
            txtPrice.KeyPress += txtPrice_KeyPress;
            txtPrice.Leave += txtPrice_Leave;
            // 
            // lblAvailable
            // 
            lblAvailable.Dock = DockStyle.Fill;
            lblAvailable.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            lblAvailable.Location = new Point(3, 201);
            lblAvailable.Name = "lblAvailable";
            lblAvailable.Size = new Size(433, 25);
            lblAvailable.TabIndex = 3;
            lblAvailable.Text = "Available (Y/N) *";
            lblAvailable.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cboAvailable
            // 
            cboAvailable.Dock = DockStyle.Fill;
            cboAvailable.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAvailable.Font = new Font("Rockwell", 13.8F);
            cboAvailable.Location = new Point(0, 228);
            cboAvailable.Margin = new Padding(0, 2, 18, 5);
            cboAvailable.Name = "cboAvailable";
            cboAvailable.Size = new Size(421, 34);
            cboAvailable.TabIndex = 3;
            cboAvailable.SelectedIndexChanged += cboAvailable_SelectedIndexChanged;
            // 
            // lblImagePath
            // 
            lblImagePath.Dock = DockStyle.Fill;
            lblImagePath.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            lblImagePath.Location = new Point(3, 268);
            lblImagePath.Name = "lblImagePath";
            lblImagePath.Size = new Size(433, 25);
            lblImagePath.TabIndex = 4;
            lblImagePath.Text = "Link to image";
            lblImagePath.TextAlign = ContentAlignment.BottomLeft;
            // 
            // imagePathLayout
            // 
            imagePathLayout.ColumnCount = 2;
            imagePathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            imagePathLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            imagePathLayout.Controls.Add(txtImage, 0, 0);
            imagePathLayout.Controls.Add(btnBrowse, 1, 0);
            imagePathLayout.Dock = DockStyle.Fill;
            imagePathLayout.Location = new Point(0, 293);
            imagePathLayout.Margin = new Padding(0);
            imagePathLayout.Name = "imagePathLayout";
            imagePathLayout.RowCount = 1;
            imagePathLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            imagePathLayout.Size = new Size(439, 42);
            imagePathLayout.TabIndex = 4;
            // 
            // txtImage
            // 
            txtImage.Dock = DockStyle.Fill;
            txtImage.Font = new Font("Rockwell", 10.5F);
            txtImage.Location = new Point(0, 2);
            txtImage.Margin = new Padding(0, 2, 7, 5);
            txtImage.MaxLength = 500;
            txtImage.Name = "txtImage";
            txtImage.Size = new Size(317, 28);
            txtImage.TabIndex = 0;
            txtImage.TextChanged += txtImage_TextChanged;
            // 
            // btnBrowse
            // 
            btnBrowse.BackColor = Color.FromArgb(90, 90, 90);
            btnBrowse.Dock = DockStyle.Fill;
            btnBrowse.FlatStyle = FlatStyle.Flat;
            btnBrowse.Font = new Font("Rockwell", 8F, FontStyle.Bold);
            btnBrowse.ForeColor = Color.White;
            btnBrowse.Location = new Point(324, 2);
            btnBrowse.Margin = new Padding(0, 2, 18, 5);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(97, 35);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "BROWSE";
            btnBrowse.UseVisualStyleBackColor = false;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // picItem
            // 
            picItem.BackColor = Color.WhiteSmoke;
            picItem.BorderStyle = BorderStyle.FixedSingle;
            picItem.Dock = DockStyle.Fill;
            picItem.Location = new Point(0, 339);
            picItem.Margin = new Padding(0, 4, 18, 4);
            picItem.Name = "picItem";
            picItem.Size = new Size(421, 122);
            picItem.SizeMode = PictureBoxSizeMode.Zoom;
            picItem.TabIndex = 5;
            picItem.TabStop = false;
            // 
            // lblImageState
            // 
            lblImageState.Dock = DockStyle.Fill;
            lblImageState.Font = new Font("Rockwell", 8F, FontStyle.Italic);
            lblImageState.ForeColor = Color.DimGray;
            lblImageState.Location = new Point(3, 465);
            lblImageState.Name = "lblImageState";
            lblImageState.Size = new Size(433, 26);
            lblImageState.TabIndex = 6;
            lblImageState.Text = "No image selected";
            lblImageState.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Font = new Font("Rockwell", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Location = new Point(3, 491);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(433, 34);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "Item is available for ordering.";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSelectionHint
            // 
            lblSelectionHint.Dock = DockStyle.Fill;
            lblSelectionHint.Font = new Font("Rockwell", 8F, FontStyle.Bold);
            lblSelectionHint.ForeColor = Color.DimGray;
            lblSelectionHint.Location = new Point(3, 525);
            lblSelectionHint.Name = "lblSelectionHint";
            lblSelectionHint.Size = new Size(433, 48);
            lblSelectionHint.TabIndex = 8;
            lblSelectionHint.Text = "Select a row in the table to retrieve it for updating.\r\nFields marked * are required.";
            lblSelectionHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // actionLayout
            // 
            actionLayout.ColumnCount = 2;
            actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            actionLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            actionLayout.Controls.Add(btnAdd, 0, 0);
            actionLayout.Controls.Add(btnUpdate, 1, 0);
            actionLayout.Controls.Add(btnClear, 0, 1);
            actionLayout.Controls.Add(btnClose, 1, 1);
            actionLayout.Dock = DockStyle.Top;
            actionLayout.Location = new Point(0, 581);
            actionLayout.Margin = new Padding(0, 8, 0, 0);
            actionLayout.Name = "actionLayout";
            actionLayout.RowCount = 2;
            actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            actionLayout.Size = new Size(439, 118);
            actionLayout.TabIndex = 5;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(46, 184, 46);
            btnAdd.Dock = DockStyle.Fill;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Rockwell", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(0, 3);
            btnAdd.Margin = new Padding(0, 3, 5, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(214, 51);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "ADD ITEM";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(0, 120, 215);
            btnUpdate.Dock = DockStyle.Fill;
            btnUpdate.Enabled = false;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Rockwell", 10F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(224, 3);
            btnUpdate.Margin = new Padding(5, 3, 18, 5);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(197, 51);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "UPDATE";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(255, 193, 7);
            btnClear.Dock = DockStyle.Fill;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Rockwell", 10F, FontStyle.Bold);
            btnClear.ForeColor = Color.Black;
            btnClear.Location = new Point(0, 64);
            btnClear.Margin = new Padding(0, 5, 5, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(214, 51);
            btnClear.TabIndex = 2;
            btnClear.Text = "CLEAR";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(198, 57, 57);
            btnClose.Dock = DockStyle.Fill;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Rockwell", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(224, 64);
            btnClose.Margin = new Padding(5, 5, 18, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(197, 51);
            btnClose.TabIndex = 3;
            btnClose.Text = "CLOSE";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // pnlList
            // 
            pnlList.BackColor = Color.White;
            pnlList.BorderStyle = BorderStyle.FixedSingle;
            pnlList.Controls.Add(dgvItems);
            pnlList.Controls.Add(pnlSearch);
            pnlList.Dock = DockStyle.Fill;
            pnlList.Location = new Point(9, 0);
            pnlList.Name = "pnlList";
            pnlList.Padding = new Padding(14);
            pnlList.Size = new Size(1126, 810);
            pnlList.TabIndex = 0;
            // 
            // dgvItems
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(232, 246, 235);
            dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.BorderStyle = BorderStyle.None;
            dgvItems.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(0, 102, 51);
            dataGridViewCellStyle2.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 102, 51);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvItems.ColumnHeadersHeight = 42;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Rockwell", 13.8F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(187, 222, 251);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvItems.DefaultCellStyle = dataGridViewCellStyle3;
            dgvItems.Dock = DockStyle.Fill;
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.Location = new Point(14, 105);
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersVisible = false;
            dgvItems.RowHeadersWidth = 51;
            dgvItems.RowTemplate.Height = 32;
            dgvItems.Size = new Size(1096, 689);
            dgvItems.TabIndex = 1;
            dgvItems.CellClick += dgvItems_CellClick;
            dgvItems.CellDoubleClick += dgvItems_CellDoubleClick;
            dgvItems.DataBindingComplete += dgvItems_DataBindingComplete;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(btnRefresh);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(lblRecordCount);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(14, 14);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(1096, 91);
            pnlSearch.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.FromArgb(90, 90, 90);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Rockwell", 8F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(917, 27);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(175, 32);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "REFRESH ALL";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Rockwell", 8F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(753, 27);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(155, 32);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "SEARCH";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Font = new Font("Rockwell", 11F);
            txtSearch.Location = new Point(0, 28);
            txtSearch.MaxLength = 120;
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Enter an Item ID, description, Y or N";
            txtSearch.Size = new Size(743, 29);
            txtSearch.TabIndex = 0;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            lblSearch.Location = new Point(0, 1);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(517, 28);
            lblSearch.TabIndex = 3;
            lblSearch.Text = "Search by Item ID, description or availability";
            // 
            // lblRecordCount
            // 
            lblRecordCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRecordCount.Font = new Font("Rockwell", 8F, FontStyle.Italic);
            lblRecordCount.ForeColor = Color.DimGray;
            lblRecordCount.Location = new Point(826, 65);
            lblRecordCount.Name = "lblRecordCount";
            lblRecordCount.Size = new Size(266, 22);
            lblRecordCount.TabIndex = 4;
            lblRecordCount.Text = "0 items shown";
            lblRecordCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmGroceryItems
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(236, 244, 238);
            ClientSize = new Size(1653, 925);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Font = new Font("Rockwell", 13.8F);
            KeyPreview = true;
            MinimumSize = new Size(1100, 720);
            Name = "frmGroceryItems";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Maintain Grocery Items";
            WindowState = FormWindowState.Maximized;
            FormClosed += frmGroceryItems_FormClosed;
            Load += frmGroceryItems_Load;
            KeyDown += frmGroceryItems_KeyDown;
            pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            pnlContent.ResumeLayout(false);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            grpItemDetails.ResumeLayout(false);
            detailsLayout.ResumeLayout(false);
            detailsLayout.PerformLayout();
            imagePathLayout.ResumeLayout(false);
            imagePathLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picItem).EndInit();
            actionLayout.ResumeLayout(false);
            pnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private ErrorProvider errorProvider1;
        private Panel pnlContent;
        private SplitContainer splitMain;
        private GroupBox grpItemDetails;
        private TableLayoutPanel detailsLayout;
        private Label lblItemID;
        private TextBox txtItemID;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblAvailable;
        private ComboBox cboAvailable;
        private Label lblImagePath;
        private TableLayoutPanel imagePathLayout;
        private TextBox txtImage;
        private Button btnBrowse;
        private PictureBox picItem;
        private Label lblImageState;
        private Label lblStatus;
        private Label lblSelectionHint;
        private TableLayoutPanel actionLayout;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnClear;
        private Button btnClose;
        private Panel pnlList;
        private DataGridView dgvItems;
        private Panel pnlSearch;
        private Button btnRefresh;
        private Button btnSearch;
        private TextBox txtSearch;
        private Label lblSearch;
        private Label lblRecordCount;
    }
}
