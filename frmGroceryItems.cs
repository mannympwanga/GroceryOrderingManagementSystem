using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public partial class frmGroceryItems : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();
        private string selectedItemId = string.Empty;

        public frmGroceryItems()
        {
            InitializeComponent();
        }

        //=====================================================
        // FORM LOAD AND ACCESS CONTROL
        //=====================================================
        private void frmGroceryItems_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(imagesFolder);
            txtImage.ReadOnly = true;

            if (!HasAdministrativeAccess())
            {
                MessageBox.Show(
                    "Only the administrator (Matron) may maintain grocery items.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                BeginInvoke(new Action(Close));
                return;
            }

            try
            {
                cboAvailable.Items.Clear();
                cboAvailable.Items.AddRange(new object[] { "Y", "N" });
                cboAvailable.SelectedIndex = 0;
                UpdateAvailabilityStatus();

                LoadItems();
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                ShowError("The grocery items could not be loaded.", ex);
            }
        }

        private bool HasAdministrativeAccess()
        {
            return UserSession.IsAdministrator;
        }

        //=====================================================
        // LOAD AND DISPLAY GROCERY ITEMS
        //=====================================================
        private void LoadItems(string searchText = "")
        {
            const string baseSql = @"
SELECT ItemID, Description, Price, [Image], Available
FROM dbo.tblGroceryItems";

            bool searching = !string.IsNullOrWhiteSpace(searchText);
            string sql = baseSql;

            if (searching)
            {
                sql += @"
WHERE ItemID LIKE @Search
   OR Description LIKE @Search
   OR Available LIKE @Search";
            }

            sql += "\nORDER BY ItemID;";

            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                if (searching)
                {
                    cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 120).Value =
                        "%" + searchText.Trim() + "%";
                }

                DataTable items = new DataTable();
                adapter.Fill(items);
                dgvItems.DataSource = items;
                FormatGrid();

                lblRecordCount.Text = items.Rows.Count +
                    (items.Rows.Count == 1 ? " item shown" : " items shown");
            }
        }

        private void FormatGrid()
        {
            dgvItems.ReadOnly = true;
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.MultiSelect = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvItems.Columns.Contains("ItemID"))
            {
                dgvItems.Columns["ItemID"].HeaderText = "Item ID";
                dgvItems.Columns["ItemID"].FillWeight = 65;
            }

            if (dgvItems.Columns.Contains("Description"))
            {
                dgvItems.Columns["Description"].HeaderText = "Description";
                dgvItems.Columns["Description"].FillWeight = 130;
            }

            if (dgvItems.Columns.Contains("Price"))
            {
                dgvItems.Columns["Price"].HeaderText = "Price";
                dgvItems.Columns["Price"].DefaultCellStyle.Format = "C2";
                dgvItems.Columns["Price"].DefaultCellStyle.FormatProvider =
        CultureInfo.GetCultureInfo("en-ZA");
                dgvItems.Columns["Price"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
                dgvItems.Columns["Price"].FillWeight = 60;
            }

            if (dgvItems.Columns.Contains("Image"))
            {
                dgvItems.Columns["Image"].HeaderText = "Image path";
                dgvItems.Columns["Image"].FillWeight = 150;
            }

            if (dgvItems.Columns.Contains("Available"))
            {
                dgvItems.Columns["Available"].HeaderText = "Available";
                dgvItems.Columns["Available"].FillWeight = 55;
            }

            dgvItems.ClearSelection();
        }

        //=====================================================
        // GROCERY ITEM VALIDATION
        //=====================================================
        private bool ValidateFields(out decimal price)
        {
            price = 0m;
            errorProvider1.Clear();
            Control firstInvalidControl = null;

            void SetValidationError(Control control, string message)
            {
                errorProvider1.SetError(control, message);
                if (firstInvalidControl == null)
                    firstInvalidControl = control;
            }

            string itemId = txtItemID.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (itemId.Length == 0)
                SetValidationError(txtItemID, "Item ID is required.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(
                itemId,
                @"^[A-Za-z0-9-]{1,20}$"))
            {
                SetValidationError(txtItemID, "Use only letters, numbers and hyphens.");
            }

            if (description.Length == 0)
                SetValidationError(txtDescription, "Description is required.");

            if (!TryReadPrice(out price) || price <= 0)
                SetValidationError(txtPrice, "Enter a price greater than zero.");

            if (cboAvailable.SelectedIndex < 0)
                SetValidationError(cboAvailable, "Select Y or N.");

            if (firstInvalidControl != null)
            {
                MessageBox.Show(
                    "Please correct the highlighted grocery item details.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                firstInvalidControl.Focus();
                return false;
            }

            return true;
        }

        private bool TryReadPrice(out decimal price)
        {
            string value = txtPrice.Text.Trim()
                .Replace("R", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Trim();

            return decimal.TryParse(
                       value,
                       NumberStyles.Number,
                       CultureInfo.CurrentCulture,
                       out price)
                || decimal.TryParse(
                       value.Replace(',', '.'),
                       NumberStyles.Number,
                       CultureInfo.InvariantCulture,
                       out price);
        }

        //=====================================================
        // DATABASE HELPERS
        //=====================================================
        private bool ItemExists(string itemId)
        {
            const string sql = @"
SELECT COUNT(*)
FROM dbo.tblGroceryItems
WHERE ItemID = @ItemID;";

            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@ItemID", SqlDbType.VarChar, 20).Value = itemId;
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void AddItemParameters(SqlCommand cmd, decimal price)
        {
            cmd.Parameters.Add("@ItemID", SqlDbType.VarChar, 20).Value =
                txtItemID.Text.Trim().ToUpperInvariant();
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 150).Value =
                txtDescription.Text.Trim();

            SqlParameter priceParameter = cmd.Parameters.Add("@Price", SqlDbType.Decimal);
            priceParameter.Precision = 10;
            priceParameter.Scale = 2;
            priceParameter.Value = price;

            cmd.Parameters.Add("@Image", SqlDbType.NVarChar, 255).Value = txtImage.Text.Trim();
            cmd.Parameters.Add("@Available", SqlDbType.Char, 1).Value = cboAvailable.Text;
        }

        //=====================================================
        // ADD GROCERY ITEM
        //=====================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields(out decimal price))
                return;

            string itemId = txtItemID.Text.Trim().ToUpperInvariant();

            try
            {
                if (ItemExists(itemId))
                {
                    MessageBox.Show(
                        "That Item ID already exists. Select the item and use Update.",
                        "Duplicate Item ID",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtItemID.Focus();
                    return;
                }

                const string sql = @"
INSERT INTO dbo.tblGroceryItems
    (ItemID, Description, Price, [Image], Available)
VALUES
    (@ItemID, @Description, @Price, @Image, @Available);";

                using (SqlConnection con = db.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    AddItemParameters(cmd, price);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "The grocery item was added successfully.",
                    "Item Added",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();
                LoadItems();
            }
            catch (SqlException ex)
            {
                ShowError("The grocery item could not be added.", ex);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while adding the item.", ex);
            }
        }

        //=====================================================
        // UPDATE GROCERY ITEM
        //=====================================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateFields(out decimal price))
                return;

            string itemId = string.IsNullOrWhiteSpace(selectedItemId)
                ? txtItemID.Text.Trim().ToUpperInvariant()
                : selectedItemId;

            try
            {
                if (!ItemExists(itemId))
                {
                    MessageBox.Show(
                        "Select an existing item from the table before updating.",
                        "Item Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmation = MessageBox.Show(
                    "Save the changes to item " + itemId + "?",
                    "Confirm Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmation != DialogResult.Yes)
                    return;

                const string sql = @"
UPDATE dbo.tblGroceryItems
SET Description = @Description,
    Price = @Price,
    [Image] = @Image,
    Available = @Available
WHERE ItemID = @OriginalItemID;";

                using (SqlConnection con = db.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    AddItemParameters(cmd, price);
                    cmd.Parameters.Add("@OriginalItemID", SqlDbType.VarChar, 20).Value = itemId;
                    con.Open();

                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("No matching grocery item was updated.");
                }

                MessageBox.Show(
                    "The grocery item was updated successfully.",
                    "Item Updated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearFields();
                LoadItems();
            }
            catch (SqlException ex)
            {
                ShowError("The grocery item could not be updated.", ex);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while updating the item.", ex);
            }
        }

        //=====================================================
        // SELECT AND COPY ITEM IMAGE
        //=====================================================
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemID.Text))
            {
                MessageBox.Show(
                    "Enter the Item ID before selecting an image.",
                    "Item ID Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtItemID.Focus();
                return;
            }

            using OpenFileDialog open = new OpenFileDialog();

            open.Title = "Select Grocery Item Image";
            open.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files|*.*";
            open.CheckFileExists = true;

            if (open.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                Directory.CreateDirectory(imagesFolder);

                // Use the Item ID as the filename, for example BR0001.jpg
                string extension =
                    Path.GetExtension(open.FileName).ToLowerInvariant();

                string fileName =
                    txtItemID.Text.Trim() + extension;

                string destinationPath =
                    Path.Combine(imagesFolder, fileName);

                // Avoid copying a file onto itself
                if (!string.Equals(
                        Path.GetFullPath(open.FileName),
                        Path.GetFullPath(destinationPath),
                        StringComparison.OrdinalIgnoreCase))
                {
                    File.Copy(
                        open.FileName,
                        destinationPath,
                        true);
                }

                // Only the filename will be stored in SQL Server
                txtImage.Text = fileName;

                LoadImage(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The image could not be copied.\n\n" + ex.Message,
                    "Image Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        //=====================================================
        // DISPLAY ITEM IMAGE
        //=====================================================
        private void LoadImage(string storedFileName)
        {
            try
            {
                if (picItem.Image != null)
                {
                    Image oldImage = picItem.Image;
                    picItem.Image = null;
                    oldImage.Dispose();
                }

                if (string.IsNullOrWhiteSpace(storedFileName))
                    return;

                // Remove any unwanted path information
                string fileName = Path.GetFileName(storedFileName);

                // C# creates the full path here
                string fullPath =
                    Path.Combine(imagesFolder, fileName);

                if (!File.Exists(fullPath))
                    return;

                // Clone the image so that the file is not locked
                using FileStream stream = new FileStream(
                    fullPath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite);

                using Image temporaryImage =
                    Image.FromStream(stream);

                picItem.Image =
                new Bitmap(temporaryImage);

                picItem.SizeMode =
                PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                picItem.Image = null;

                MessageBox.Show(
                    "The image could not be displayed.\n\n" + ex.Message,
                    "Image Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void DisposeCurrentImage()
        {
            Image current = picItem.Image;
            picItem.Image = null;
            current?.Dispose();
        }

        //=====================================================
        // SEARCH, REFRESH AND CLOSE
        //=====================================================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadItems(txtSearch.Text);
            }
            catch (Exception ex)
            {
                ShowError("The item search could not be completed.", ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                ClearFields();
                LoadItems();
            }
            catch (Exception ex)
            {
                ShowError("The grocery item list could not be refreshed.", ex);
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
        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count)
                return;

            DataGridViewRow row = dgvItems.Rows[e.RowIndex];

            selectedItemId = Convert.ToString(row.Cells["ItemID"].Value) ?? string.Empty;
            txtItemID.Text = selectedItemId;
            txtDescription.Text = Convert.ToString(row.Cells["Description"].Value) ?? string.Empty;

            if (decimal.TryParse(Convert.ToString(row.Cells["Price"].Value), out decimal price))
                txtPrice.Text = price.ToString("0.00");
            else
                txtPrice.Text = Convert.ToString(row.Cells["Price"].Value) ?? string.Empty;

            txtImage.Text = Convert.ToString(row.Cells["Image"].Value) ?? string.Empty;
            cboAvailable.Text = Convert.ToString(row.Cells["Available"].Value) ?? "Y";
            LoadImage(txtImage.Text);

            txtItemID.ReadOnly = true;
            txtItemID.BackColor = Color.Gainsboro;
            btnUpdate.Enabled = true;
        }

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvItems_CellClick(sender, e);
        }

        private void dgvItems_DataBindingComplete(
            object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            dgvItems.ClearSelection();
        }

        //=====================================================
        // CLEAR ITEM FIELDS
        //=====================================================
        private void ClearFields()
        {
            selectedItemId = string.Empty;
            txtItemID.ReadOnly = false;
            txtItemID.BackColor = Color.White;
            txtItemID.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtImage.Clear();
            cboAvailable.SelectedIndex = cboAvailable.Items.Count > 0 ? 0 : -1;
            DisposeCurrentImage();
            lblImageState.Text = "No image selected";
            errorProvider1.Clear();
            dgvItems.ClearSelection();
            btnUpdate.Enabled = false;
            UpdateAvailabilityStatus();
            txtItemID.Focus();
        }

        //=====================================================
        // INPUT CONTROLS AND STATUS
        //=====================================================
        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalSeparator = Convert.ToChar(
                CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != decimalSeparator
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if ((e.KeyChar == decimalSeparator || e.KeyChar == '.')
                && (txtPrice.Text.Contains(decimalSeparator.ToString())
                    || txtPrice.Text.Contains('.')))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            if (TryReadPrice(out decimal price))
            {
                txtPrice.Text = price.ToString("0.00");
                errorProvider1.SetError(txtPrice, string.Empty);
            }
            else if (txtPrice.TextLength > 0)
            {
                errorProvider1.SetError(txtPrice, "Enter a valid price.");
            }
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsLetterOrDigit(e.KeyChar)
                && e.KeyChar != ' '
                && e.KeyChar != '-'
                && e.KeyChar != '&'
                && e.KeyChar != '\''
                && e.KeyChar != '/')
            {
                e.Handled = true;
            }
        }

        private void txtImage_TextChanged(object sender, EventArgs e)
        {
            if (txtImage.Focused)
                LoadImage(txtImage.Text.Trim());
        }

        private void cboAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAvailabilityStatus();
        }

        private void UpdateAvailabilityStatus()
        {
            bool available = cboAvailable.Text == "Y";
            lblStatus.Text = available
                ? "Item is available for ordering."
                : "Item is unavailable and cannot be ordered.";
            lblStatus.ForeColor = available ? Color.DarkGreen : Color.Firebrick;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void frmGroceryItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ClearFields();
                e.SuppressKeyPress = true;
            }
        }

        private readonly string imagesFolder = Path.Combine(AppContext.BaseDirectory, "Images");

        //=====================================================
        // CLEANUP AND ERROR FEEDBACK
        //=====================================================
        private void frmGroceryItems_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeCurrentImage();
        }

        private static void ShowError(string heading, Exception ex)
        {
            MessageBox.Show(
                heading + Environment.NewLine + Environment.NewLine + ex.Message,
                "Grocery Items Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void lblImageState_Click(object sender, EventArgs e)
        {

        }
    }
}
