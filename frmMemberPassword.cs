using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public sealed class frmMemberPassword : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        private readonly TextBox txtMemberId = new TextBox();
        private readonly TextBox txtContact = new TextBox();
        private readonly TextBox txtPassword = new TextBox();
        private readonly TextBox txtConfirmPassword = new TextBox();
        private readonly CheckBox chkShowPassword = new CheckBox();
        private readonly Button btnSave = new Button() { BackColor = Color.Green };
        private readonly Button btnClose = new Button() { BackColor = Color.Red };
        private readonly ErrorProvider errorProvider = new ErrorProvider();

        public frmMemberPassword()
        {
            InitializeForm();
        }

        //=====================================================
        // FORM DESIGN
        //=====================================================
        private void InitializeForm()
        {
            Text = "Member Password";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(650, 470);
            MinimumSize = new Size(650, 470);
            Font = new Font("Rockwell", 11F, FontStyle.Regular);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Label lblTitle = CreateLabel(
                "CREATE OR CHANGE MEMBER PASSWORD",
                new Point(35, 25),
                new Size(580, 40),
                16F,
                FontStyle.Bold);

            Label lblHelp = CreateLabel(
                "Your Member ID is your username. Your role is saved automatically as Member.",
                new Point(35, 72),
                new Size(580, 48),
                10F,
                FontStyle.Italic);

            AddField("Member ID", txtMemberId, 125);
            AddField("Current contact number", txtContact, 185);
            AddField("New password", txtPassword, 245);
            AddField("Confirm password", txtConfirmPassword, 305);

            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            chkShowPassword.AutoSize = true;
            chkShowPassword.Location = new Point(270, 360);
            chkShowPassword.Text = "Show password";
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;

            btnSave.Location = new Point(270, 400);
            btnSave.Size = new Size(165, 45);
            btnSave.Text = "SAVE PASSWORD";
            btnSave.Font = new Font(Font, FontStyle.Bold);
            btnSave.Click += btnSave_Click;

            btnClose.Location = new Point(450, 400);
            btnClose.Size = new Size(165, 45);
            btnClose.Text = "CLOSE";
            btnClose.Click += (sender, e) => Close();

            Controls.AddRange(new Control[]
            {
                lblTitle,
                lblHelp,
                chkShowPassword,
                btnSave,
                btnClose
            });

            errorProvider.ContainerControl = this;
            AcceptButton = btnSave;
            CancelButton = btnClose;
            Load += frmMemberPassword_Load;
        }

        private static Label CreateLabel(
            string text,
            Point location,
            Size size,
            float fontSize,
            FontStyle fontStyle)
        {
            return new Label
            {
                AutoSize = false,
                BackColor = Color.Transparent,
                Font = new Font("Rockwell", fontSize, fontStyle),
                Location = location,
                Size = size,
                Text = text
            };
        }

        private void AddField(string labelText, TextBox textBox, int top)
        {
            Label label = CreateLabel(
                labelText,
                new Point(35, top + 7),
                new Size(220, 35),
                11F,
                FontStyle.Bold);

            textBox.Location = new Point(270, top);
            textBox.Size = new Size(345, 32);
            textBox.MaxLength = 255;

            Controls.Add(label);
            Controls.Add(textBox);
        }

        //=====================================================
        // FORM LOAD
        //=====================================================
        private void frmMemberPassword_Load(object? sender, EventArgs e)
        {
            if (UserSession.IsMember)
            {
                txtMemberId.Text = UserSession.MemberID;
                txtMemberId.ReadOnly = true;
                txtContact.Focus();
            }
            else
            {
                txtMemberId.Focus();
            }
        }

        //=====================================================
        // PASSWORD VALIDATION
        //=====================================================
        private bool ValidateFields()
        {
            errorProvider.Clear();
            Control? firstInvalidControl = null;

            void SetError(Control control, string message)
            {
                errorProvider.SetError(control, message);
                firstInvalidControl ??= control;
            }

            string memberId = txtMemberId.Text.Trim();
            string contact = txtContact.Text.Trim();
            string password = txtPassword.Text;

            if (!Regex.IsMatch(memberId, @"^[A-Za-z0-9-]{1,20}$"))
                SetError(txtMemberId, "Enter a valid Member ID.");

            if (!Regex.IsMatch(contact, @"^\d{10}$"))
                SetError(txtContact, "Enter the member's current 10-digit contact number.");

            if (password.Length < 8
                || !Regex.IsMatch(password, @"[A-Z]")
                || !Regex.IsMatch(password, @"[a-z]")
                || !Regex.IsMatch(password, @"\d"))
            {
                SetError(
                    txtPassword,
                    "Use at least 8 characters with upper-case, lower-case and a number.");
            }

            if (!string.Equals(
                    password,
                    txtConfirmPassword.Text,
                    StringComparison.Ordinal))
            {
                SetError(txtConfirmPassword, "The passwords do not match.");
            }

            if (firstInvalidControl == null)
                return true;

            MessageBox.Show(
                "Please correct the highlighted password details.",
                "Validation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            firstInvalidControl.Focus();
            return false;
        }

        //=====================================================
        // SAVE MEMBER PASSWORD
        //=====================================================
        private void btnSave_Click(object? sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            string memberId = txtMemberId.Text.Trim().ToUpperInvariant();
            string contact = txtContact.Text.Trim();
            string passwordHash = PasswordSecurity.HashPassword(txtPassword.Text);

            try
            {
                using (SqlConnection con = db.GetConnection())
                {
                    con.Open();

                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        VerifyActiveMember(
                            con,
                            transaction,
                            memberId,
                            contact);

                        SaveMemberUser(
                            con,
                            transaction,
                            memberId,
                            passwordHash);

                        transaction.Commit();
                    }
                }

                MessageBox.Show(
                    "The member password was saved successfully." +
                    Environment.NewLine +
                    "Username: " + memberId,
                    "Password Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The member password could not be saved." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Password Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private static void VerifyActiveMember(
            SqlConnection con,
            SqlTransaction transaction,
            string memberId,
            string contact)
        {
            const string sql = @"
SELECT COUNT(*)
FROM dbo.tblMembers
WHERE MemberID = @MemberID
  AND Contact = @Contact
  AND Active = 'Y';";

            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 20).Value =
                    memberId;
                cmd.Parameters.Add("@Contact", SqlDbType.VarChar, 20).Value =
                    contact;

                if (Convert.ToInt32(cmd.ExecuteScalar()) != 1)
                {
                    throw new InvalidOperationException(
                        "The Member ID and contact number do not match an active member.");
                }
            }
        }

        private static void SaveMemberUser(
            SqlConnection con,
            SqlTransaction transaction,
            string memberId,
            string passwordHash)
        {
            const string roleSql = @"
SELECT [Role]
FROM dbo.tblUsers WITH (UPDLOCK, HOLDLOCK)
WHERE Username = @Username;";

            string? existingRole;

            using (SqlCommand roleCommand =
                   new SqlCommand(roleSql, con, transaction))
            {
                roleCommand.Parameters.Add(
                    "@Username",
                    SqlDbType.NVarChar,
                    50).Value = memberId;

                existingRole = Convert.ToString(roleCommand.ExecuteScalar());
            }

            if (!string.IsNullOrWhiteSpace(existingRole)
                && !existingRole.Equals(
                    "Member",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "That username belongs to a non-member account. Ask the Matron for help.");
            }

            const string saveSql = @"
IF EXISTS (SELECT 1 FROM dbo.tblUsers WHERE Username = @Username)
BEGIN
    UPDATE dbo.tblUsers
    SET [Password] = @Password,
        [Role] = 'Member'
    WHERE Username = @Username;
END
ELSE
BEGIN
    INSERT INTO dbo.tblUsers (Username, [Password], [Role])
    VALUES (@Username, @Password, 'Member');
END;";

            using (SqlCommand cmd = new SqlCommand(saveSql, con, transaction))
            {
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value =
                    memberId;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value =
                    passwordHash;
                cmd.ExecuteNonQuery();
            }
        }

        //=====================================================
        // SHOW OR HIDE PASSWORD
        //=====================================================
        private void chkShowPassword_CheckedChanged(object? sender, EventArgs e)
        {
            bool hidePassword = !chkShowPassword.Checked;
            txtPassword.UseSystemPasswordChar = hidePassword;
            txtConfirmPassword.UseSystemPasswordChar = hidePassword;
        }
    }
}
