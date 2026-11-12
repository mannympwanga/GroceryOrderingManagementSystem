using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public partial class frmMembers : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();
        private string selectedMemberId = string.Empty;

        public frmMembers()
        {
            InitializeComponent();
        }

        //=====================================================
        // FORM LOAD AND ACCESS CONTROL
        //=====================================================
        private void frmMembers_Load(object sender, EventArgs e)
        {
            if (!HasAdministrativeAccess())
            {
                MessageBox.Show(
                    "Only the administrator (Matron) may maintain member records.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                BeginInvoke(new Action(Close));
                return;
            }

            try
            {
                cboActive.Items.Clear();
                cboActive.Items.AddRange(new object[] { "Y", "N" });
                cboActive.SelectedIndex = 0;

                LoadMembers();
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                ShowError("The member records could not be loaded.", ex);
            }
        }

        private bool HasAdministrativeAccess()
        {
            return UserSession.IsAdministrator;
        }

        //=====================================================
        // LOAD AND DISPLAY MEMBERS
        //=====================================================
        private void LoadMembers(string searchText = "")
        {
            const string baseSql = @"
SELECT MemberID, Name, Surname, Contact, Email, Active
FROM dbo.tblMembers";

            string sql = baseSql;
            bool searching = !string.IsNullOrWhiteSpace(searchText);

            if (searching)
            {
                sql += @"
WHERE MemberID LIKE @Search
   OR Name LIKE @Search
   OR Surname LIKE @Search
   OR Contact LIKE @Search
   OR Email LIKE @Search";
            }

            sql += "\nORDER BY MemberID;";

            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                if (searching)
                {
                    cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 120).Value =
                        "%" + searchText.Trim() + "%";
                }

                DataTable members = new DataTable();
                adapter.Fill(members);
                dgvMembers.DataSource = members;
                FormatGrid();
                lblRecordCount.Text = members.Rows.Count +
                    (members.Rows.Count == 1 ? " member shown" : " members shown");
            }
        }

        private void FormatGrid()
        {
            dgvMembers.ReadOnly = true;
            dgvMembers.AllowUserToAddRows = false;
            dgvMembers.AllowUserToDeleteRows = false;
            dgvMembers.AllowUserToResizeRows = false;
            dgvMembers.MultiSelect = false;
            dgvMembers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvMembers.Columns.Contains("MemberID"))
            {
                dgvMembers.Columns["MemberID"].HeaderText = "Member ID";
                dgvMembers.Columns["MemberID"].FillWeight = 65;
            }

            if (dgvMembers.Columns.Contains("Name"))
                dgvMembers.Columns["Name"].HeaderText = "Name";

            if (dgvMembers.Columns.Contains("Surname"))
                dgvMembers.Columns["Surname"].HeaderText = "Surname";

            if (dgvMembers.Columns.Contains("Contact"))
                dgvMembers.Columns["Contact"].HeaderText = "Contact Number";

            if (dgvMembers.Columns.Contains("Email"))
            {
                dgvMembers.Columns["Email"].HeaderText = "Email Address";
                dgvMembers.Columns["Email"].FillWeight = 150;
            }

            if (dgvMembers.Columns.Contains("Active"))
            {
                dgvMembers.Columns["Active"].HeaderText = "Active";
                dgvMembers.Columns["Active"].FillWeight = 45;
            }

            dgvMembers.ClearSelection();
        }

        //=====================================================
        // MEMBER VALIDATION
        //=====================================================
        private bool ValidateFields()
        {
            errorProvider1.Clear();
            Control firstInvalidControl = null;

            void SetValidationError(Control control, string message)
            {
                errorProvider1.SetError(control, message);
                if (firstInvalidControl == null)
                    firstInvalidControl = control;
            }

            string memberId = txtMemberID.Text.Trim();
            string name = txtName.Text.Trim();
            string surname = txtSurname.Text.Trim();
            string contact = txtContact.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (memberId.Length == 0)
                SetValidationError(txtMemberID, "Member ID is required.");
            else if (!Regex.IsMatch(memberId, @"^[A-Za-z0-9-]{1,20}$"))
                SetValidationError(txtMemberID, "Use only letters, numbers and hyphens.");

            if (name.Length == 0)
                SetValidationError(txtName, "Name is required.");
            else if (!Regex.IsMatch(name, @"^[\p{L}][\p{L}\s'-]*$"))
                SetValidationError(txtName, "Enter a valid name.");

            if (surname.Length == 0)
                SetValidationError(txtSurname, "Surname is required.");
            else if (!Regex.IsMatch(surname, @"^[\p{L}][\p{L}\s'-]*$"))
                SetValidationError(txtSurname, "Enter a valid surname.");

            if (!Regex.IsMatch(contact, @"^\d{10}$"))
                SetValidationError(txtContact, "Enter exactly 10 digits.");

            if (!Regex.IsMatch(
                    email,
                    @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
            {
                SetValidationError(txtEmail, "Enter a valid email address.");
            }

            if (cboActive.SelectedIndex < 0)
                SetValidationError(cboActive, "Select Y or N.");

            if (firstInvalidControl != null)
            {
                MessageBox.Show(
                    "Please correct the highlighted member details.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                firstInvalidControl.Focus();
                return false;
            }

            return true;
        }

        //=====================================================
        // DATABASE HELPERS
        //=====================================================
        private bool MemberExists(string memberId)
        {
            const string sql = @"
SELECT COUNT(*)
FROM dbo.tblMembers
WHERE MemberID = @MemberID;";

            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 20).Value = memberId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void AddMemberParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 20).Value =
                txtMemberID.Text.Trim().ToUpperInvariant();
            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 80).Value = txtName.Text.Trim();
            cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 80).Value = txtSurname.Text.Trim();
            cmd.Parameters.Add("@Contact", SqlDbType.VarChar, 20).Value = txtContact.Text.Trim();
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = txtEmail.Text.Trim();
            cmd.Parameters.Add("@Active", SqlDbType.Char, 1).Value = cboActive.Text;
        }

        //=====================================================
        // ADD MEMBER
        //=====================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            string memberId = txtMemberID.Text.Trim().ToUpperInvariant();

            try
            {
                if (MemberExists(memberId))
                {
                    MessageBox.Show(
                        "That Member ID already exists. Select the record and use Update.",
                        "Duplicate Member ID",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtMemberID.Focus();
                    return;
                }

                const string sql = @"
INSERT INTO dbo.tblMembers
    (MemberID, Name, Surname, Contact, Email, Active)
VALUES
    (@MemberID, @Name, @Surname, @Contact, @Email, @Active);";

                using (SqlConnection con = db.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    AddMemberParameters(cmd);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "The member was added successfully.",
                    "Member Added",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();
                LoadMembers();
            }
            catch (SqlException ex)
            {
                ShowError("The member could not be added.", ex);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while adding the member.", ex);
            }
        }

        //=====================================================
        // UPDATE MEMBER
        //=====================================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
                return;

            string memberId = string.IsNullOrWhiteSpace(selectedMemberId)
                ? txtMemberID.Text.Trim().ToUpperInvariant()
                : selectedMemberId;

            try
            {
                if (!MemberExists(memberId))
                {
                    MessageBox.Show(
                        "Select an existing member from the table before updating.",
                        "Member Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmation = MessageBox.Show(
                    "Save the changes to member " + memberId + "?",
                    "Confirm Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmation != DialogResult.Yes)
                    return;

                const string sql = @"
UPDATE dbo.tblMembers
SET Name = @Name,
    Surname = @Surname,
    Contact = @Contact,
    Email = @Email,
    Active = @Active
WHERE MemberID = @OriginalMemberID;";

                using (SqlConnection con = db.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    AddMemberParameters(cmd);
                    cmd.Parameters.Add("@OriginalMemberID", SqlDbType.VarChar, 20).Value = memberId;
                    con.Open();

                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No matching member record was updated.");
                }

                MessageBox.Show(
                    "The member was updated successfully.",
                    "Member Updated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();
                LoadMembers();
            }
            catch (SqlException ex)
            {
                ShowError("The member could not be updated.", ex);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while updating the member.", ex);
            }
        }

        //=====================================================
        // SEARCH, REFRESH AND CLOSE
        //=====================================================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadMembers(txtSearch.Text);
            }
            catch (Exception ex)
            {
                ShowError("The member search could not be completed.", ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                ClearFields();
                LoadMembers();
            }
            catch (Exception ex)
            {
                ShowError("The member list could not be refreshed.", ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //=====================================================
        // GRID SELECTION
        //=====================================================
        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvMembers.Rows.Count)
                return;

            DataGridViewRow row = dgvMembers.Rows[e.RowIndex];

            selectedMemberId = Convert.ToString(row.Cells["MemberID"].Value) ?? string.Empty;
            txtMemberID.Text = selectedMemberId;
            txtName.Text = Convert.ToString(row.Cells["Name"].Value) ?? string.Empty;
            txtSurname.Text = Convert.ToString(row.Cells["Surname"].Value) ?? string.Empty;
            txtContact.Text = Convert.ToString(row.Cells["Contact"].Value) ?? string.Empty;
            txtEmail.Text = Convert.ToString(row.Cells["Email"].Value) ?? string.Empty;
            cboActive.Text = Convert.ToString(row.Cells["Active"].Value) ?? "Y";

            txtMemberID.ReadOnly = true;
            txtMemberID.BackColor = System.Drawing.Color.Gainsboro;
            btnUpdate.Enabled = true;
        }

        private void dgvMembers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvMembers_CellClick(sender, e);
        }

        private void dgvMembers_DataBindingComplete(
            object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            dgvMembers.ClearSelection();
        }

        //=====================================================
        // CLEAR MEMBER FIELDS
        //=====================================================
        private void ClearFields()
        {
            selectedMemberId = string.Empty;
            txtMemberID.ReadOnly = false;
            txtMemberID.BackColor = System.Drawing.Color.White;
            txtMemberID.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            cboActive.SelectedIndex = cboActive.Items.Count > 0 ? 0 : -1;
            errorProvider1.Clear();
            dgvMembers.ClearSelection();
            btnUpdate.Enabled = false;
            txtMemberID.Focus();
        }

        //=====================================================
        // INPUT CONTROLS
        //=====================================================
        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsLetter(e.KeyChar)
                && e.KeyChar != ' '
                && e.KeyChar != '\''
                && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtName_KeyPress(sender, e);
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.TextLength > 0
                && !Regex.IsMatch(
                    txtEmail.Text.Trim(),
                    @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
            {
                errorProvider1.SetError(txtEmail, "Enter a valid email address.");
            }
            else
            {
                errorProvider1.SetError(txtEmail, string.Empty);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void frmMembers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ClearFields();
                e.SuppressKeyPress = true;
            }
        }

        //=====================================================
        // ERROR FEEDBACK
        //=====================================================
        private static void ShowError(string heading, Exception ex)
        {
            MessageBox.Show(
                heading + Environment.NewLine + Environment.NewLine + ex.Message,
                "Members Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
