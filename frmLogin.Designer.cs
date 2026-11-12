
namespace GroceryOrderingManagementSystem
{
    partial class frmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            label1 = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnExit = new Button();
            chkShowPassword = new CheckBox();
            btnMemberPassword = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Castellar", 55F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(0, 192, 192);
            label1.Location = new Point(1064, 95);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(383, 111);
            label1.TabIndex = 0;
            label1.Text = "LOGIN";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Sitka Text", 28.1999989F, FontStyle.Bold);
            txtUsername.Location = new Point(934, 307);
            txtUsername.Margin = new Padding(6, 4, 6, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(542, 66);
            txtUsername.TabIndex = 3;
            txtUsername.KeyDown += txtUsername_KeyDown;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Sitka Text", 28.1999989F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(934, 424);
            txtPassword.Margin = new Padding(6, 4, 6, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(542, 66);
            txtPassword.TabIndex = 4;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.Lime;
            btnLogin.Font = new Font("Bookman Old Style", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogin.Location = new Point(1001, 626);
            btnLogin.Margin = new Padding(6, 4, 6, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(368, 106);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "LOGIN";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Verdana", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.Red;
            btnExit.Location = new Point(1001, 773);
            btnExit.Margin = new Padding(6, 4, 6, 4);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(368, 95);
            btnExit.TabIndex = 6;
            btnExit.Text = "EXIT";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            chkShowPassword.Location = new Point(1488, 444);
            chkShowPassword.Margin = new Padding(6, 4, 6, 4);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(205, 32);
            chkShowPassword.TabIndex = 7;
            chkShowPassword.Text = "Show password";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnMemberPassword
            // 
            btnMemberPassword.Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            btnMemberPassword.Location = new Point(934, 520);
            btnMemberPassword.Name = "btnMemberPassword";
            btnMemberPassword.Size = new Size(542, 61);
            btnMemberPassword.TabIndex = 8;
            btnMemberPassword.Text = "CREATE / CHANGE MEMBER PASSWORD";
            btnMemberPassword.UseVisualStyleBackColor = true;
            btnMemberPassword.Click += btnMemberPassword_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(15F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(2338, 1043);
            Controls.Add(btnMemberPassword);
            Controls.Add(chkShowPassword);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(label1);
            Font = new Font("Rockwell", 13.8F, FontStyle.Bold);
            Margin = new Padding(6, 4, 6, 4);
            Name = "frmLogin";
            Text = "Login";
            Load += frmLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        private CheckBox chkShowPassword;
        private Button btnMemberPassword;
    }
}
