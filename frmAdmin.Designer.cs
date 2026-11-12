namespace GroceryOrderingManagementSystem
{
    partial class frmAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdmin));
            menuStrip1 = new MenuStrip();
            btnMembers = new ToolStripMenuItem();
            btnItems = new ToolStripMenuItem();
            btnReports = new ToolStripMenuItem();
            btnUsers = new ToolStripMenuItem();
            btnAbout = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { btnMembers, btnItems, btnReports, btnUsers, btnAbout });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(11, 3, 0, 3);
            menuStrip1.Size = new Size(2833, 38);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // btnMembers
            // 
            btnMembers.Name = "btnMembers";
            btnMembers.Size = new Size(135, 32);
            btnMembers.Text = "Members";
            btnMembers.Click += btnMembers_Click;
            // 
            // btnItems
            // 
            btnItems.Name = "btnItems";
            btnItems.Size = new Size(189, 32);
            btnItems.Text = "Grocery Items";
            btnItems.Click += btnItems_Click;
            // 
            // btnReports
            // 
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(113, 32);
            btnReports.Text = "Reports";
            btnReports.Click += btnReports_Click;
            // 
            // btnUsers
            // 
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(139, 32);
            btnUsers.Text = "Add Users";
            btnUsers.Click += btnUsers_Click;
            // 
            // btnAbout
            // 
            btnAbout.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(91, 32);
            btnAbout.Text = "About";
            btnAbout.Click += btnAbout_Click;
            // 
            // frmAdmin
            // 
            AutoScaleDimensions = new SizeF(15F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(2833, 1219);
            Controls.Add(menuStrip1);
            Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(6, 4, 6, 4);
            Name = "frmAdmin";
            Text = "frmAdmin";
            Load += frmAdmin_Load_1;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem btnMembers;
        private ToolStripMenuItem btnItems;
        private ToolStripMenuItem btnReports;
        private ToolStripMenuItem btnAbout;
        private ToolStripMenuItem btnUsers;
    }
}