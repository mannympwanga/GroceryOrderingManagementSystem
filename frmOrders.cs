using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public partial class frmOrders : Form
    {
        private static readonly CultureInfo SouthAfricanCulture =
            CultureInfo.GetCultureInfo("en-ZA");

        private static readonly TimeSpan ThursdayCutOff =
            new TimeSpan(18, 0, 0);

        private readonly DatabaseHelper db = new DatabaseHelper();
        private readonly List<int> deletedOrderIds = new List<int>();
        private readonly string imageFolder =
            Path.Combine(AppContext.BaseDirectory, "Images");

        
        private readonly PictureBox picItemImage = new PictureBox();

        private bool isLoading;
        private bool hasUnsavedChanges;
        private DateTime activeOrderDate = DateTime.Today;

        public frmOrders()
        {
            InitializeComponent();
            CreateItemImageControl();
        }

        //=====================================================
        // SELECTED GROCERY ITEM IMAGE
        //=====================================================
        private void CreateItemImageControl()
        {
            Control imageContainer = cboItem.Parent ?? this;

            picItemImage.Name = "picItemImage";
            picItemImage.BackColor = Color.White;
            picItemImage.BorderStyle = BorderStyle.FixedSingle;
            picItemImage.SizeMode = PictureBoxSizeMode.Zoom;
            picItemImage.Size = new Size(cboItem.Width, 190);

            int imageTop = ReferenceEquals(btnAddToOrder.Parent, imageContainer)
                ? btnAddToOrder.Bottom + 15
                : cboItem.Bottom + 100;

            picItemImage.Location = new Point(cboItem.Left, imageTop);
            picItemImage.Anchor = AnchorStyles.Top |
                                  AnchorStyles.Left |
                                  AnchorStyles.Right;
            picItemImage.TabStop = false;

            imageContainer.Controls.Add(picItemImage);
            picItemImage.BringToFront();
        }

        //=====================================================
        // FORM LOAD AND ACCESS CONTROL
        //=====================================================
        private void frmOrders_Load(object sender, EventArgs e)
        {
            if (!UserSession.IsMember && !UserSession.IsAdministrator)
            {
                MessageBox.Show(
                    "Please log in before opening the order form.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                BeginInvoke(new Action(Close));
                return;
            }

            try
            {
                isLoading = true;

                FormatGrid();
                LoadMembers();
                LoadAvailableItems();
                dtpOrderDate.Value = DateTime.Today;
                activeOrderDate = DateTime.Today;
                nudQuantity.Value = 1;
                lblGrandTotal.Text = "Grand total: R0,00";

                ApplyMemberAccess();
                LoadCurrentWeekOrder();

                UpdateOrderingStatus();
                SetOrderControlsEnabled(IsOrderingOpen(DateTime.Now, out _));

                hasUnsavedChanges = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The order form could not be loaded.\n\n" + ex.Message,
                    "Loading Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }

        //=====================================================
        // LOAD MEMBERS AND ITEMS
        //=====================================================
        private void LoadMembers()
        {
            const string sql = @"
                SELECT
                    MemberID,
                    MemberID + ' - ' + Name + ' ' + Surname AS MemberDisplay
                FROM tblMembers
                WHERE Active = 'Y'
                ORDER BY Name, Surname;";

            using (SqlConnection con = db.GetConnection())
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, con))
            {
                DataTable members = new DataTable();
                adapter.Fill(members);

                cboMember.DataSource = members;
                cboMember.DisplayMember = "MemberDisplay";
                cboMember.ValueMember = "MemberID";
                cboMember.SelectedIndex = -1;
            }
        }

        private void LoadAvailableItems()
        {
            string? selectedItemId = cboItem.SelectedValue?.ToString();

            const string sql = @"
                SELECT ItemID, Description, Price, [Image]
                FROM tblGroceryItems
                WHERE Available = 'Y'
                ORDER BY Description;";

            using (SqlConnection con = db.GetConnection())
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, con))
            {
                DataTable items = new DataTable();
                adapter.Fill(items);

                cboItem.DataSource = items;
                cboItem.DisplayMember = "Description";
                cboItem.ValueMember = "ItemID";
                cboItem.SelectedIndex = -1;

                if (!string.IsNullOrWhiteSpace(selectedItemId))
                {
                    cboItem.SelectedValue = selectedItemId;
                }
            }
        }

        //=====================================================
        // APPLY MEMBER ACCESS CONTROL
        //=====================================================
        private void ApplyMemberAccess()
        {
            if (UserSession.IsMember)
            {
                cboMember.SelectedValue = UserSession.MemberID;

                if (cboMember.SelectedIndex == -1)
                {
                    throw new InvalidOperationException(
                        "The logged-in member is inactive or could not be found.");
                }

                cboMember.Enabled = false;
                lblMemberHelp.Text = "Logged-in member (cannot be changed)";
            }
            else
            {
                cboMember.Enabled = true;
                lblMemberHelp.Text = "Administrator: select the member placing the order";
            }
        }

        //=====================================================
        // LOAD THE MEMBER'S EXISTING ORDER FOR THIS WEEK
        //=====================================================
        private static DateTime GetWeekStart(DateTime date)
        {
            int daysSinceMonday =
                ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

            return date.Date.AddDays(-daysSinceMonday);
        }

        private void LoadCurrentWeekOrder()
        {
            dgvOrder.Rows.Clear();
            deletedOrderIds.Clear();
            activeOrderDate = DateTime.Today;
            dtpOrderDate.Value = activeOrderDate;

            if (cboMember.SelectedIndex == -1 || cboMember.SelectedValue == null)
            {
                lblOrderMode.Text = "Select a member to load an order.";
                CalculateGrandTotal();
                hasUnsavedChanges = false;
                SetMemberSelectionState();
                return;
            }

            string memberId = cboMember.SelectedValue.ToString()!;
            DateTime weekStart = GetWeekStart(DateTime.Today);
            DateTime fridayStart = weekStart.AddDays(4);

            const string sql = @"
                SELECT
                    mo.OrderID,
                    mo.OrderDate,
                    mo.ItemID,
                    gi.Description,
                    CASE
                        WHEN mo.Quantity = 0 THEN CAST(0 AS decimal(18, 2))
                        ELSE CAST(mo.TotalPrice / mo.Quantity AS decimal(18, 2))
                    END AS UnitPrice,
                    mo.Quantity,
                    mo.TotalPrice
                FROM tblMemberOrders AS mo
                INNER JOIN tblGroceryItems AS gi
                    ON gi.ItemID = mo.ItemID
                WHERE mo.MemberID = @MemberID
                  AND mo.OrderDate >= @WeekStart
                  AND mo.OrderDate < @FridayStart
                ORDER BY mo.OrderID;";

            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@MemberID", SqlDbType.NVarChar, 50).Value =
                    memberId;

                cmd.Parameters.Add("@WeekStart", SqlDbType.Date).Value =
                    weekStart;

                cmd.Parameters.Add("@FridayStart", SqlDbType.Date).Value =
                    fridayStart;

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    bool firstRow = true;

                    while (reader.Read())
                    {
                        DateTime storedOrderDate =
                            Convert.ToDateTime(reader["OrderDate"]).Date;

                        if (firstRow)
                        {
                            activeOrderDate = storedOrderDate;
                            dtpOrderDate.Value = storedOrderDate;
                            firstRow = false;
                        }

                        dgvOrder.Rows.Add(
                            Convert.ToInt32(reader["OrderID"]),
                            Convert.ToString(reader["ItemID"]),
                            Convert.ToString(reader["Description"]),
                            Convert.ToDecimal(reader["UnitPrice"]),
                            Convert.ToInt32(reader["Quantity"]),
                            Convert.ToDecimal(reader["TotalPrice"]));
                    }
                }
            }

            hasUnsavedChanges = false;

            if (dgvOrder.Rows.Count == 0)
            {
                lblOrderMode.Text =
                    "New weekly order — Save will add it to the database.";
            }
            else
            {
                lblOrderMode.Text =
                    "Existing weekly order loaded — Save will update it.";
            }

            CalculateGrandTotal();
            SetMemberSelectionState();
        }

        //=====================================================
        // ORDERING PERIOD
        // Orders may be placed Monday to Thursday before 18:00.
        // Ordering is closed from Thursday 18:00 through Sunday.
        //=====================================================
        private bool IsOrderingOpen(DateTime dateTime, out string message)
        {
            bool isOrderingDay =
                dateTime.DayOfWeek == DayOfWeek.Monday ||
                dateTime.DayOfWeek == DayOfWeek.Tuesday ||
                dateTime.DayOfWeek == DayOfWeek.Wednesday ||
                dateTime.DayOfWeek == DayOfWeek.Thursday;

            bool afterThursdayCutOff =
                dateTime.DayOfWeek == DayOfWeek.Thursday &&
                dateTime.TimeOfDay >= ThursdayCutOff;

            if (!isOrderingDay || afterThursdayCutOff)
            {
                message =
                    "Ordering is closed. Weekly orders may only be placed " +
                    "from Monday until Thursday at 18:00.";

                return false;
            }

            message =
                "Ordering is open. The weekly cut-off is Thursday at 18:00.";
            return true;
        }

        private bool EnforceOrderingPeriod()
        {
            if (IsOrderingOpen(DateTime.Now, out string message))
            {
                return true;
            }

            MessageBox.Show(
                message,
                "Ordering Period Closed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            UpdateOrderingStatus();
            SetOrderControlsEnabled(false);
            return false;
        }

        private void UpdateOrderingStatus()
        {
            bool isOpen = IsOrderingOpen(DateTime.Now, out string message);

            lblOrderingStatus.Text = message;
            lblOrderingStatus.ForeColor = isOpen
                ? Color.DarkGreen
                : Color.DarkRed;
        }

        private void SetOrderControlsEnabled(bool enabled)
        {
            cboItem.Enabled = enabled;
            nudQuantity.Enabled = enabled;
            btnAddToOrder.Enabled = enabled;
            btnRemove.Enabled = enabled;
            btnClear.Enabled = enabled;
            btnSave.Enabled = enabled;

            if (UserSession.IsAdministrator)
            {
                cboMember.Enabled = enabled && dgvOrder.Rows.Count == 0;
            }
        }

        //=====================================================
        // ITEM AVAILABILITY
        //=====================================================
        private bool TryGetAvailableItem(
            SqlConnection con,
            SqlTransaction? transaction,
            string itemId,
            out string description,
            out decimal price)
        {
            description = "";
            price = 0m;

            const string sql = @"
                SELECT Description, Price, Available
                FROM tblGroceryItems WITH (UPDLOCK, HOLDLOCK)
                WHERE ItemID = @ItemID;";

            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                cmd.Parameters.Add("@ItemID", SqlDbType.NVarChar, 50).Value = itemId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return false;
                    }

                    string available =
                        Convert.ToString(reader["Available"])?.Trim() ?? "N";

                    if (!available.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }

                    description = Convert.ToString(reader["Description"]) ?? "";
                    price = Convert.ToDecimal(reader["Price"]);
                    return true;
                }
            }
        }

        private bool TryGetAvailableItem(
            string itemId,
            out string description,
            out decimal price)
        {
            using (SqlConnection con = db.GetConnection())
            {
                con.Open();
                return TryGetAvailableItem(
                    con,
                    null,
                    itemId,
                    out description,
                    out price);
            }
        }

        //=====================================================
        // ITEM AND QUANTITY CONTROLS
        //=====================================================
        private void cboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading || cboItem.SelectedItem is not DataRowView item)
            {
                txtPrice.Clear();
                txtLineTotal.Clear();
                ClearItemImage();
                return;
            }

            decimal price = Convert.ToDecimal(item["Price"]);
            txtPrice.Text = price.ToString("0.00", SouthAfricanCulture);

            string imageFileName =
                item["Image"] == DBNull.Value
                    ? ""
                    : Convert.ToString(item["Image"]) ?? "";

            DisplayItemImage(imageFileName);
            CalculateLineTotal();
        }

        private void DisplayItemImage(string storedImageName)
        {
            ClearItemImage();

            if (string.IsNullOrWhiteSpace(storedImageName))
            {
                return;
            }

            try
            {
                // Path.GetFileName also protects the application if an older
                // database record still contains a complete C:\\... path.
                string fileName = Path.GetFileName(storedImageName.Trim());
                string fullPath = Path.Combine(imageFolder, fileName);

                if (!File.Exists(fullPath))
                {
                    picItemImage.BackColor = Color.MistyRose;
                    picItemImage.Tag = "Missing image: " + fileName;
                    return;
                }

                using (FileStream stream = new FileStream(
                    fullPath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite))
                using (Image temporaryImage = Image.FromStream(stream))
                {
                    // Clone the image so that Windows does not keep the image
                    // file locked after it has been displayed.
                    picItemImage.Image = new Bitmap(temporaryImage);
                }

                picItemImage.BackColor = Color.White;
                picItemImage.Tag = fileName;
            }
            catch (ArgumentException)
            {
                // System.Drawing cannot normally decode AVIF or WEBP files.
                // Convert these files to PNG/JPG and update the SQL filename.
                picItemImage.BackColor = Color.LightGoldenrodYellow;
                picItemImage.Tag =
                    "Unsupported image format. Convert this image to PNG or JPG.";
            }
            catch (Exception ex)
            {
                picItemImage.BackColor = Color.MistyRose;
                picItemImage.Tag = ex.Message;
            }
        }

        //=====================================================
        // CLEAR ITEM IMAGE
        //=====================================================

        private void ClearItemImage()
        {
            if (picItemImage.Image != null)
            {
                Image previousImage = picItemImage.Image;
                picItemImage.Image = null;
                previousImage.Dispose();
            }

            picItemImage.BackColor = Color.White;
            picItemImage.Tag = null;
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            CalculateLineTotal();
        }

        //=====================================================
        // CALCULATE LINE TOTAL
        //=====================================================
        private void CalculateLineTotal()
        {
            if (!decimal.TryParse(
                txtPrice.Text,
                NumberStyles.Number,
                SouthAfricanCulture,
                out decimal price))
            {
                txtLineTotal.Clear();
                return;
            }

            decimal lineTotal = price * nudQuantity.Value;
            txtLineTotal.Text = lineTotal.ToString("0.00", SouthAfricanCulture);
        }

        //=====================================================
        // VALIDATE ORDER ENTRY
        //=====================================================
        private bool ValidateOrderEntry()
        {
            errorProvider1.Clear();

            if (cboMember.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboMember, "Select an active member.");
                cboMember.Focus();
                return false;
            }

            if (cboItem.SelectedIndex == -1 || cboItem.SelectedValue == null)
            {
                errorProvider1.SetError(cboItem, "Select an available grocery item.");
                cboItem.Focus();
                return false;
            }

            if (nudQuantity.Value < 1 || nudQuantity.Value > 12)
            {
                errorProvider1.SetError(
                    nudQuantity,
                    "The quantity must be from 1 to 12.");

                nudQuantity.Focus();
                return false;
            }

            return true;
        }

        //=====================================================
        // ADD ITEM
        //=====================================================
        private void btnAddToOrder_Click(object sender, EventArgs e)
        {
            if (!EnforceOrderingPeriod() || !ValidateOrderEntry())
            {
                return;
            }

            string itemId = cboItem.SelectedValue!.ToString()!;
            int quantityToAdd = Decimal.ToInt32(nudQuantity.Value);

            try
            {
                // Availability and current price are checked again here,
                // immediately before the item is placed in the order.
                if (!TryGetAvailableItem(
                    itemId,
                    out string description,
                    out decimal currentPrice))
                {
                    MessageBox.Show(
                        "This grocery item is no longer available and cannot be ordered.",
                        "Item Unavailable",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    LoadAvailableItems();
                    return;
                }

                DataGridViewRow? existingRow = FindOrderRow(itemId);

                if (existingRow != null)
                {
                    int existingQuantity =
                        Convert.ToInt32(existingRow.Cells["Quantity"].Value);

                    int combinedQuantity = existingQuantity + quantityToAdd;

                    if (combinedQuantity > 12)
                    {
                        MessageBox.Show(
                            $"You already ordered {existingQuantity} of this item. " +
                            "A member may order no more than 12 of the same grocery item.",
                            "Maximum Quantity Reached",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    existingRow.Cells["Description"].Value = description;
                    existingRow.Cells["Price"].Value = currentPrice;
                    existingRow.Cells["Quantity"].Value = combinedQuantity;
                    existingRow.Cells["Total"].Value =
                        currentPrice * combinedQuantity;
                }
                else
                {
                    dgvOrder.Rows.Add(
                        null,
                        itemId,
                        description,
                        currentPrice,
                        quantityToAdd,
                        currentPrice * quantityToAdd);
                }

                hasUnsavedChanges = true;
                SetMemberSelectionState();
                CalculateGrandTotal();
                ResetItemEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The item could not be added.\n\n" + ex.Message,
                    "Add Item Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private DataGridViewRow? FindOrderRow(string itemId)
        {
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (string.Equals(
                    Convert.ToString(row.Cells["ItemID"].Value),
                    itemId,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return row;
                }
            }

            return null;
        }

        //=====================================================
        // REMOVE AND CLEAR
        //=====================================================
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!EnforceOrderingPeriod())
            {
                return;
            }

            if (dgvOrder.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Select an item in the order list first.",
                    "Remove Item",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            DataGridViewRow selectedRow = dgvOrder.SelectedRows[0];
            RememberDeletedOrderId(selectedRow);
            dgvOrder.Rows.Remove(selectedRow);
            hasUnsavedChanges = true;

            CalculateGrandTotal();
            SetMemberSelectionState();
        }

        //=====================================================
        // CLEAR ORDER
        //=====================================================
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!EnforceOrderingPeriod())
            {
                return;
            }

            if (dgvOrder.Rows.Count == 0)
            {
                ResetItemEntry();
                return;
            }

            DialogResult result = MessageBox.Show(
                "Clear every item from the current order?",
                "Clear Order",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ClearOrder();
            }
        }

        private void ClearOrder()
        {
            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                RememberDeletedOrderId(row);
            }

            dgvOrder.Rows.Clear();
            hasUnsavedChanges = deletedOrderIds.Count > 0;

            CalculateGrandTotal();
            ResetItemEntry();
            SetMemberSelectionState();

            lblOrderMode.Text = hasUnsavedChanges
                ? "Order cleared — Save to remove it from the database."
                : "New weekly order — Save will add it to the database.";
        }

        private void RememberDeletedOrderId(DataGridViewRow row)
        {
            object? value = row.Cells["OrderID"].Value;

            if (value != null &&
                value != DBNull.Value &&
                int.TryParse(value.ToString(), out int orderId) &&
                !deletedOrderIds.Contains(orderId))
            {
                deletedOrderIds.Add(orderId);
            }
        }

        private void ResetItemEntry()
        {
            cboItem.SelectedIndex = -1;
            nudQuantity.Value = 1;
            txtPrice.Clear();
            txtLineTotal.Clear();
            ClearItemImage();
            errorProvider1.Clear();
            cboItem.Focus();
        }

        private void SetMemberSelectionState()
        {
            if (UserSession.IsAdministrator)
            {
                cboMember.Enabled =
                    IsOrderingOpen(DateTime.Now, out _) &&
                    !hasUnsavedChanges;
            }
            else
            {
                cboMember.Enabled = false;
            }
        }

        //=====================================================
        // SAVE ORDER
        //=====================================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!EnforceOrderingPeriod())
            {
                return;
            }

            if (cboMember.SelectedIndex == -1 || cboMember.SelectedValue == null)
            {
                MessageBox.Show(
                    "Select an active member before saving.",
                    "Member Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cboMember.Focus();
                return;
            }

            if (dgvOrder.Rows.Count == 0 && deletedOrderIds.Count == 0)
            {
                MessageBox.Show(
                    "Add at least one available grocery item before saving.",
                    "Empty Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!ValidateCombinedQuantities(out string quantityError))
            {
                MessageBox.Show(
                    quantityError,
                    "Quantity Limit",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            bool deletingEntireOrder =
                dgvOrder.Rows.Count == 0 && deletedOrderIds.Count > 0;

            DialogResult confirmation = MessageBox.Show(
                deletingEntireOrder
                    ? "This will delete the member's entire order for this week. Continue?"
                    : "Save the additions and changes to this weekly order?",
                deletingEntireOrder ? "Delete Weekly Order" : "Confirm Order",
                MessageBoxButtons.YesNo,
                deletingEntireOrder
                    ? MessageBoxIcon.Warning
                    : MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            string memberId = cboMember.SelectedValue.ToString()!;

            try
            {
                using (SqlConnection con = db.GetConnection())
                {
                    con.Open();

                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            bool orderIdIsIdentity =
                                IsOrderIdIdentity(con, transaction);

                            foreach (int orderId in deletedOrderIds)
                            {
                                DeleteOrderLine(
                                    con,
                                    transaction,
                                    orderId,
                                    memberId);
                            }

                            foreach (DataGridViewRow row in dgvOrder.Rows)
                            {
                                string itemId =
                                    Convert.ToString(row.Cells["ItemID"].Value) ?? "";

                                int quantity =
                                    Convert.ToInt32(row.Cells["Quantity"].Value);

                                // Availability is checked again inside the same
                                // transaction immediately before INSERT/UPDATE.
                                if (!TryGetAvailableItem(
                                    con,
                                    transaction,
                                    itemId,
                                    out string description,
                                    out decimal currentPrice))
                                {
                                    throw new InvalidOperationException(
                                        $"Item {itemId} is no longer available. " +
                                        "The order was not saved.");
                                }

                                decimal totalPrice = currentPrice * quantity;

                                row.Cells["Description"].Value = description;
                                row.Cells["Price"].Value = currentPrice;
                                row.Cells["Total"].Value = totalPrice;

                                object? orderIdValue = row.Cells["OrderID"].Value;

                                if (orderIdValue != null &&
                                    orderIdValue != DBNull.Value &&
                                    int.TryParse(
                                        orderIdValue.ToString(),
                                        out int existingOrderId))
                                {
                                    UpdateOrderLine(
                                        con,
                                        transaction,
                                        existingOrderId,
                                        memberId,
                                        itemId,
                                        quantity,
                                        totalPrice,
                                        activeOrderDate);
                                }
                                else
                                {
                                    InsertOrderLine(
                                        con,
                                        transaction,
                                        memberId,
                                        itemId,
                                        quantity,
                                        totalPrice,
                                        activeOrderDate,
                                        orderIdIsIdentity);
                                }
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            try
                            {
                                transaction.Rollback();
                            }
                            catch
                            {
                                
                            }

                            throw;
                        }
                    }
                }

                hasUnsavedChanges = false;
                deletedOrderIds.Clear();

                MessageBox.Show(
                    deletingEntireOrder
                        ? "The weekly order was deleted successfully."
                        : "The weekly order was saved successfully.",
                    deletingEntireOrder ? "Order Deleted" : "Order Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadAvailableItems();
                LoadCurrentWeekOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The order was not saved.\n\n" + ex.Message,
                    "Save Order Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                LoadAvailableItems();
            }
        }

        private bool ValidateCombinedQuantities(out string message)
        {
            Dictionary<string, int> quantities =
                new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                string itemId =
                    Convert.ToString(row.Cells["ItemID"].Value) ?? "";

                int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                if (quantity < 1)
                {
                    message = $"Item {itemId} must have a quantity of at least 1.";
                    return false;
                }

                quantities.TryGetValue(itemId, out int existingQuantity);
                int combinedQuantity = existingQuantity + quantity;

                if (combinedQuantity > 12)
                {
                    message =
                        $"Item {itemId} has a combined quantity of " +
                        $"{combinedQuantity}. The maximum is 12.";

                    return false;
                }

                quantities[itemId] = combinedQuantity;
            }

            message = "";
            return true;
        }

        private static void DeleteOrderLine(
            SqlConnection con,
            SqlTransaction transaction,
            int orderId,
            string memberId)
        {
            const string sql = @"
                DELETE FROM tblMemberOrders
                WHERE OrderID = @OrderID
                  AND MemberID = @MemberID;";

            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderId;
                cmd.Parameters.Add("@MemberID", SqlDbType.NVarChar, 50).Value =
                    memberId;

                cmd.ExecuteNonQuery();
            }
        }

        private static void UpdateOrderLine(
            SqlConnection con,
            SqlTransaction transaction,
            int orderId,
            string memberId,
            string itemId,
            int quantity,
            decimal totalPrice,
            DateTime orderDate)
        {
            const string sql = @"
                UPDATE tblMemberOrders
                SET OrderDate = @OrderDate,
                    ItemID = @ItemID,
                    Quantity = @Quantity,
                    TotalPrice = @TotalPrice
                WHERE OrderID = @OrderID
                  AND MemberID = @MemberID;";

            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                AddOrderParameters(
                    cmd,
                    memberId,
                    itemId,
                    quantity,
                    totalPrice,
                    orderDate);

                cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderId;

                if (cmd.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException(
                        $"Order line {orderId} could not be updated.");
                }
            }
        }

        private static void InsertOrderLine(
            SqlConnection con,
            SqlTransaction transaction,
            string memberId,
            string itemId,
            int quantity,
            decimal totalPrice,
            DateTime orderDate,
            bool orderIdIsIdentity)
        {
            string sql;
            int nextOrderId = 0;

            if (orderIdIsIdentity)
            {
                sql = @"
                    INSERT INTO tblMemberOrders
                    (
                        OrderDate,
                        MemberID,
                        ItemID,
                        Quantity,
                        TotalPrice
                    )
                    VALUES
                    (
                        @OrderDate,
                        @MemberID,
                        @ItemID,
                        @Quantity,
                        @TotalPrice
                    );";
            }
            else
            {
                // Some supplied databases define OrderID as a required INT
                // rather than an IDENTITY column. Generate it safely while
                // the save transaction holds a serializable range lock.
                nextOrderId = GetNextOrderId(con, transaction);

                sql = @"
                    INSERT INTO tblMemberOrders
                    (
                        OrderID,
                        OrderDate,
                        MemberID,
                        ItemID,
                        Quantity,
                        TotalPrice
                    )
                    VALUES
                    (
                        @OrderID,
                        @OrderDate,
                        @MemberID,
                        @ItemID,
                        @Quantity,
                        @TotalPrice
                    );";
            }

            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                if (!orderIdIsIdentity)
                {
                    cmd.Parameters.Add("@OrderID", SqlDbType.Int).Value =
                        nextOrderId;
                }

                AddOrderParameters(
                    cmd,
                    memberId,
                    itemId,
                    quantity,
                    totalPrice,
                    orderDate);

                cmd.ExecuteNonQuery();
            }
        }

        private static bool IsOrderIdIdentity(
            SqlConnection con,
            SqlTransaction transaction)
        {
            const string sql = @"
                SELECT COLUMNPROPERTY(
                    OBJECT_ID(N'dbo.tblMemberOrders'),
                    N'OrderID',
                    'IsIdentity');";

            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                object? result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    throw new InvalidOperationException(
                        "The OrderID column could not be found in " +
                        "dbo.tblMemberOrders.");
                }

                return Convert.ToInt32(result) == 1;
            }
        }

        private static int GetNextOrderId(
            SqlConnection con,
            SqlTransaction transaction)
        {
            const string sql = @"
                SELECT ISNULL(MAX(TRY_CONVERT(int, OrderID)), 0) + 1
                FROM dbo.tblMemberOrders WITH (UPDLOCK, HOLDLOCK);";

            using (SqlCommand cmd = new SqlCommand(sql, con, transaction))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private static void AddOrderParameters(
            SqlCommand cmd,
            string memberId,
            string itemId,
            int quantity,
            decimal totalPrice,
            DateTime orderDate)
        {
            cmd.Parameters.Add("@OrderDate", SqlDbType.Date).Value =
                orderDate.Date;

            cmd.Parameters.Add("@MemberID", SqlDbType.NVarChar, 50).Value =
                memberId;

            cmd.Parameters.Add("@ItemID", SqlDbType.NVarChar, 50).Value =
                itemId;

            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;

            SqlParameter totalParameter =
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Decimal);

            totalParameter.Precision = 18;
            totalParameter.Scale = 2;
            totalParameter.Value = totalPrice;
        }

        //=====================================================
        // TOTALS AND GRID
        //=====================================================
        private void CalculateGrandTotal()
        {
            decimal grandTotal = 0m;

            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    grandTotal += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }

            lblGrandTotal.Text =
                "Grand total: " + grandTotal.ToString("C2", SouthAfricanCulture);
        }

        private void FormatGrid()
        {
            dgvOrder.AutoGenerateColumns = false;
            dgvOrder.AllowUserToAddRows = false;
            dgvOrder.AllowUserToDeleteRows = false;
            dgvOrder.AllowUserToResizeRows = false;
            dgvOrder.ReadOnly = true;
            dgvOrder.MultiSelect = false;
            dgvOrder.RowHeadersVisible = false;
            dgvOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Price.DefaultCellStyle.Format = "C2";
            Price.DefaultCellStyle.FormatProvider = SouthAfricanCulture;
            Total.DefaultCellStyle.Format = "C2";
            Total.DefaultCellStyle.FormatProvider = SouthAfricanCulture;
        }

        private void cboMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading)
            {
                return;
            }

            errorProvider1.SetError(cboMember, "");

            try
            {
                LoadCurrentWeekOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The member's current order could not be loaded.\n\n" +
                    ex.Message,
                    "Load Order Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLoadOrder_Click(object sender, EventArgs e)
        {
            if (hasUnsavedChanges)
            {
                DialogResult result = MessageBox.Show(
                    "Reloading will discard the unsaved changes. Continue?",
                    "Reload Weekly Order",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                LoadAvailableItems();
                LoadCurrentWeekOrder();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The weekly order could not be reloaded.\n\n" + ex.Message,
                    "Load Order Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
                btnRemove.PerformClick();
            }
        }

        //=====================================================
        // CLOSE FORM
        //=====================================================
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmOrders_FormClosing(
            object sender,
            FormClosingEventArgs e)
        {
            if (!hasUnsavedChanges)
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                "This order has not been saved. Close the form and discard it?",
                "Unsaved Order",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
