using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public partial class frmLogin : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public frmLogin()
        {
            InitializeComponent();
        }

        //=====================================================
        // FORM LOAD
        //=====================================================
        private void frmLogin_Load(object sender, EventArgs e)
        {
            chkShowPassword.Checked = false;
            txtPassword.UseSystemPasswordChar = true;
            AcceptButton = btnLogin;
            txtUsername.Focus();
        }

        //=====================================================
        // LOGIN VALIDATION
        //=====================================================
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string loginName = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(loginName))
            {
                ShowValidation("Enter your username or Member ID.", txtUsername);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowValidation("Enter your password.", txtPassword);
                return;
            }

            try
            {
                using (SqlConnection con = db.GetConnection())
                {
                    con.Open();

                    if (!TryAuthenticateUser(
                            con,
                            loginName,
                            password,
                            out string role,
                            out string memberId,
                            out string displayName,
                            out string failureReason))
                    {
                        LoginFailed(failureReason);
                        return;
                    }

                    UserSession.LoginName = loginName;
                    UserSession.Username = displayName;
                    UserSession.MemberID = memberId;
                    UserSession.Role = role;

                    MessageBox.Show(
                        UserSession.IsAdministrator
                            ? "Administrator (Matron) login successful."
                            : "Member login successful.",
                        "Login Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    OpenSecuredForm(
                        UserSession.IsAdministrator
                            ? new frmAdmin()
                            : new frmDashboard());
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "The system could not connect to the database." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An unexpected login error occurred." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //=====================================================
        // USER AUTHENTICATION
        //=====================================================
        private static bool TryAuthenticateUser(
            SqlConnection con,
            string loginName,
            string enteredPassword,
            out string role,
            out string memberId,
            out string displayName,
            out string failureReason)
        {
            role = string.Empty;
            memberId = string.Empty;
            displayName = string.Empty;
            failureReason = "The username or password is incorrect.";

            const string userSql = @"
SELECT TOP (1)
    Username,
    [Password],
    [Role]
FROM dbo.tblUsers
WHERE Username = @Username;";

            string storedPassword;
            string storedRole;
            string storedUsername;

            using (SqlCommand cmd = new SqlCommand(userSql, con))
            {
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value =
                    loginName;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return false;

                    storedUsername =
                        Convert.ToString(reader["Username"]) ?? string.Empty;
                    storedPassword =
                        Convert.ToString(reader["Password"]) ?? string.Empty;
                    storedRole =
                        Convert.ToString(reader["Role"])?.Trim() ?? string.Empty;
                }
            }


            if (storedRole.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                || storedRole.Equals("Matron", StringComparison.OrdinalIgnoreCase))
            {
                role = "Admin";
                displayName = storedUsername;
                return true;
            }

            if (!storedRole.Equals("Member", StringComparison.OrdinalIgnoreCase))
            {
                failureReason =
                    "This account has an invalid role. Ask the Matron to correct it.";
                return false;
            }

            return TryLoadActiveMember(
                con,
                storedUsername,
                out memberId,
                out displayName,
                out failureReason,
                out role);
        }

        private static bool TryLoadActiveMember(
            SqlConnection con,
            string memberUsername,
            out string memberId,
            out string displayName,
            out string failureReason,
            out string role)
        {
            memberId = string.Empty;
            displayName = string.Empty;
            role = string.Empty;
            failureReason =
                "This member account is inactive or is not linked to a member record.";

            const string memberSql = @"
SELECT TOP (1)
    MemberID,
    Name,
    Surname
FROM dbo.tblMembers
WHERE MemberID = @MemberID
  AND Active = 'Y';";

            using (SqlCommand cmd = new SqlCommand(memberSql, con))
            {
                cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 20).Value =
                    memberUsername;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return false;

                    memberId =
                        Convert.ToString(reader["MemberID"]) ?? string.Empty;

                    displayName = (
                        (Convert.ToString(reader["Name"]) ?? string.Empty) + " " +
                        (Convert.ToString(reader["Surname"]) ?? string.Empty))
                        .Trim();

                    role = "Member";
                    failureReason = string.Empty;
                    return true;
                }
            }
        }

        //=====================================================
        // MEMBER PASSWORD ENROLMENT
        //=====================================================
        private void btnMemberPassword_Click(object sender, EventArgs e)
        {
            using (frmMemberPassword passwordForm = new frmMemberPassword())
            {
                passwordForm.ShowDialog(this);
            }

            txtUsername.Focus();
        }

        //=====================================================
        // LOGIN FEEDBACK
        //=====================================================
        private static void ShowValidation(string message, Control control)
        {
            MessageBox.Show(
                message,
                "Login Validation",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            control.Focus();
        }

        private void LoginFailed(string message)
        {
            MessageBox.Show(
                message,
                "Login Failed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            txtPassword.Clear();
            chkShowPassword.Checked = false;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.Focus();
        }

        //=====================================================
        // OPEN AUTHORISED FORM
        //=====================================================
        private void OpenSecuredForm(Form nextForm)
        {
            Hide();

            nextForm.FormClosed += (sender, e) =>
            {
                UserSession.Clear();

                txtUsername.Clear();
                txtPassword.Clear();
                chkShowPassword.Checked = false;
                txtPassword.UseSystemPasswordChar = true;

                Show();
                Activate();
                txtUsername.Focus();
            };

            nextForm.Show();
        }

        //=====================================================
        // SHOW OR HIDE PASSWORD
        //=====================================================
        private void chkShowPassword_CheckedChanged(
            object sender,
            EventArgs e)
        {
            txtPassword.UseSystemPasswordChar =
                !chkShowPassword.Checked;
        }

        //=====================================================
        // EXIT
        //=====================================================
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit Application",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Application.Exit();
        }

        //=====================================================
        // KEYBOARD CONTROLS
        //=====================================================
        private void txtUsername_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin.PerformClick();
            }
        }
    }
}
