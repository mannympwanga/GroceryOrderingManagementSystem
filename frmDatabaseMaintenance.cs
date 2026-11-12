using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public sealed class frmDatabaseMaintenance : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        private readonly Label lblStorage = new Label();
        private readonly Label lblFrequency = new Label();
        private readonly DataGridView dgvBackups = new DataGridView();
        private readonly Button btnBackup = new Button();
        private readonly Button btnVerify = new Button();
        private readonly Button btnRestore = new Button();
        private readonly Button btnOpenFolder = new Button();
        private readonly Button btnRefresh = new Button();
        private readonly Button btnClose = new Button();

        private string backupFolder = string.Empty;

        public frmDatabaseMaintenance()
        {
            InitializeForm();
        }

        //=====================================================
        // FORM DESIGN
        //=====================================================
        private void InitializeForm()
        {
            Text = "Database Backup and Recovery";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(1120, 700);
            MinimumSize = new Size(1000, 650);
            Font = new Font("Rockwell", 10.5F, FontStyle.Regular);

            Label title = new Label
            {
                AutoSize = false,
                BackColor = Color.Transparent,
                Font = new Font("Rockwell", 17F, FontStyle.Bold),
                Location = new Point(30, 20),
                Size = new Size(700, 40),
                Text = "DATABASE BACKUP AND RECOVERY"
            };

            lblStorage.AutoSize = false;
            lblStorage.BackColor = Color.Transparent;
            lblStorage.Location = new Point(30, 72);
            lblStorage.Size = new Size(1050, 36);
            lblStorage.Text = "Backup storage: loading...";

            lblFrequency.AutoSize = false;
            lblFrequency.BackColor = Color.Transparent;
            lblFrequency.Location = new Point(30, 108);
            lblFrequency.Size = new Size(1050, 36);
            lblFrequency.Text =
                "Backup method: manual. Frequency: weekly and before major data changes.";

            ConfigureButton(btnBackup, "CREATE BACKUP", 30, btnBackup_Click);
            ConfigureButton(btnVerify, "VERIFY SELECTED", 205, btnVerify_Click);
            ConfigureButton(btnRestore, "RESTORE SELECTED", 380, btnRestore_Click);
            ConfigureButton(btnOpenFolder, "OPEN LOCATION", 555, btnOpenFolder_Click);
            ConfigureButton(btnRefresh, "REFRESH", 730, btnRefresh_Click);
            ConfigureButton(btnClose, "CLOSE", 905, btnClose_Click);

            dgvBackups.Location = new Point(30, 220);
            dgvBackups.Size = new Size(1055, 435);
            dgvBackups.Anchor = AnchorStyles.Top |
                                AnchorStyles.Bottom |
                                AnchorStyles.Left |
                                AnchorStyles.Right;
            dgvBackups.ReadOnly = true;
            dgvBackups.AllowUserToAddRows = false;
            dgvBackups.AllowUserToDeleteRows = false;
            dgvBackups.MultiSelect = false;
            dgvBackups.RowHeadersVisible = false;
            dgvBackups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBackups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Controls.AddRange(new Control[]
            {
                title,
                lblStorage,
                lblFrequency,
                btnBackup,
                btnVerify,
                btnRestore,
                btnOpenFolder,
                btnRefresh,
                btnClose,
                dgvBackups
            });

            Load += frmDatabaseMaintenance_Load;
        }

        private void ConfigureButton(
            Button button,
            string text,
            int left,
            EventHandler clickHandler)
        {
            button.Location = new Point(left, 155);
            button.Size = new Size(160, 48);
            button.Font = new Font(Font, FontStyle.Bold);
            button.Text = text;
            button.Click += clickHandler;
        }

        //=====================================================
        // FORM LOAD AND ACCESS CONTROL
        //=====================================================
        private void frmDatabaseMaintenance_Load(object? sender, EventArgs e)
        {
            if (!UserSession.IsAdministrator)
            {
                MessageBox.Show(
                    "Only the administrator (Matron) may back up or restore the database.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                BeginInvoke(new Action(Close));
                return;
            }

            try
            {
                backupFolder = GetDefaultBackupFolder();
                lblStorage.Text = "Backup storage: " + backupFolder;
                LoadBackupHistory();
            }
            catch (Exception ex)
            {
                ShowError("The database maintenance page could not be loaded.", ex);
            }
        }

        //=====================================================
        // BACKUP LOCATION AND HISTORY
        //=====================================================
        private string GetDefaultBackupFolder()
        {
            const string sql = @"
SELECT CONVERT(nvarchar(4000),
    SERVERPROPERTY('InstanceDefaultBackupPath'));";

            using (SqlConnection con = db.GetMasterConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                string? serverFolder = Convert.ToString(cmd.ExecuteScalar());

                if (!string.IsNullOrWhiteSpace(serverFolder))
                    return serverFolder.Trim();
            }

            string fallbackFolder = Path.Combine(
                AppContext.BaseDirectory,
                "DatabaseBackups");

            Directory.CreateDirectory(fallbackFolder);
            return fallbackFolder;
        }

        private void LoadBackupHistory()
        {
            const string sql = @"
SELECT TOP (25)
    bs.backup_finish_date AS [Backup Date],
    CAST(bs.backup_size / 1048576.0 AS decimal(18, 2)) AS [Size MB],
    bmf.physical_device_name AS [Physical Path],
    CASE WHEN bs.is_copy_only = 1 THEN 'Copy-only' ELSE 'Full' END AS [Type]
FROM msdb.dbo.backupset AS bs
INNER JOIN msdb.dbo.backupmediafamily AS bmf
    ON bmf.media_set_id = bs.media_set_id
WHERE bs.database_name = @DatabaseName
  AND bs.[type] = 'D'
ORDER BY bs.backup_finish_date DESC;";

            using (SqlConnection con = db.GetMasterConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.Add(
                    "@DatabaseName",
                    SqlDbType.NVarChar,
                    128).Value = db.DatabaseName;

                DataTable backups = new DataTable();
                adapter.Fill(backups);
                dgvBackups.DataSource = backups;

                if (backups.Rows.Count == 0)
                {
                    lblFrequency.Text =
                        "No full backup is recorded. Create one before the examination.";
                }
                else
                {
                    DateTime latest =
                        Convert.ToDateTime(backups.Rows[0]["Backup Date"]);

                    int age = Math.Max(0, (DateTime.Now.Date - latest.Date).Days);
                    lblFrequency.Text =
                        $"Latest backup: {latest:g} ({age} day(s) old). " +
                        "Recommended frequency: weekly and before major data changes.";
                }
            }

            dgvBackups.ClearSelection();
        }

        //=====================================================
        // CREATE FULL DATABASE BACKUP
        //=====================================================
        private void btnBackup_Click(object? sender, EventArgs e)
        {
            string backupPath = Path.Combine(
                backupFolder,
                db.DatabaseName + "_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak");

            string sql = $@"
BACKUP DATABASE {QuoteIdentifier(db.DatabaseName)}
TO DISK = @BackupPath
WITH COPY_ONLY, INIT, CHECKSUM, STATS = 10;";

            try
            {
                using (SqlConnection con = db.GetMasterConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(
                        "@BackupPath",
                        SqlDbType.NVarChar,
                        4000).Value = backupPath;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                VerifyBackup(backupPath);
                LoadBackupHistory();

                MessageBox.Show(
                    "The full database backup was created and verified." +
                    Environment.NewLine + Environment.NewLine + backupPath,
                    "Backup Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError("The database backup could not be created.", ex);
            }
        }

        //=====================================================
        // VERIFY BACKUP
        //=====================================================
        private void btnVerify_Click(object? sender, EventArgs e)
        {
            string? backupPath = GetSelectedBackupPath();

            if (backupPath == null)
                return;

            try
            {
                VerifyBackup(backupPath);

                MessageBox.Show(
                    "SQL Server verified the selected backup successfully.",
                    "Backup Verified",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError("The selected backup could not be verified.", ex);
            }
        }

        private void VerifyBackup(string backupPath)
        {
            const string sql = @"
RESTORE VERIFYONLY
FROM DISK = @BackupPath
WITH CHECKSUM;";

            using (SqlConnection con = db.GetMasterConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(
                    "@BackupPath",
                    SqlDbType.NVarChar,
                    4000).Value = backupPath;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //=====================================================
        // RESTORE DATABASE AND VERIFY APPLICATION DATA
        //=====================================================
        private void btnRestore_Click(object? sender, EventArgs e)
        {
            string? backupPath = GetSelectedBackupPath();

            if (backupPath == null)
                return;

            DialogResult confirmation = MessageBox.Show(
                "Restoring will replace the current database with the selected backup. " +
                "All users must be out of the system. Continue?",
                "Confirm Database Restore",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
                return;

            string databaseIdentifier = QuoteIdentifier(db.DatabaseName);
            string restoreSql = $@"
ALTER DATABASE {databaseIdentifier}
SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

RESTORE DATABASE {databaseIdentifier}
FROM DISK = @BackupPath
WITH REPLACE, RECOVERY;

ALTER DATABASE {databaseIdentifier}
SET MULTI_USER;";

            try
            {
                VerifyBackup(backupPath);

                using (SqlConnection con = db.GetMasterConnection())
                using (SqlCommand cmd = new SqlCommand(restoreSql, con))
                {
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add(
                        "@BackupPath",
                        SqlDbType.NVarChar,
                        4000).Value = backupPath;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                string verification = VerifyRecoveredData();
                LoadBackupHistory();

                MessageBox.Show(
                    "The database was restored successfully." +
                    Environment.NewLine + Environment.NewLine +
                    verification,
                    "Restore Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                TryReturnDatabaseToMultiUser();
                ShowError("The database could not be restored.", ex);
            }
        }

        private string VerifyRecoveredData()
        {
            const string sql = @"
SELECT
    (SELECT COUNT(*) FROM dbo.tblMembers) AS Members,
    (SELECT COUNT(*) FROM dbo.tblGroceryItems) AS Items,
    (SELECT COUNT(*) FROM dbo.tblMemberOrders) AS OrderLines,
    (SELECT COUNT(*) FROM dbo.tblUsers) AS Users;";

            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException(
                            "The recovered database could not be verified.");
                    }

                    return string.Format(
                        "Verification counts: {0} members, {1} items, " +
                        "{2} order lines and {3} users.",
                        reader["Members"],
                        reader["Items"],
                        reader["OrderLines"],
                        reader["Users"]);
                }
            }
        }

        //=====================================================
        // ATTEMPT TO RETURN DATABASE TO MULTI_USER MODE
        //=====================================================
        private void TryReturnDatabaseToMultiUser()
        {
            try
            {
                string sql = $@"
IF DB_ID(@DatabaseName) IS NOT NULL
    ALTER DATABASE {QuoteIdentifier(db.DatabaseName)} SET MULTI_USER;";

                using (SqlConnection con = db.GetMasterConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.Add(
                        "@DatabaseName",
                        SqlDbType.NVarChar,
                        128).Value = db.DatabaseName;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                
            }
        }

        //=====================================================
        // FORM CONTROLS
        //=====================================================
        private string? GetSelectedBackupPath()
        {
            if (dgvBackups.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Select a backup from the history table first.",
                    "Backup Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return null;
            }

            return Convert.ToString(
                dgvBackups.SelectedRows[0].Cells["Physical Path"].Value);
        }

        private void btnOpenFolder_Click(object? sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = backupFolder,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ShowError("The backup location could not be opened.", ex);
            }
        }

        //=====================================================
        // ERROR HANDLING AND FORM CLOSURE
        //=====================================================
        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            try
            {
                LoadBackupHistory();
            }
            catch (Exception ex)
            {
                ShowError("The backup history could not be refreshed.", ex);
            }
        }

        private void btnClose_Click(object? sender, EventArgs e)
        {
            Close();
        }

        //=====================================================
        // HELPER METHODS
        //=====================================================
        private static string QuoteIdentifier(string identifier)
        {
            return "[" + identifier.Replace("]", "]]", StringComparison.Ordinal) + "]";
        }

        //=====================================================
        // ERROR HANDLING
        //=====================================================
        private static void ShowError(string heading, Exception ex)
        {
            MessageBox.Show(
                heading + Environment.NewLine + Environment.NewLine + ex.Message,
                "Database Maintenance Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
