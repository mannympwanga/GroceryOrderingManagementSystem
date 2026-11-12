namespace GroceryOrderingManagementSystem
{
    partial class frmDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDashboard));
            Dashboard = new MenuStrip();
            btnMembers = new ToolStripMenuItem();
            btnPassword = new ToolStripMenuItem();
            btnAdminFunctions = new ToolStripMenuItem();
            btnAbout = new ToolStripMenuItem();
            btnLogout = new ToolStripMenuItem();
            lblWelcome = new Label();
            Dashboard.SuspendLayout();
            SuspendLayout();
            // 
            // Dashboard
            // 
            Dashboard.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            Dashboard.ImageScalingSize = new Size(20, 20);
            Dashboard.Items.AddRange(new ToolStripItem[] { btnMembers, btnPassword, btnAdminFunctions, btnAbout, btnLogout });
            Dashboard.Location = new Point(0, 0);
            Dashboard.Name = "Dashboard";
            Dashboard.Padding = new Padding(11, 3, 0, 3);
            Dashboard.Size = new Size(2074, 38);
            Dashboard.TabIndex = 0;
            Dashboard.Text = "menuStrip1";
            // 
            // btnMembers
            // 
            btnMembers.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            btnMembers.Name = "btnMembers";
            btnMembers.Size = new Size(197, 32);
            btnMembers.Text = "Members Only";
            btnMembers.Click += btnMembers_Click;
            // 
            // btnPassword
            // 
            btnPassword.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            btnPassword.Name = "btnPassword";
            btnPassword.Size = new Size(226, 32);
            btnPassword.Text = "Change Password";
            btnPassword.Click += btnPassword_Click;
            // 
            // btnAdminFunctions
            // 
            btnAdminFunctions.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            btnAdminFunctions.Name = "btnAdminFunctions";
            btnAdminFunctions.Size = new Size(218, 32);
            btnAdminFunctions.Text = "Admin Functions";
            btnAdminFunctions.Click += btnAdminFunctions_Click;
            // 
            // btnAbout
            // 
            btnAbout.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(91, 32);
            btnAbout.Text = "About";
            btnAbout.Click += btnAbout_Click;
            // 
            // btnLogout
            // 
            btnLogout.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(103, 32);
            btnLogout.Text = "Logout";
            btnLogout.Click += btnLogout_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.BackColor = Color.Transparent;
            lblWelcome.Font = new Font("Rockwell", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.Location = new Point(721, 38);
            lblWelcome.MaximumSize = new Size(12000, 2800);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Padding = new Padding(20);
            lblWelcome.Size = new Size(196, 78);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Welcome";
            // 
            // frmDashboard
            // 
            AutoScaleDimensions = new SizeF(15F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(2074, 1035);
            Controls.Add(lblWelcome);
            Controls.Add(Dashboard);
            Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            MainMenuStrip = Dashboard;
            Margin = new Padding(6, 4, 6, 4);
            Name = "frmDashboard";
            Text = "Member Dashboard";
            Load += frmDashboard_Load;
            Dashboard.ResumeLayout(false);
            Dashboard.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip Dashboard;
        private ToolStripMenuItem btnMembers;
        private ToolStripMenuItem btnAbout;
        private ToolStripMenuItem btnLogout;
        private ToolStripMenuItem btnAdminFunctions;
        private ToolStripMenuItem btnPassword;
        private Label lblWelcome;
    }
}
