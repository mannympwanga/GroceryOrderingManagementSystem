namespace GroceryOrderingManagementSystem
{
    partial class frmReports
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            lblTitle = new Label();
            tabReports = new TabControl();
            tabWeekly = new TabPage();
            weeklyLayout = new TableLayoutPanel();
            weeklyOptions = new Panel();
            lblSelectWeek = new Label();
            dtpWeek = new DateTimePicker();
            btnLoadWeekly = new Button();
            btnWeeklyPdf = new Button();
            btnLoadIndividual = new Button();
            btnIndividualPdfs = new Button();
            lblWeekRange = new Label();
            lblPurchaseFriday = new Label();
            weeklySplit = new SplitContainer();
            grpWeekly = new GroupBox();
            dgvWeekly = new DataGridView();
            lblWeeklyTotal = new Label();
            grpIndividual = new GroupBox();
            dgvIndividual = new DataGridView();
            lblIndividualTotal = new Label();
            tabManagement = new TabPage();
            managementLayout = new TableLayoutPanel();
            managementOptions = new Panel();
            lblFrom = new Label();
            dtpFrom = new DateTimePicker();
            lblTo = new Label();
            dtpTo = new DateTimePicker();
            lblTop = new Label();
            nudTop = new NumericUpDown();
            lblLow = new Label();
            nudLow = new NumericUpDown();
            btnItemTrends = new Button();
            btnMemberActivity = new Button();
            lblManagementHelp = new Label();
            managementSplit = new SplitContainer();
            grpItemTrends = new GroupBox();
            dgvItemTrends = new DataGridView();
            lblTrendSummary = new Label();
            grpMemberActivity = new GroupBox();
            dgvMemberActivity = new DataGridView();
            lblParticipationSummary = new Label();
            footerPanel = new Panel();
            btnOpenFolder = new Button();
            btnClose = new Button();
            errorProvider1 = new ErrorProvider(components);
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            tabReports.SuspendLayout();
            tabWeekly.SuspendLayout();
            weeklyLayout.SuspendLayout();
            weeklyOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)weeklySplit).BeginInit();
            weeklySplit.Panel1.SuspendLayout();
            weeklySplit.Panel2.SuspendLayout();
            weeklySplit.SuspendLayout();
            grpWeekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWeekly).BeginInit();
            grpIndividual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIndividual).BeginInit();
            tabManagement.SuspendLayout();
            managementLayout.SuspendLayout();
            managementOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)managementSplit).BeginInit();
            managementSplit.Panel1.SuspendLayout();
            managementSplit.Panel2.SuspendLayout();
            managementSplit.SuspendLayout();
            grpItemTrends.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItemTrends).BeginInit();
            grpMemberActivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMemberActivity).BeginInit();
            footerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.BackColor = Color.Transparent;
            rootLayout.BackgroundImage = Properties.Resources._676b3c4c_c68b_42be_8899_5d18196a14d6;
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(tabReports, 0, 1);
            rootLayout.Controls.Add(footerPanel, 0, 2);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(14);
            rootLayout.RowCount = 3;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
            rootLayout.Size = new Size(1600, 900);
            rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.Transparent;
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(17, 17);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1566, 72);
            headerPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.LawnGreen;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Rockwell", 27.75F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Blue;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1566, 72);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "REPORTS AND MANAGEMENT INFORMATION";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabReports
            // 
            tabReports.Controls.Add(tabWeekly);
            tabReports.Controls.Add(tabManagement);
            tabReports.Dock = DockStyle.Fill;
            tabReports.Font = new Font("Rockwell", 12F, FontStyle.Bold);
            tabReports.Location = new Point(17, 95);
            tabReports.Name = "tabReports";
            tabReports.SelectedIndex = 0;
            tabReports.Size = new Size(1566, 720);
            tabReports.TabIndex = 1;
            // 
            // tabWeekly
            // 
            tabWeekly.BackColor = Color.WhiteSmoke;
            tabWeekly.Controls.Add(weeklyLayout);
            tabWeekly.Location = new Point(4, 33);
            tabWeekly.Name = "tabWeekly";
            tabWeekly.Padding = new Padding(8);
            tabWeekly.Size = new Size(1558, 683);
            tabWeekly.TabIndex = 0;
            tabWeekly.Text = "Weekly and Member Reports";
            // 
            // weeklyLayout
            // 
            weeklyLayout.ColumnCount = 1;
            weeklyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            weeklyLayout.Controls.Add(weeklyOptions, 0, 0);
            weeklyLayout.Controls.Add(weeklySplit, 0, 1);
            weeklyLayout.Dock = DockStyle.Fill;
            weeklyLayout.Location = new Point(8, 8);
            weeklyLayout.Name = "weeklyLayout";
            weeklyLayout.RowCount = 2;
            weeklyLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            weeklyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            weeklyLayout.Size = new Size(1542, 667);
            weeklyLayout.TabIndex = 0;
            // 
            // weeklyOptions
            // 
            weeklyOptions.BackColor = Color.Transparent;
            weeklyOptions.Controls.Add(lblSelectWeek);
            weeklyOptions.Controls.Add(dtpWeek);
            weeklyOptions.Controls.Add(btnLoadWeekly);
            weeklyOptions.Controls.Add(btnWeeklyPdf);
            weeklyOptions.Controls.Add(btnLoadIndividual);
            weeklyOptions.Controls.Add(btnIndividualPdfs);
            weeklyOptions.Controls.Add(lblWeekRange);
            weeklyOptions.Controls.Add(lblPurchaseFriday);
            weeklyOptions.Dock = DockStyle.Fill;
            weeklyOptions.Location = new Point(3, 3);
            weeklyOptions.Name = "weeklyOptions";
            weeklyOptions.Size = new Size(1536, 106);
            weeklyOptions.TabIndex = 0;
            // 
            // lblSelectWeek
            // 
            lblSelectWeek.AutoSize = true;
            lblSelectWeek.Location = new Point(16, 18);
            lblSelectWeek.Name = "lblSelectWeek";
            lblSelectWeek.Size = new Size(189, 24);
            lblSelectWeek.TabIndex = 0;
            lblSelectWeek.Text = "Select order week:";
            // 
            // dtpWeek
            // 
            dtpWeek.Format = DateTimePickerFormat.Short;
            dtpWeek.Location = new Point(221, 11);
            dtpWeek.Name = "dtpWeek";
            dtpWeek.Size = new Size(160, 31);
            dtpWeek.TabIndex = 0;
            dtpWeek.ValueChanged += dtpWeek_ValueChanged;
            // 
            // btnLoadWeekly
            // 
            btnLoadWeekly.BackColor = Color.GreenYellow;
            btnLoadWeekly.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnLoadWeekly.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLoadWeekly.FlatStyle = FlatStyle.Flat;
            btnLoadWeekly.Location = new Point(434, 7);
            btnLoadWeekly.Name = "btnLoadWeekly";
            btnLoadWeekly.Size = new Size(188, 40);
            btnLoadWeekly.TabIndex = 1;
            btnLoadWeekly.Text = "SHOW WEEKLY LIST";
            btnLoadWeekly.UseVisualStyleBackColor = false;
            btnLoadWeekly.Click += btnLoadWeekly_Click;
            // 
            // btnWeeklyPdf
            // 
            btnWeeklyPdf.BackColor = Color.Gold;
            btnWeeklyPdf.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnWeeklyPdf.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnWeeklyPdf.FlatStyle = FlatStyle.Flat;
            btnWeeklyPdf.Location = new Point(644, 7);
            btnWeeklyPdf.Name = "btnWeeklyPdf";
            btnWeeklyPdf.Size = new Size(200, 40);
            btnWeeklyPdf.TabIndex = 2;
            btnWeeklyPdf.Text = "CREATE WEEKLY PDF";
            btnWeeklyPdf.UseVisualStyleBackColor = false;
            btnWeeklyPdf.Click += btnWeeklyPdf_Click;
            // 
            // btnLoadIndividual
            // 
            btnLoadIndividual.BackColor = Color.Orange;
            btnLoadIndividual.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnLoadIndividual.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLoadIndividual.FlatStyle = FlatStyle.Flat;
            btnLoadIndividual.Location = new Point(869, 7);
            btnLoadIndividual.Name = "btnLoadIndividual";
            btnLoadIndividual.Size = new Size(220, 40);
            btnLoadIndividual.TabIndex = 3;
            btnLoadIndividual.Text = "VIEW MEMBER ORDERS";
            btnLoadIndividual.UseVisualStyleBackColor = false;
            btnLoadIndividual.Click += btnLoadIndividual_Click;
            // 
            // btnIndividualPdfs
            // 
            btnIndividualPdfs.BackColor = Color.YellowGreen;
            btnIndividualPdfs.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnIndividualPdfs.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnIndividualPdfs.FlatStyle = FlatStyle.Flat;
            btnIndividualPdfs.Location = new Point(1106, 7);
            btnIndividualPdfs.Name = "btnIndividualPdfs";
            btnIndividualPdfs.Size = new Size(250, 40);
            btnIndividualPdfs.TabIndex = 4;
            btnIndividualPdfs.Text = "GENERATE MEMBER PDFs";
            btnIndividualPdfs.UseVisualStyleBackColor = false;
            btnIndividualPdfs.Click += btnIndividualPdfs_Click;
            // 
            // lblWeekRange
            // 
            lblWeekRange.AutoSize = true;
            lblWeekRange.Font = new Font("Rockwell", 11F, FontStyle.Bold);
            lblWeekRange.Location = new Point(16, 61);
            lblWeekRange.Name = "lblWeekRange";
            lblWeekRange.Size = new Size(165, 23);
            lblWeekRange.TabIndex = 5;
            lblWeekRange.Text = "Ordering period";
            // 
            // lblPurchaseFriday
            // 
            lblPurchaseFriday.AutoSize = true;
            lblPurchaseFriday.Font = new Font("Rockwell", 11F, FontStyle.Bold);
            lblPurchaseFriday.ForeColor = Color.DarkGreen;
            lblPurchaseFriday.Location = new Point(16, 84);
            lblPurchaseFriday.Name = "lblPurchaseFriday";
            lblPurchaseFriday.Size = new Size(166, 23);
            lblPurchaseFriday.TabIndex = 6;
            lblPurchaseFriday.Text = "Purchase Friday";
            // 
            // weeklySplit
            // 
            weeklySplit.Dock = DockStyle.Fill;
            weeklySplit.Location = new Point(3, 115);
            weeklySplit.Name = "weeklySplit";
            // 
            // weeklySplit.Panel1
            // 
            weeklySplit.Panel1.Controls.Add(grpWeekly);
            // 
            // weeklySplit.Panel2
            // 
            weeklySplit.Panel2.Controls.Add(grpIndividual);
            weeklySplit.Size = new Size(1536, 549);
            weeklySplit.SplitterDistance = 690;
            weeklySplit.SplitterWidth = 8;
            weeklySplit.TabIndex = 1;
            // 
            // grpWeekly
            // 
            grpWeekly.Controls.Add(dgvWeekly);
            grpWeekly.Controls.Add(lblWeeklyTotal);
            grpWeekly.Dock = DockStyle.Fill;
            grpWeekly.Font = new Font("Rockwell", 12F, FontStyle.Bold);
            grpWeekly.Location = new Point(0, 0);
            grpWeekly.Name = "grpWeekly";
            grpWeekly.Padding = new Padding(10);
            grpWeekly.Size = new Size(690, 549);
            grpWeekly.TabIndex = 0;
            grpWeekly.TabStop = false;
            grpWeekly.Text = "Consolidated Weekly Grocery Order";
            // 
            // dgvWeekly
            // 
            dgvWeekly.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWeekly.Dock = DockStyle.Fill;
            dgvWeekly.Location = new Point(10, 34);
            dgvWeekly.Name = "dgvWeekly";
            dgvWeekly.RowHeadersWidth = 51;
            dgvWeekly.Size = new Size(670, 457);
            dgvWeekly.TabIndex = 0;
            // 
            // lblWeeklyTotal
            // 
            lblWeeklyTotal.BackColor = Color.Transparent;
            lblWeeklyTotal.Dock = DockStyle.Bottom;
            lblWeeklyTotal.Font = new Font("Rockwell", 12F, FontStyle.Bold);
            lblWeeklyTotal.ForeColor = Color.DarkGreen;
            lblWeeklyTotal.Location = new Point(10, 491);
            lblWeeklyTotal.Name = "lblWeeklyTotal";
            lblWeeklyTotal.Size = new Size(670, 48);
            lblWeeklyTotal.TabIndex = 1;
            lblWeeklyTotal.Text = "Grand total: R0,00";
            lblWeeklyTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grpIndividual
            // 
            grpIndividual.Controls.Add(dgvIndividual);
            grpIndividual.Controls.Add(lblIndividualTotal);
            grpIndividual.Dock = DockStyle.Fill;
            grpIndividual.Font = new Font("Rockwell", 12F, FontStyle.Bold);
            grpIndividual.Location = new Point(0, 0);
            grpIndividual.Name = "grpIndividual";
            grpIndividual.Padding = new Padding(10);
            grpIndividual.Size = new Size(838, 549);
            grpIndividual.TabIndex = 0;
            grpIndividual.TabStop = false;
            grpIndividual.Text = "Individual Member Order Summaries";
            // 
            // dgvIndividual
            // 
            dgvIndividual.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIndividual.Dock = DockStyle.Fill;
            dgvIndividual.Location = new Point(10, 34);
            dgvIndividual.Name = "dgvIndividual";
            dgvIndividual.RowHeadersWidth = 51;
            dgvIndividual.Size = new Size(818, 457);
            dgvIndividual.TabIndex = 0;
            // 
            // lblIndividualTotal
            // 
            lblIndividualTotal.BackColor = Color.Transparent;
            lblIndividualTotal.Dock = DockStyle.Bottom;
            lblIndividualTotal.Font = new Font("Rockwell", 11F, FontStyle.Bold);
            lblIndividualTotal.Location = new Point(10, 491);
            lblIndividualTotal.Name = "lblIndividualTotal";
            lblIndividualTotal.Size = new Size(818, 48);
            lblIndividualTotal.TabIndex = 1;
            lblIndividualTotal.Text = "Individual orders have not been loaded.";
            lblIndividualTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tabManagement
            // 
            tabManagement.BackColor = Color.Transparent;
            tabManagement.Controls.Add(managementLayout);
            tabManagement.Location = new Point(4, 33);
            tabManagement.Name = "tabManagement";
            tabManagement.Padding = new Padding(8);
            tabManagement.Size = new Size(1558, 683);
            tabManagement.TabIndex = 1;
            tabManagement.Text = "Management Information Reports";
            // 
            // managementLayout
            // 
            managementLayout.ColumnCount = 1;
            managementLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            managementLayout.Controls.Add(managementOptions, 0, 0);
            managementLayout.Controls.Add(managementSplit, 0, 1);
            managementLayout.Dock = DockStyle.Fill;
            managementLayout.Location = new Point(8, 8);
            managementLayout.Name = "managementLayout";
            managementLayout.RowCount = 2;
            managementLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 108F));
            managementLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            managementLayout.Size = new Size(1542, 667);
            managementLayout.TabIndex = 0;
            // 
            // managementOptions
            // 
            managementOptions.BackColor = Color.Transparent;
            managementOptions.Controls.Add(lblFrom);
            managementOptions.Controls.Add(dtpFrom);
            managementOptions.Controls.Add(lblTo);
            managementOptions.Controls.Add(dtpTo);
            managementOptions.Controls.Add(lblTop);
            managementOptions.Controls.Add(nudTop);
            managementOptions.Controls.Add(lblLow);
            managementOptions.Controls.Add(nudLow);
            managementOptions.Controls.Add(btnItemTrends);
            managementOptions.Controls.Add(btnMemberActivity);
            managementOptions.Controls.Add(lblManagementHelp);
            managementOptions.Dock = DockStyle.Fill;
            managementOptions.Location = new Point(3, 3);
            managementOptions.Name = "managementOptions";
            managementOptions.Size = new Size(1536, 102);
            managementOptions.TabIndex = 0;
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(18, 17);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(83, 24);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "FROM:";
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(107, 15);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(155, 31);
            dtpFrom.TabIndex = 0;
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(270, 17);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(48, 24);
            lblTo.TabIndex = 2;
            lblTo.Text = "TO:";
            // 
            // dtpTo
            // 
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(324, 15);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(155, 31);
            dtpTo.TabIndex = 1;
            // 
            // lblTop
            // 
            lblTop.AutoSize = true;
            lblTop.Location = new Point(500, 17);
            lblTop.Name = "lblTop";
            lblTop.Size = new Size(104, 24);
            lblTop.TabIndex = 4;
            lblTop.Text = "Show top:";
            // 
            // nudTop
            // 
            nudTop.Location = new Point(607, 10);
            nudTop.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudTop.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTop.Name = "nudTop";
            nudTop.Size = new Size(55, 31);
            nudTop.TabIndex = 2;
            nudTop.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // lblLow
            // 
            lblLow.AutoSize = true;
            lblLow.Location = new Point(668, 17);
            lblLow.Name = "lblLow";
            lblLow.Size = new Size(107, 24);
            lblLow.TabIndex = 6;
            lblLow.Text = "Show low:";
            // 
            // nudLow
            // 
            nudLow.Location = new Point(781, 12);
            nudLow.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudLow.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLow.Name = "nudLow";
            nudLow.Size = new Size(55, 31);
            nudLow.TabIndex = 3;
            nudLow.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // btnItemTrends
            // 
            btnItemTrends.BackColor = Color.GreenYellow;
            btnItemTrends.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnItemTrends.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnItemTrends.FlatStyle = FlatStyle.Flat;
            btnItemTrends.Location = new Point(1005, 9);
            btnItemTrends.Name = "btnItemTrends";
            btnItemTrends.Size = new Size(215, 40);
            btnItemTrends.TabIndex = 4;
            btnItemTrends.Text = "CREATE ITEM REPORT";
            btnItemTrends.UseVisualStyleBackColor = false;
            btnItemTrends.Click += btnItemTrends_Click;
            // 
            // btnMemberActivity
            // 
            btnMemberActivity.BackColor = Color.Gold;
            btnMemberActivity.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnMemberActivity.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnMemberActivity.FlatStyle = FlatStyle.Flat;
            btnMemberActivity.Location = new Point(1251, 12);
            btnMemberActivity.Name = "btnMemberActivity";
            btnMemberActivity.Size = new Size(250, 40);
            btnMemberActivity.TabIndex = 5;
            btnMemberActivity.Text = "CREATE MEMBER REPORT";
            btnMemberActivity.UseVisualStyleBackColor = false;
            btnMemberActivity.Click += btnMemberActivity_Click;
            // 
            // lblManagementHelp
            // 
            lblManagementHelp.AutoSize = true;
            lblManagementHelp.Font = new Font("Rockwell", 10.5F, FontStyle.Bold);
            lblManagementHelp.ForeColor = Color.DarkSlateGray;
            lblManagementHelp.Location = new Point(18, 65);
            lblManagementHelp.Name = "lblManagementHelp";
            lblManagementHelp.Size = new Size(930, 22);
            lblManagementHelp.TabIndex = 10;
            lblManagementHelp.Text = "Item trends rank the highest and lowest ordered items. Participation counts distinct weekly order cycles.";
            // 
            // managementSplit
            // 
            managementSplit.Dock = DockStyle.Fill;
            managementSplit.Location = new Point(3, 111);
            managementSplit.Name = "managementSplit";
            // 
            // managementSplit.Panel1
            // 
            managementSplit.Panel1.Controls.Add(grpItemTrends);
            // 
            // managementSplit.Panel2
            // 
            managementSplit.Panel2.Controls.Add(grpMemberActivity);
            managementSplit.Size = new Size(1536, 553);
            managementSplit.SplitterDistance = 750;
            managementSplit.SplitterWidth = 8;
            managementSplit.TabIndex = 1;
            // 
            // grpItemTrends
            // 
            grpItemTrends.Controls.Add(dgvItemTrends);
            grpItemTrends.Controls.Add(lblTrendSummary);
            grpItemTrends.Dock = DockStyle.Fill;
            grpItemTrends.Location = new Point(0, 0);
            grpItemTrends.Name = "grpItemTrends";
            grpItemTrends.Padding = new Padding(10);
            grpItemTrends.Size = new Size(750, 553);
            grpItemTrends.TabIndex = 0;
            grpItemTrends.TabStop = false;
            grpItemTrends.Text = "Grocery Item Ordering Trends";
            // 
            // dgvItemTrends
            // 
            dgvItemTrends.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItemTrends.Dock = DockStyle.Fill;
            dgvItemTrends.Location = new Point(10, 34);
            dgvItemTrends.Name = "dgvItemTrends";
            dgvItemTrends.RowHeadersWidth = 51;
            dgvItemTrends.Size = new Size(730, 457);
            dgvItemTrends.TabIndex = 0;
            // 
            // lblTrendSummary
            // 
            lblTrendSummary.BackColor = Color.Transparent;
            lblTrendSummary.Dock = DockStyle.Bottom;
            lblTrendSummary.Font = new Font("Rockwell", 10.5F, FontStyle.Bold);
            lblTrendSummary.Location = new Point(10, 491);
            lblTrendSummary.Name = "lblTrendSummary";
            lblTrendSummary.Size = new Size(730, 52);
            lblTrendSummary.TabIndex = 1;
            lblTrendSummary.Text = "Choose a valid date range and create the item report.";
            lblTrendSummary.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpMemberActivity
            // 
            grpMemberActivity.Controls.Add(dgvMemberActivity);
            grpMemberActivity.Controls.Add(lblParticipationSummary);
            grpMemberActivity.Dock = DockStyle.Fill;
            grpMemberActivity.Location = new Point(0, 0);
            grpMemberActivity.Name = "grpMemberActivity";
            grpMemberActivity.Padding = new Padding(10);
            grpMemberActivity.Size = new Size(778, 553);
            grpMemberActivity.TabIndex = 0;
            grpMemberActivity.TabStop = false;
            grpMemberActivity.Text = "Member Participation Analysis";
            // 
            // dgvMemberActivity
            // 
            dgvMemberActivity.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMemberActivity.Dock = DockStyle.Fill;
            dgvMemberActivity.Location = new Point(10, 34);
            dgvMemberActivity.Name = "dgvMemberActivity";
            dgvMemberActivity.RowHeadersWidth = 51;
            dgvMemberActivity.Size = new Size(758, 457);
            dgvMemberActivity.TabIndex = 0;
            // 
            // lblParticipationSummary
            // 
            lblParticipationSummary.BackColor = Color.Transparent;
            lblParticipationSummary.Dock = DockStyle.Bottom;
            lblParticipationSummary.Font = new Font("Rockwell", 10.5F, FontStyle.Bold);
            lblParticipationSummary.Location = new Point(10, 491);
            lblParticipationSummary.Name = "lblParticipationSummary";
            lblParticipationSummary.Size = new Size(758, 52);
            lblParticipationSummary.TabIndex = 1;
            lblParticipationSummary.Text = "Choose a valid date range and create the participation report.";
            lblParticipationSummary.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // footerPanel
            // 
            footerPanel.BackColor = Color.Transparent;
            footerPanel.Controls.Add(btnOpenFolder);
            footerPanel.Controls.Add(btnClose);
            footerPanel.Dock = DockStyle.Fill;
            footerPanel.Location = new Point(17, 821);
            footerPanel.Name = "footerPanel";
            footerPanel.Size = new Size(1566, 62);
            footerPanel.TabIndex = 2;
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.BackColor = Color.Aqua;
            btnOpenFolder.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnOpenFolder.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnOpenFolder.FlatStyle = FlatStyle.Flat;
            btnOpenFolder.Location = new Point(18, 10);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new Size(235, 42);
            btnOpenFolder.TabIndex = 0;
            btnOpenFolder.Text = "OPEN REPORTS FOLDER";
            btnOpenFolder.UseVisualStyleBackColor = false;
            btnOpenFolder.Click += btnOpenFolder_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Red;
            btnClose.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnClose.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.DarkRed;
            btnClose.Location = new Point(1373, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(175, 42);
            btnClose.TabIndex = 1;
            btnClose.Text = "CLOSE";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // frmReports
            // 
            AutoScaleDimensions = new SizeF(12F, 22F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1600, 900);
            Controls.Add(rootLayout);
            Font = new Font("Rockwell", 12F);
            Margin = new Padding(5, 4, 5, 4);
            MinimumSize = new Size(1250, 760);
            Name = "frmReports";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Grocery Ordering Management System - Reports";
            WindowState = FormWindowState.Maximized;
            Load += frmReports_Load;
            rootLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            tabReports.ResumeLayout(false);
            tabWeekly.ResumeLayout(false);
            weeklyLayout.ResumeLayout(false);
            weeklyOptions.ResumeLayout(false);
            weeklyOptions.PerformLayout();
            weeklySplit.Panel1.ResumeLayout(false);
            weeklySplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)weeklySplit).EndInit();
            weeklySplit.ResumeLayout(false);
            grpWeekly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvWeekly).EndInit();
            grpIndividual.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvIndividual).EndInit();
            tabManagement.ResumeLayout(false);
            managementLayout.ResumeLayout(false);
            managementOptions.ResumeLayout(false);
            managementOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLow).EndInit();
            managementSplit.Panel1.ResumeLayout(false);
            managementSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)managementSplit).EndInit();
            managementSplit.ResumeLayout(false);
            grpItemTrends.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvItemTrends).EndInit();
            grpMemberActivity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMemberActivity).EndInit();
            footerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel rootLayout;
        private Panel headerPanel;
        private Label lblTitle;
        private TabControl tabReports;
        private TabPage tabWeekly;
        private TableLayoutPanel weeklyLayout;
        private Panel weeklyOptions;
        private Label lblSelectWeek;
        private DateTimePicker dtpWeek;
        private Button btnLoadWeekly;
        private Button btnWeeklyPdf;
        private Button btnLoadIndividual;
        private Button btnIndividualPdfs;
        private Label lblWeekRange;
        private Label lblPurchaseFriday;
        private SplitContainer weeklySplit;
        private GroupBox grpWeekly;
        private DataGridView dgvWeekly;
        private Label lblWeeklyTotal;
        private GroupBox grpIndividual;
        private DataGridView dgvIndividual;
        private Label lblIndividualTotal;
        private TabPage tabManagement;
        private TableLayoutPanel managementLayout;
        private Panel managementOptions;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Label lblTop;
        private NumericUpDown nudTop;
        private Label lblLow;
        private NumericUpDown nudLow;
        private Button btnItemTrends;
        private Button btnMemberActivity;
        private Label lblManagementHelp;
        private SplitContainer managementSplit;
        private GroupBox grpItemTrends;
        private DataGridView dgvItemTrends;
        private Label lblTrendSummary;
        private GroupBox grpMemberActivity;
        private DataGridView dgvMemberActivity;
        private Label lblParticipationSummary;
        private Panel footerPanel;
        private Button btnOpenFolder;
        private Button btnClose;
        private ErrorProvider errorProvider1;
    }
}
