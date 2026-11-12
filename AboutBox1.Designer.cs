namespace GroceryOrderingManagementSystem
{
    partial class frmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            tableLayoutPanel = new TableLayoutPanel();
            logoPictureBox = new PictureBox();
            labelProductName = new Label();
            labelVersion = new Label();
            labelCopyright = new Label();
            labelCompanyName = new Label();
            textBoxDescription = new TextBox();
            btnClose = new Button();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackgroundImage = (Image)resources.GetObject("tableLayoutPanel.BackgroundImage");
            tableLayoutPanel.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
            tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
            tableLayoutPanel.Controls.Add(labelCompanyName, 1, 3);
            tableLayoutPanel.Controls.Add(textBoxDescription, 1, 4);
            tableLayoutPanel.Controls.Add(btnClose, 1, 5);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(22, 20);
            tableLayoutPanel.Margin = new Padding(8, 7, 8, 7);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.Size = new Size(1672, 936);
            tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            logoPictureBox.BackgroundImage = (Image)resources.GetObject("logoPictureBox.BackgroundImage");
            logoPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            logoPictureBox.Dock = DockStyle.Fill;
            logoPictureBox.Image = (Image)resources.GetObject("logoPictureBox.Image");
            logoPictureBox.Location = new Point(8, 7);
            logoPictureBox.Margin = new Padding(8, 7, 8, 7);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel.SetRowSpan(logoPictureBox, 6);
            logoPictureBox.Size = new Size(535, 922);
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPictureBox.TabIndex = 12;
            logoPictureBox.TabStop = false;
            logoPictureBox.Click += logoPictureBox_Click;
            // 
            // labelProductName
            // 
            labelProductName.BackColor = Color.PaleTurquoise;
            labelProductName.Dock = DockStyle.Fill;
            labelProductName.Font = new Font("Sitka Text", 13.7999992F);
            labelProductName.Location = new Point(566, 0);
            labelProductName.Margin = new Padding(15, 0, 8, 0);
            labelProductName.MaximumSize = new Size(0, 49);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new Size(1098, 49);
            labelProductName.TabIndex = 19;
            labelProductName.Text = "Product Name: Grocery Ordering Management System";
            labelProductName.TextAlign = ContentAlignment.MiddleCenter;
            labelProductName.Click += labelProductName_Click;
            // 
            // labelVersion
            // 
            labelVersion.BackColor = Color.PaleTurquoise;
            labelVersion.Dock = DockStyle.Fill;
            labelVersion.Font = new Font("Sitka Text", 13.7999992F);
            labelVersion.Location = new Point(566, 93);
            labelVersion.Margin = new Padding(15, 0, 8, 0);
            labelVersion.MaximumSize = new Size(0, 49);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(1098, 49);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "Version: 1.0";
            labelVersion.TextAlign = ContentAlignment.MiddleCenter;
            labelVersion.Click += labelVersion_Click;
            // 
            // labelCopyright
            // 
            labelCopyright.BackColor = Color.PaleTurquoise;
            labelCopyright.Dock = DockStyle.Fill;
            labelCopyright.Font = new Font("Sitka Text", 13.7999992F);
            labelCopyright.Location = new Point(566, 186);
            labelCopyright.Margin = new Padding(15, 0, 8, 0);
            labelCopyright.MaximumSize = new Size(0, 49);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(1098, 49);
            labelCopyright.TabIndex = 21;
            labelCopyright.Text = "Copyright";
            labelCopyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelCompanyName
            // 
            labelCompanyName.BackColor = Color.PaleTurquoise;
            labelCompanyName.Dock = DockStyle.Fill;
            labelCompanyName.Font = new Font("Sitka Text", 13.7999992F);
            labelCompanyName.Location = new Point(566, 279);
            labelCompanyName.Margin = new Padding(15, 0, 8, 0);
            labelCompanyName.MaximumSize = new Size(0, 49);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new Size(1098, 49);
            labelCompanyName.TabIndex = 22;
            labelCompanyName.Text = "Company Name";
            labelCompanyName.TextAlign = ContentAlignment.TopCenter;
            labelCompanyName.Click += labelCompanyName_Click;
            // 
            // textBoxDescription
            // 
            textBoxDescription.BackColor = Color.PaleTurquoise;
            textBoxDescription.Dock = DockStyle.Fill;
            textBoxDescription.Font = new Font("Sitka Text", 13.7999992F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxDescription.Location = new Point(566, 379);
            textBoxDescription.Margin = new Padding(15, 7, 8, 7);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.ScrollBars = ScrollBars.Both;
            textBoxDescription.Size = new Size(1098, 454);
            textBoxDescription.TabIndex = 23;
            textBoxDescription.TabStop = false;
            textBoxDescription.Text = "Student: Emmanuel Mpwanga\r\nStudent Number: 17945968\r\nModule: ICT3715\r\nYear: 2026";
            textBoxDescription.TextChanged += textBoxDescription_TextChanged;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.BackColor = Color.Lime;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Font = new Font("Sitka Text", 13.7999992F);
            btnClose.Location = new Point(1381, 860);
            btnClose.Margin = new Padding(8, 7, 8, 7);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(283, 69);
            btnClose.TabIndex = 24;
            btnClose.Text = "&OK";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // frmAbout
            // 
            AcceptButton = btnClose;
            AutoScaleDimensions = new SizeF(15F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1716, 976);
            Controls.Add(tableLayoutPanel);
            Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(8, 7, 8, 7);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAbout";
            Padding = new Padding(22, 20, 22, 20);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button btnClose;
    }
}
