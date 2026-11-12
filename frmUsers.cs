using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public sealed class frmUsers : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        private readonly TextBox txtUsername = new TextBox();
        private readonly TextBox txtPassword = new TextBox();
        private readonly TextBox txtConfirmPassword = new TextBox();
        private readonly ComboBox cboRole = new ComboBox();
        private readonly CheckBox chkShowPassword = new CheckBox();
        private readonly TextBox txtSearch = new TextBox();
        private readonly DataGridView dgvUsers = new DataGridView();
        private readonly Button btnAdd = new Button() { BackColor = Color.LightGreen };
        private readonly Button btnUpdate = new Button() { BackColor = Color.LightBlue };
        private readonly Button btnClear = new Button() { BackColor = Color.LightYellow };
        private readonly Button btnSearch = new Button() { BackColor = Color.Gold };
        private readonly Button btnRefresh = new Button() { BackColor = Color.SkyBlue };
        private readonly Button btnClose = new Button() { BackColor = Color.DarkRed };
        private readonly ErrorProvider errorProvider = new ErrorProvider();

        private string selectedUsername = string.Empty;

        public frmUsers()
        {
            InitializeForm();
        }

        //=====================================================
        // FORM DESIGN
        //=====================================================
        private void InitializeForm()
        {
            Text = "Maintain Users and Roles";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1120, 690);
            MinimumSize = new Size(1000, 650);
            Font = new Font("Rockwell", 10.5F, FontStyle.Regular);
            KeyPreview = true;

            // Add controls to the form
            Controls.Add(CreateLabel(
                "MAINTAIN USERS AND ROLES",
                new Point(30, 20),
                new Size(510, 38),
                17F,
                FontStyle.Bold));

            // Add a descriptive label about password and username requirements
            Controls.Add(CreateLabel(
                "Passwords are never displayed. Member usernames must match a Member ID.",
                new Point(30, 60),
                new Size(700, 30),
                10F,
                FontStyle.Italic));

            AddField("Username / Member ID", txtUsername, 30, 115, 280);
            AddField("Password", txtPassword, 30, 180, 280);
            AddField("Confirm password", txtConfirmPassword, 30, 245, 280);

            Controls.Add(CreateLabel(
                "Role",
                new Point(30, 317),
                new Size(220, 30),
                10.5F,
                FontStyle.Bold));

            // Configure the ComboBox for role selection
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Location = new Point(30, 350);
            cboRole.Size = new Size(280, 32);
            Controls.Add(cboRole);

            // Configure the CheckBox for showing/hiding password
            chkShowPassword.AutoSize = true;
            chkShowPassword.Location = new Point(30, 395);
            chkShowPassword.Text = "Show password";
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            Controls.Add(chkShowPassword);

            // Configure buttons for adding, updating, clearing, and closing
            ConfigureButton(btnAdd, "ADD USER", new Point(30, 440), btnAdd_Click);
            ConfigureButton(btnUpdate, "UPDATE USER", new Point(180, 440), btnUpdate_Click);
            ConfigureButton(btnClear, "CLEAR", new Point(30, 495), btnClear_Click);
            ConfigureButton(btnClose, "CLOSE", new Point(180, 495), btnClose_Click);

            // Configure search label and textbox
            Controls.Add(CreateLabel(
                "Search users",
                new Point(360, 95),
                new Size(180, 30),
                10.5F,
                FontStyle.Bold));

            // Configure the search TextBox
            txtSearch.Location = new Point(360, 127);
            txtSearch.Size = new Size(420, 32);
            txtSearch.KeyDown += txtSearch_KeyDown;
            Controls.Add(txtSearch);

            ConfigureButton(btnSearch, "SEARCH", new Point(795, 120), btnSearch_Click);
            ConfigureButton(btnRefresh, "REFRESH", new Point(945, 120), btnRefresh_Click);

            // Configure the DataGridView for displaying users
            dgvUsers.Location = new Point(360, 180);
            dgvUsers.Size = new Size(730, 455);
            dgvUsers.Anchor = AnchorStyles.Top |
                              AnchorStyles.Bottom |
                              AnchorStyles.Left |
                              AnchorStyles.Right;
            dgvUsers.CellClick += dgvUsers_CellClick;
            Controls.Add(dgvUsers);

            // Set password fields to use system password character
            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;
            btnUpdate.Enabled = false;

            errorProvider.ContainerControl = this;
            Load += frmUsers_Load;
            KeyDown += frmUsers_KeyDown;
        }


        //=====================================================
        // HELPER METHODS FOR CREATING CONTROLS
        //=====================================================
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


        //=====================================================
        // ADDING FIELDS AND CONFIGURING BUTTONS
        //=====================================================
        private void AddField(
            string labelText,
            TextBox textBox,
            int left,
            int top,
            int width)
        {
            Controls.Add(CreateLabel(
                labelText,
                new Point(left, top),
                new Size(width, 30),
                10.5F,
                FontStyle.Bold));

            textBox.Location = new Point(left, top + 32);
            textBox.Size = new Size(width, 32);
            textBox.MaxLength = 255;
            Controls.Add(textBox);
        }

        //=====================================================
        // CONFIGURE BUTTONS
        //=====================================================
        private void ConfigureButton(
            Button button,
            string text,
            Point location,
            EventHandler clickHandler)
        {
            button.Location = location;
            button.Size = new Size(130, 45);
            button.Text = text;
            button.Font = new Font(Font, FontStyle.Bold);
            button.Click += clickHandler;
            Controls.Add(button);
        }

        //=====================================================
        // FORM LOAD AND ACCESS CONTROL
        //=====================================================
        private void frmUsers_Load(object? sender, EventArgs e)
        {
            if (!UserSession.IsAdministrator)
            {
                MessageBox.Show(
                    "Only the administrator (Matron) may maintain user accounts.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                BeginInvoke(new Action(Close));
                return;
            }

            cboRole.Items.AddRange(new object[] { "Admin", "Member" });
            cboRole.SelectedIndex = 1;
            LoadUsers();
            txtUsername.Focus();
        }

        //=====================================================
        // LOAD AND SEARCH USERS
        //=====================================================
        private void LoadUsers(string searchText = "")
        {
            const string baseSql = @"
SELECT Username, [Role]
FROM dbo.tblUsers";

            string sql = baseSql;
            bool searching = !string.IsNullOrWhiteSpace(searchText);

            if (searching)
            {
                sql += @"
WHERE Username LIKE @Search
   OR [Role] LIKE @Search";
            }

            sql += "\nORDER BY [Role], Username;";

            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                if (searching)
                {
                    cmd.Parameters.Add(
                        "@Search",
                        SqlDbType.NVarChar,
                        100).Value = "%" + searchText.Trim() + "%";
                }

                DataTable users = new DataTable();
                adapter.Fill(users);
                dgvUsers.DataSource = users;
            }

            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.MultiSelect = false;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.ClearSelection();
        }

        private bool UserExists(string username)
        {
            const string sql = @"
SELECT COUNT(*)
FROM dbo.tblUsers
WHERE Username = @Username;";

            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add(
                    "@Username",
                    SqlDbType.NVarChar,
                    50).Value = username;

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        //=====================================================
        // USER VALIDATION
        //=====================================================
        private bool ValidateFields(bool passwordRequired)
        {
            errorProvider.Clear();
            Control? firstInvalidControl = null;

            void SetError(Control control, string message)
            {
                errorProvider.SetError(control, message);
                firstInvalidControl ??= control;
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (!Regex.IsMatch(username, @"^[A-Za-z0-9._-]{1,50}$"))
            {
                SetError(
                    txtUsername,
                    "Use 1 to 50 letters, numbers, dots, underscores or hyphens.");
            }

            bool passwordEntered = password.Length > 0;

            if (passwordRequired || passwordEntered)
            {
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
            }

            if (cboRole.SelectedIndex < 0)
                SetError(cboRole, "Select Admin or Member.");

            if (firstInvalidControl != null)
            {
                MessageBox.Show(
                    "Please correct the highlighted user details.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                firstInvalidControl.Focus();
                return false;
            }

            if (cboRole.Text == "Member" && !MemberRecordExists(username))
            {
                MessageBox.Show(
                    "A Member account username must match an existing Member ID.",
                    "Member Record Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUsername.Focus();
                return false;
            }

            return true;
        }

        private bool MemberRecordExists(string memberId)
        {
            const string sql = @"
SELECT COUNT(*)
FROM dbo.tblMembers
WHERE MemberID = @MemberID;";

            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 20).Value =
                    memberId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
            }
        }

        //=====================================================
        // ADD USER
        //=====================================================
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateFields(passwordRequired: true))
                return;

            string username = txtUsername.Text.Trim();

            try
            {
                if (UserExists(username))
                {
                    MessageBox.Show(
                        "That username already exists. Select it and use Update.",
                        "Duplicate Username",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                const string sql = @"
INSERT INTO dbo.tblUsers (Username, [Password], [Role])
VALUES (@Username, @Password, @Role);";

                using (SqlConnection con = db.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    AddUserParameters(cmd, includePassword: true);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "The user account was added successfully.",
                    "User Added",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();
                LoadUsers();
            }
            catch (Exception ex)
            {
                ShowError("The user account could not be added.", ex);
            }
        }

        //=====================================================
        // UPDATE USER AND ROLE
        //=====================================================
        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedUsername))
            {
                MessageBox.Show(
                    "Select an existing user from the table first.",
                    "User Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateFields(passwordRequired: false))
                return;

            if (string.Equals(
                    selectedUsername,
                    UserSession.LoginName,
                    StringComparison.OrdinalIgnoreCase)
                && cboRole.Text != "Admin")
            {
                MessageBox.Show(
                    "You cannot remove the administrator role from the account currently in use.",
                    "Role Change Blocked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                "Save the changes to user " + selectedUsername + "?",
                "Confirm Update",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
                return;

            try
            {
                bool updatePassword = txtPassword.TextLength > 0;

                string sql = updatePassword
                    ? @"
UPDATE dbo.tblUsers
SET [Password] = @Password,
    [Role] = @Role
WHERE Username = @OriginalUsername;"
                    : @"
UPDATE dbo.tblUsers
SET [Role] = @Role
WHERE Username = @OriginalUsername;";

                using (SqlConnection con = db.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    AddUserParameters(cmd, updatePassword);
                    cmd.Parameters.Add(
                        "@OriginalUsername",
                        SqlDbType.NVarChar,
                        50).Value = selectedUsername;

                    con.Open();

                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        throw new InvalidOperationException(
                            "No matching user account was updated.");
                    }
                }

                MessageBox.Show(
                    "The user account was updated successfully.",
                    "User Updated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();
                LoadUsers();
            }
            catch (Exception ex)
            {
                ShowError("The user account could not be updated.", ex);
            }
        }

        //=====================================================
        // ADD SQL PARAMETERS FOR USER INSERT OR UPDATE
        //=====================================================

        private void AddUserParameters(
            SqlCommand cmd,
            bool includePassword)
        {
            cmd.Parameters.Add(
                "@Username",
                SqlDbType.NVarChar,
                50).Value = txtUsername.Text.Trim();

            if (includePassword)
            {
                cmd.Parameters.Add(
                    "@Password",
                    SqlDbType.NVarChar,
                    255).Value = PasswordSecurity.HashPassword(txtPassword.Text);
            }

            cmd.Parameters.Add(
                "@Role",
                SqlDbType.NVarChar,
                20).Value = cboRole.Text;
        }

        //=====================================================
        // GRID AND FORM CONTROLS
        //=====================================================
        private void dgvUsers_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvUsers.Rows.Count)
                return;

            DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
            selectedUsername =
                Convert.ToString(row.Cells["Username"].Value) ?? string.Empty;

            txtUsername.Text = selectedUsername;
            txtUsername.ReadOnly = true;
            cboRole.Text = Convert.ToString(row.Cells["Role"].Value) ?? "Member";
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            btnUpdate.Enabled = true;
        }

        //=====================================================
        //  SEARCH, REFRESH, AND CLEAR BUTTONS
        //=====================================================
        private void btnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                LoadUsers(txtSearch.Text);
            }
            catch (Exception ex)
            {
                ShowError("The user search could not be completed.", ex);
            }
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            ClearFields();
            LoadUsers();
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            selectedUsername = string.Empty;
            txtUsername.ReadOnly = false;
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            cboRole.SelectedIndex = cboRole.Items.Count > 0 ? 1 : -1;
            chkShowPassword.Checked = false;
            errorProvider.Clear();
            dgvUsers.ClearSelection();
            btnUpdate.Enabled = false;
            txtUsername.Focus();
        }


        //=====================================================
        // EVENT HANDLERS
        //=====================================================

        private void chkShowPassword_CheckedChanged(object? sender, EventArgs e)
        {
            bool hidePassword = !chkShowPassword.Checked;
            txtPassword.UseSystemPasswordChar = hidePassword;
            txtConfirmPassword.UseSystemPasswordChar = hidePassword;
        }

        //=====================================================
        // FORM KEYBOARD SHORTCUTS
        //=====================================================

        private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch.PerformClick();
            }
        }

        //=====================================================
        // FORM CLOSING AND ERROR HANDLING
        //=====================================================
        private void frmUsers_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                ClearFields();
            }
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private static void ShowError(string heading, Exception ex)
        {
            MessageBox.Show(
                heading + Environment.NewLine + Environment.NewLine + ex.Message,
                "User Maintenance Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
