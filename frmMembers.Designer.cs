namespace GroceryOrderingManagementSystem
{
    partial class frmMembers
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
            lblSubtitle = new Label();
            lblTitle = new Label();
            pnlContent = new Panel();
            splitMain = new SplitContainer();
            grpMemberDetails = new GroupBox();
            detailsLayout = new TableLayoutPanel();
            lblMemberID = new Label();
            txtMemberID = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            lblSurname = new Label();
            txtSurname = new TextBox();
            lblContact = new Label();
            txtContact = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblActive = new Label();
            cboActive = new ComboBox();
            lblSelectionHint = new Label();
            actionLayout = new TableLayoutPanel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnClear = new Button();
            btnClose = new Button();
            pnlList = new Panel();
            dgvMembers = new DataGridView();
            pnlSearch = new Panel();
            btnRefresh = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            lblRecordCount = new Label();
            errorProvider1 = new ErrorProvider(components);
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            grpMemberDetails.SuspendLayout();
            detailsLayout.SuspendLayout();
            actionLayout.SuspendLayout();
            pnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMembers).BeginInit();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(0, 102, 51);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(24, 12, 24, 10);
            pnlHeader.Size = new Size(1400, 105);
            pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.WhiteSmoke;
            lblSubtitle.Location = new Point(24, 66);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(1352, 29);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Administrator access • Add new members • Update existing members • Search and retrieve records";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Georgia", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(24, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1352, 52);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "MAINTAIN MEMBER RECORDS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(236, 244, 238);
            pnlContent.Controls.Add(splitMain);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 105);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(18);
            pnlContent.Size = new Size(1400, 745);
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
            splitMain.Panel1.Controls.Add(grpMemberDetails);
            splitMain.Panel1.Padding = new Padding(0, 0, 9, 0);
            splitMain.Panel1MinSize = 390;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(pnlList);
            splitMain.Panel2.Padding = new Padding(9, 0, 0, 0);
            splitMain.Panel2MinSize = 520;
            splitMain.Size = new Size(1364, 709);
            splitMain.SplitterDistance = 430;
            splitMain.SplitterWidth = 2;
            splitMain.TabIndex = 0;
            // 
            // grpMemberDetails
            // 
            grpMemberDetails.BackColor = Color.White;
            grpMemberDetails.Controls.Add(detailsLayout);
            grpMemberDetails.Dock = DockStyle.Fill;
            grpMemberDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            grpMemberDetails.ForeColor = Color.FromArgb(35, 35, 35);
            grpMemberDetails.Location = new Point(0, 0);
            grpMemberDetails.Name = "grpMemberDetails";
            grpMemberDetails.Padding = new Padding(16, 14, 16, 16);
            grpMemberDetails.Size = new Size(421, 709);
            grpMemberDetails.TabIndex = 0;
            grpMemberDetails.TabStop = false;
            grpMemberDetails.Text = "Member details";
            // 
            // detailsLayout
            // 
            detailsLayout.ColumnCount = 1;
            detailsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            detailsLayout.Controls.Add(lblMemberID, 0, 0);
            detailsLayout.Controls.Add(txtMemberID, 0, 1);
            detailsLayout.Controls.Add(lblName, 0, 2);
            detailsLayout.Controls.Add(txtName, 0, 3);
            detailsLayout.Controls.Add(lblSurname, 0, 4);
            detailsLayout.Controls.Add(txtSurname, 0, 5);
            detailsLayout.Controls.Add(lblContact, 0, 6);
            detailsLayout.Controls.Add(txtContact, 0, 7);
            detailsLayout.Controls.Add(lblEmail, 0, 8);
            detailsLayout.Controls.Add(txtEmail, 0, 9);
            detailsLayout.Controls.Add(lblActive, 0, 10);
            detailsLayout.Controls.Add(cboActive, 0, 11);
            detailsLayout.Controls.Add(lblSelectionHint, 0, 12);
            detailsLayout.Controls.Add(actionLayout, 0, 13);
            detailsLayout.Dock = DockStyle.Fill;
            detailsLayout.Location = new Point(16, 41);
            detailsLayout.Name = "detailsLayout";
            detailsLayout.RowCount = 14;
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
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            detailsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            detailsLayout.Size = new Size(389, 652);
            detailsLayout.TabIndex = 0;
            // 
            // lblMemberID
            // 
            lblMemberID.Dock = DockStyle.Fill;
            lblMemberID.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMemberID.Location = new Point(3, 0);
            lblMemberID.Name = "lblMemberID";
            lblMemberID.Size = new Size(383, 25);
            lblMemberID.TabIndex = 0;
            lblMemberID.Text = "Member ID *";
            lblMemberID.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtMemberID
            // 
            txtMemberID.CharacterCasing = CharacterCasing.Upper;
            txtMemberID.Dock = DockStyle.Fill;
            txtMemberID.Font = new Font("Segoe UI", 11F);
            txtMemberID.Location = new Point(0, 27);
            txtMemberID.Margin = new Padding(0, 2, 18, 5);
            txtMemberID.MaxLength = 20;
            txtMemberID.Name = "txtMemberID";
            txtMemberID.Size = new Size(371, 32);
            txtMemberID.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblName.Location = new Point(3, 67);
            lblName.Name = "lblName";
            lblName.Size = new Size(383, 25);
            lblName.TabIndex = 1;
            lblName.Text = "Name *";
            lblName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Fill;
            txtName.Font = new Font("Segoe UI", 11F);
            txtName.Location = new Point(0, 94);
            txtName.Margin = new Padding(0, 2, 18, 5);
            txtName.MaxLength = 80;
            txtName.Name = "txtName";
            txtName.Size = new Size(371, 32);
            txtName.TabIndex = 1;
            txtName.KeyPress += txtName_KeyPress;
            // 
            // lblSurname
            // 
            lblSurname.Dock = DockStyle.Fill;
            lblSurname.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSurname.Location = new Point(3, 134);
            lblSurname.Name = "lblSurname";
            lblSurname.Size = new Size(383, 25);
            lblSurname.TabIndex = 2;
            lblSurname.Text = "Surname *";
            lblSurname.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtSurname
            // 
            txtSurname.Dock = DockStyle.Fill;
            txtSurname.Font = new Font("Segoe UI", 11F);
            txtSurname.Location = new Point(0, 161);
            txtSurname.Margin = new Padding(0, 2, 18, 5);
            txtSurname.MaxLength = 80;
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(371, 32);
            txtSurname.TabIndex = 2;
            txtSurname.KeyPress += txtSurname_KeyPress;
            // 
            // lblContact
            // 
            lblContact.Dock = DockStyle.Fill;
            lblContact.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblContact.Location = new Point(3, 201);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(383, 25);
            lblContact.TabIndex = 3;
            lblContact.Text = "Contact number (10 digits) *";
            lblContact.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtContact
            // 
            txtContact.Dock = DockStyle.Fill;
            txtContact.Font = new Font("Segoe UI", 11F);
            txtContact.Location = new Point(0, 228);
            txtContact.Margin = new Padding(0, 2, 18, 5);
            txtContact.MaxLength = 10;
            txtContact.Name = "txtContact";
            txtContact.Size = new Size(371, 32);
            txtContact.TabIndex = 3;
            txtContact.KeyPress += txtContact_KeyPress;
            // 
            // lblEmail
            // 
            lblEmail.Dock = DockStyle.Fill;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.Location = new Point(3, 268);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(383, 25);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email address *";
            lblEmail.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtEmail
            // 
            txtEmail.Dock = DockStyle.Fill;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(0, 295);
            txtEmail.Margin = new Padding(0, 2, 18, 5);
            txtEmail.MaxLength = 150;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(371, 32);
            txtEmail.TabIndex = 4;
            txtEmail.Leave += txtEmail_Leave;
            // 
            // lblActive
            // 
            lblActive.Dock = DockStyle.Fill;
            lblActive.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblActive.Location = new Point(3, 335);
            lblActive.Name = "lblActive";
            lblActive.Size = new Size(383, 25);
            lblActive.TabIndex = 5;
            lblActive.Text = "Active (Y/N) *";
            lblActive.TextAlign = ContentAlignment.BottomLeft;
            // 
            // cboActive
            // 
            cboActive.Dock = DockStyle.Fill;
            cboActive.DropDownStyle = ComboBoxStyle.DropDownList;
            cboActive.Font = new Font("Segoe UI", 11F);
            cboActive.Location = new Point(0, 362);
            cboActive.Margin = new Padding(0, 2, 18, 5);
            cboActive.Name = "cboActive";
            cboActive.Size = new Size(371, 33);
            cboActive.TabIndex = 5;
            // 
            // lblSelectionHint
            // 
            lblSelectionHint.Dock = DockStyle.Fill;
            lblSelectionHint.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblSelectionHint.ForeColor = Color.DimGray;
            lblSelectionHint.Location = new Point(3, 402);
            lblSelectionHint.Name = "lblSelectionHint";
            lblSelectionHint.Size = new Size(383, 48);
            lblSelectionHint.TabIndex = 6;
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
            actionLayout.Location = new Point(0, 458);
            actionLayout.Margin = new Padding(0, 8, 0, 0);
            actionLayout.Name = "actionLayout";
            actionLayout.RowCount = 2;
            actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            actionLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            actionLayout.Size = new Size(389, 118);
            actionLayout.TabIndex = 6;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(46, 184, 46);
            btnAdd.Dock = DockStyle.Fill;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(0, 3);
            btnAdd.Margin = new Padding(0, 3, 5, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(189, 51);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "ADD MEMBER";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(0, 120, 215);
            btnUpdate.Dock = DockStyle.Fill;
            btnUpdate.Enabled = false;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(199, 3);
            btnUpdate.Margin = new Padding(5, 3, 18, 5);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(172, 51);
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
            btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClear.ForeColor = Color.Black;
            btnClear.Location = new Point(0, 64);
            btnClear.Margin = new Padding(0, 5, 5, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(189, 51);
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
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(199, 64);
            btnClose.Margin = new Padding(5, 5, 18, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(172, 51);
            btnClose.TabIndex = 3;
            btnClose.Text = "CLOSE";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // pnlList
            // 
            pnlList.BackColor = Color.White;
            pnlList.BorderStyle = BorderStyle.FixedSingle;
            pnlList.Controls.Add(dgvMembers);
            pnlList.Controls.Add(pnlSearch);
            pnlList.Dock = DockStyle.Fill;
            pnlList.Location = new Point(9, 0);
            pnlList.Name = "pnlList";
            pnlList.Padding = new Padding(14);
            pnlList.Size = new Size(923, 709);
            pnlList.TabIndex = 0;
            // 
            // dgvMembers
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(232, 246, 235);
            dgvMembers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvMembers.BackgroundColor = Color.White;
            dgvMembers.BorderStyle = BorderStyle.None;
            dgvMembers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(0, 102, 51);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 102, 51);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvMembers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvMembers.ColumnHeadersHeight = 42;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(187, 222, 251);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvMembers.DefaultCellStyle = dataGridViewCellStyle3;
            dgvMembers.Dock = DockStyle.Fill;
            dgvMembers.EnableHeadersVisualStyles = false;
            dgvMembers.Location = new Point(14, 105);
            dgvMembers.Name = "dgvMembers";
            dgvMembers.RowHeadersVisible = false;
            dgvMembers.RowHeadersWidth = 51;
            dgvMembers.RowTemplate.Height = 32;
            dgvMembers.Size = new Size(893, 588);
            dgvMembers.TabIndex = 1;
            dgvMembers.CellClick += dgvMembers_CellClick;
            dgvMembers.CellDoubleClick += dgvMembers_CellDoubleClick;
            dgvMembers.DataBindingComplete += dgvMembers_DataBindingComplete;
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
            pnlSearch.Size = new Size(893, 91);
            pnlSearch.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.FromArgb(90, 90, 90);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(714, 27);
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
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(550, 27);
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
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(0, 28);
            txtSearch.MaxLength = 120;
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Enter search text, or leave blank to show all members";
            txtSearch.Size = new Size(540, 32);
            txtSearch.TabIndex = 0;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearch.Location = new Point(0, 1);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(455, 23);
            lblSearch.TabIndex = 3;
            lblSearch.Text = "Search by member ID, name, surname, contact or email";
            // 
            // lblRecordCount
            // 
            lblRecordCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRecordCount.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblRecordCount.ForeColor = Color.DimGray;
            lblRecordCount.Location = new Point(623, 65);
            lblRecordCount.Name = "lblRecordCount";
            lblRecordCount.Size = new Size(266, 22);
            lblRecordCount.TabIndex = 4;
            lblRecordCount.Text = "0 members shown";
            lblRecordCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            // 
            // frmMembers
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(236, 244, 238);
            ClientSize = new Size(1400, 850);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 10F);
            KeyPreview = true;
            MinimumSize = new Size(1050, 700);
            Name = "frmMembers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Maintain Member Records";
            WindowState = FormWindowState.Maximized;
            Load += frmMembers_Load;
            KeyDown += frmMembers_KeyDown;
            pnlHeader.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            grpMemberDetails.ResumeLayout(false);
            detailsLayout.ResumeLayout(false);
            detailsLayout.PerformLayout();
            actionLayout.ResumeLayout(false);
            pnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMembers).EndInit();
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlContent;
        private SplitContainer splitMain;
        private GroupBox grpMemberDetails;
        private TableLayoutPanel detailsLayout;
        private Label lblMemberID;
        private TextBox txtMemberID;
        private Label lblName;
        private TextBox txtName;
        private Label lblSurname;
        private TextBox txtSurname;
        private Label lblContact;
        private TextBox txtContact;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblActive;
        private ComboBox cboActive;
        private Label lblSelectionHint;
        private TableLayoutPanel actionLayout;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnClear;
        private Button btnClose;
        private Panel pnlList;
        private DataGridView dgvMembers;
        private Panel pnlSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;
        private Label lblSearch;
        private Label lblRecordCount;
        private ErrorProvider errorProvider1;
    }
}
