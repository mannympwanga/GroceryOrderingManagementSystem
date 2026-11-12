using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public partial class frmReports : Form
    {
        private static readonly CultureInfo SouthAfricanCulture =
            CultureInfo.GetCultureInfo("en-ZA");

        private readonly DatabaseHelper db = new DatabaseHelper();

        private readonly string reportsFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "GroceryOrderingManagementSystem",
            "Reports");

        private DataTable weeklyItems = new DataTable();
        private DataTable individualOrders = new DataTable();
        private DateTime loadedWeekStart;
        private DateTime loadedPurchaseFriday;

        public frmReports()
        {
            InitializeComponent();
        }

        //=====================================================
        // FORM LOAD AND ACCESS CONTROL
        //=====================================================
        private void frmReports_Load(object sender, EventArgs e)
        {
            // Only allow administrators to access management reports.
            if (!UserSession.IsAdministrator)
            {
                MessageBox.Show(
                    "Only an administrator may access management reports.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                BeginInvoke(new Action(Close));
                return;
            }

            // Ensure the reports folder exists and initialize controls.
            try
            {
                Directory.CreateDirectory(reportsFolder);

                DateTime latestOrderDate = GetLatestOrderDate() ?? DateTime.Today;

                dtpWeek.Value = latestOrderDate;
                dtpFrom.Value = latestOrderDate.AddMonths(-3);
                dtpTo.Value = latestOrderDate;
                nudTop.Value = 5;
                nudLow.Value = 5;

                ConfigureGrid(dgvWeekly);
                ConfigureGrid(dgvIndividual);
                ConfigureGrid(dgvItemTrends);
                ConfigureGrid(dgvMemberActivity);

                UpdateWeekDescription();
                ClearReportDisplays();
            }
            catch (Exception ex)
            {
                // If the form cannot be loaded, show an error message and close the form.
                MessageBox.Show(
                    "The reports form could not be loaded.\n\n" + ex.Message,
                    "Reports Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //=====================================================
        // DATABASE QUERY AND GRID CONFIGURATION
        //=====================================================
        private DateTime? GetLatestOrderDate()
        {
            // Retrieve the most recent order date from the database.
            const string sql = @"
                SELECT MAX(OrderDate)
                FROM dbo.tblMemberOrders;";

            // Use a using statement to ensure proper disposal of the connection and command objects.
            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return null;

                return Convert.ToDateTime(result).Date;
            }
        }

        //=====================================================
        // GRID CONFIGURATION AND DISPLAY HELPERS
        //=====================================================
        private static void ConfigureGrid(DataGridView grid)
        {
            // Configure the DataGridView to display data in a read-only, user-friendly manner.
            grid.AutoGenerateColumns = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = System.Drawing.SystemColors.Window;
            grid.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            grid.AlternatingRowsDefaultCellStyle.BackColor =
                System.Drawing.SystemColors.Window;
        }

        //=====================================================
        // REPORT DISPLAY MANAGEMENT
        //=====================================================
        private void ClearReportDisplays()
        {
            // Clear the data sources of all DataGridView controls and reset labels to their default state.
            dgvWeekly.DataSource = null;
            dgvIndividual.DataSource = null;
            dgvItemTrends.DataSource = null;
            dgvMemberActivity.DataSource = null;

            lblWeeklyTotal.Text = "Grand total: R0,00";
            lblIndividualTotal.Text = "Individual orders have not been loaded.";
            lblTrendSummary.Text = "Choose a valid date range and create the item report.";
            lblParticipationSummary.Text =
                "Choose a valid date range and create the participation report.";
        }

        //=====================================================
        // WEEKLY DATE CALCULATION AND VALIDATION
        //=====================================================
        private static DateTime GetMonday(DateTime date)
        {
            // Calculate the number of days to subtract to get to the previous Monday.
            int daysSinceMonday =
                ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

            return date.Date.AddDays(-daysSinceMonday);
        }

        // Calculate the Friday of the purchasing week based on the given date.
        private static DateTime GetPurchaseFriday(DateTime date)
        {
            return GetMonday(date).AddDays(4);
        }

        // Event handler for when the week selection changes in the DateTimePicker control.
        private void dtpWeek_ValueChanged(object sender, EventArgs e)
        {
            UpdateWeekDescription();
            weeklyItems = new DataTable();
            individualOrders = new DataTable();
            dgvWeekly.DataSource = null;
            dgvIndividual.DataSource = null;
            lblWeeklyTotal.Text = "Grand total: R0,00";
            lblIndividualTotal.Text = "Individual orders have not been loaded.";
        }

        // Update the labels that describe the ordering period and purchase Friday based on the selected week.
        private void UpdateWeekDescription()
        {
            DateTime monday = GetMonday(dtpWeek.Value);
            DateTime friday = monday.AddDays(4);

            lblWeekRange.Text = string.Format(
                SouthAfricanCulture,
                "Ordering period: {0:dddd, dd MMMM yyyy} to {1:dddd, dd MMMM yyyy}",
                monday,
                friday);

            lblPurchaseFriday.Text = string.Format(
                SouthAfricanCulture,
                "Purchase Friday: {0:dd MMMM yyyy}",
                friday);
        }

        // Validate that the selected week is not in the future.
        private bool ValidateWeeklyDate()
        {
            DateTime selectedWeekStart = GetMonday(dtpWeek.Value);
            DateTime currentWeekStart = GetMonday(DateTime.Today);

            // The current ordering week is valid even when its purchasing
            // Friday has not arrived, but a later purchasing cycle is not.
            if (selectedWeekStart > currentWeekStart)
            {
                MessageBox.Show(
                    "The selected purchasing week is in the future.",
                    "Invalid Week",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                dtpWeek.Focus();
                return false;
            }

            return true;
        }

        // Validate the management report date range,
        // ensuring that the From date is not later than the
        // To date, and that the To date is not in the future.
        private bool ValidateManagementDates(
            out DateTime fromDate,
            out DateTime toDate)
        {
            fromDate = dtpFrom.Value.Date;
            toDate = dtpTo.Value.Date;

            errorProvider1.Clear();

            if (fromDate > toDate)
            {
                errorProvider1.SetError(
                    dtpFrom,
                    "The From date cannot be later than the To date.");

                MessageBox.Show(
                    "The From date must be earlier than or equal to the To date.",
                    "Invalid Date Range",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                dtpFrom.Focus();
                return false;
            }
            // Ensure that the To date is not in the future.
            if (toDate > DateTime.Today)
            {

                // Set an error on the To date picker to indicate that future dates are not allowed.
                errorProvider1.SetError(
                    dtpTo,
                    "Reports cannot include future dates.");

                MessageBox.Show(
                    "The To date cannot be later than today.",
                    "Invalid Date Range",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                dtpTo.Focus();
                return false;
            }
            // Ensure that the date range does not exceed ten years (3660 days).
            if ((toDate - fromDate).TotalDays > 3660)
            {
                MessageBox.Show(
                    "Select a reporting period of ten years or less.",
                    "Date Range Too Large",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            errorProvider1.Clear();
            return true;
        }

        //=====================================================
        // CONSOLIDATED WEEKLY GROCERY ORDER
        //=====================================================
        private void btnLoadWeekly_Click(object sender, EventArgs e)
        {
            // Validate the selected week before attempting to load weekly items.
            if (!ValidateWeeklyDate())
                return;

            try
            {
                LoadWeeklyItems();
            }
            catch (Exception ex)
            {
                ShowReportError("The consolidated weekly report could not be created.", ex);
            }
        }

        // Load the weekly grocery order items for the selected week and display them in the DataGridView.
        private void LoadWeeklyItems()
        {
            loadedWeekStart = GetMonday(dtpWeek.Value);
            loadedPurchaseFriday = loadedWeekStart.AddDays(4);

            // Monday to the end of Friday is included. This supports current
            // Monday-Thursday orders and historical rows stored on a Friday.
            DateTime endExclusive = loadedPurchaseFriday.AddDays(1);

            const string sql = @"
                SELECT
                    gi.ItemID AS [Item ID],
                    gi.Description,
                    SUM(mo.Quantity) AS [Total Quantity],
                    CAST(SUM(mo.TotalPrice) AS decimal(18, 2)) AS [Total Amount]
                FROM dbo.tblMemberOrders AS mo
                INNER JOIN dbo.tblGroceryItems AS gi
                    ON gi.ItemID = mo.ItemID
                WHERE mo.OrderDate >= @WeekStart
                  AND mo.OrderDate < @EndExclusive
                GROUP BY gi.ItemID, gi.Description
                ORDER BY gi.Description, gi.ItemID;";

            // Fill the weeklyItems DataTable with the results of the SQL query.
            weeklyItems = FillTable(
                sql,
                new SqlParameter("@WeekStart", SqlDbType.Date)
                {
                    Value = loadedWeekStart
                },
                new SqlParameter("@EndExclusive", SqlDbType.Date)
                {
                    Value = endExclusive
                });

            dgvWeekly.DataSource = weeklyItems;
            FormatCurrencyColumn(dgvWeekly, "Total Amount");

            decimal grandTotal = weeklyItems.AsEnumerable()
                .Sum(row => row.Field<decimal>("Total Amount"));

            int totalUnits = weeklyItems.AsEnumerable()
                .Sum(row => row.Field<int>("Total Quantity"));

            lblWeeklyTotal.Text = string.Format(
                SouthAfricanCulture,
                "{0} different items • {1} units • Grand total: {2:C2}",
                weeklyItems.Rows.Count,
                totalUnits,
                grandTotal);

            lblPurchaseFriday.Text = string.Format(
                SouthAfricanCulture,
                "Purchase Friday: {0:dd MMMM yyyy}",
                loadedPurchaseFriday);

            if (weeklyItems.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No orders were found for the selected weekly period.",
                    "No Weekly Orders",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        //=====================================================
        // INDIVIDUAL MEMBER ORDER SUMMARIES
        //=====================================================
        private void btnLoadIndividual_Click(object sender, EventArgs e)
        {
            // Validate the selected week before attempting to load individual member orders.
            if (!ValidateWeeklyDate())
                return;

            try
            {
                LoadIndividualOrders();
            }
            catch (Exception ex)
            {
                ShowReportError("The member summaries could not be created.", ex);
            }
        }

        //=====================================================
        // Load the individual member orders for the selected week and display them in the DataGridView.
        //=====================================================
        private void LoadIndividualOrders()
        {
            // Calculate the start of the week (Monday) and the purchase Friday based on the selected date.
            loadedWeekStart = GetMonday(dtpWeek.Value);
            loadedPurchaseFriday = loadedWeekStart.AddDays(4);
            DateTime endExclusive = loadedPurchaseFriday.AddDays(1);

            // SQL query to retrieve individual member orders,
            // including member details, item details, quantity, and total amount.
            const string sql = @"
                SELECT
                    m.MemberID AS [Member ID],
                    m.Name + ' ' + m.Surname AS [Member],
                    gi.ItemID AS [Item ID],
                    gi.Description AS [Item],
                    SUM(mo.Quantity) AS [Quantity],
                    CAST(SUM(mo.TotalPrice) AS decimal(18, 2)) AS [Total Amount]
                FROM dbo.tblMemberOrders AS mo
                INNER JOIN dbo.tblMembers AS m
                    ON m.MemberID = mo.MemberID
                INNER JOIN dbo.tblGroceryItems AS gi
                    ON gi.ItemID = mo.ItemID
                WHERE mo.OrderDate >= @WeekStart
                  AND mo.OrderDate < @EndExclusive
                GROUP BY
                    m.MemberID,
                    m.Name,
                    m.Surname,
                    gi.ItemID,
                    gi.Description
                ORDER BY
                    m.MemberID,
                    gi.Description,
                    gi.ItemID;";

            individualOrders = FillTable(
                sql,
                new SqlParameter("@WeekStart", SqlDbType.Date)
                {
                    Value = loadedWeekStart
                },
                new SqlParameter("@EndExclusive", SqlDbType.Date)
                {
                    Value = endExclusive
                });

            dgvIndividual.DataSource = individualOrders;
            FormatCurrencyColumn(dgvIndividual, "Total Amount");
            // Count the number of distinct members who placed orders during the week.
            int memberCount = individualOrders.AsEnumerable()
                .Select(row => row.Field<string>("Member ID"))
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Count();

            decimal combinedTotal = individualOrders.AsEnumerable()
                .Sum(row => row.Field<decimal>("Total Amount"));

            lblIndividualTotal.Text = string.Format(
                SouthAfricanCulture,
                "{0} member orders • Combined value: {1:C2}",
                memberCount,
                combinedTotal);

            if (individualOrders.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No individual member orders were found for this week.",
                    "No Member Orders",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        //=====================================================
        // WEEKLY AND INDIVIDUAL PDF GENERATION
        //=====================================================
        private void btnWeeklyPdf_Click(object sender, EventArgs e)
        {
            // Validate the selected week before attempting to generate the weekly PDF report.
            if (!ValidateWeeklyDate())
                return;
            // Attempt to load the weekly items and generate the PDF report.
            try
            {
                LoadWeeklyItems();

                if (weeklyItems.Rows.Count == 0)
                    return;

                string filePath = Path.Combine(
                    reportsFolder,
                    $"WeeklyOrder_{loadedPurchaseFriday:yyyyMMdd}.pdf");

                CreateWeeklyPdf(filePath);
                ShowPdfSuccess(filePath);
            }
            catch (Exception ex)
            {
                ShowReportError("The weekly PDF could not be generated.", ex);
            }
        }

        //=====================================================
        // Create a PDF report for the weekly grocery order summary.
        //=====================================================
        private void CreateWeeklyPdf(string filePath)
        {
            // Convert the weeklyItems DataTable into a list of WeeklyItem objects for easier processing.
            List<WeeklyItem> items = weeklyItems.AsEnumerable()
                .Select(row => new WeeklyItem
                {
                    ItemId = row.Field<string>("Item ID") ?? "",
                    Description = row.Field<string>("Description") ?? "",
                    Quantity = row.Field<int>("Total Quantity"),
                    Total = row.Field<decimal>("Total Amount")
                })
                .ToList();
            // Calculate the grand total of all items in the weekly order.
            decimal grandTotal = items.Sum(item => item.Total);

            // Prepare the rows for the PDF table by converting each WeeklyItem into a string array.
            List<string[]> rows = items
                .Select(item => new[]
                {
                    item.ItemId,
                    item.Description,
                    item.Quantity.ToString(SouthAfricanCulture),
                    item.Total.ToString("N2", SouthAfricanCulture)
                })
                .ToList();
            // Generate the PDF report using the SimplePdfWriter class,
            // providing the necessary details and formatting.
            SimplePdfWriter.WriteTableReport(
                filePath,
                "WEEKLY GROCERY ORDER SUMMARY",
                new[]
                {
                    $"Purchase Friday: {loadedPurchaseFriday:dddd, dd MMMM yyyy}",
                    $"Ordering period: {loadedWeekStart:dd MMMM yyyy} " +
                    $"to {loadedPurchaseFriday:dd MMMM yyyy}"
                },
                new[] { "Item ID", "Description", "Total Qty", "Total (R)" },
                rows,
                new[] { 75f, 270f, 80f, 100f },
                "GRAND TOTAL: " +
                grandTotal.ToString("C2", SouthAfricanCulture));
        }

        //=====================================================
        // Generate individual member PDF reports for the selected week.
        //=====================================================
        private void btnIndividualPdfs_Click(object sender, EventArgs e)
        {
            // Validate the selected week before attempting to generate individual member PDF reports.
            if (!ValidateWeeklyDate())
                return;

            try
            {
                // Load the individual member orders for the selected week.
                LoadIndividualOrders();

                if (individualOrders.Rows.Count == 0)
                    return;

                List<MemberOrderSummary> summaries = BuildMemberSummaries();
                // Create a folder for the individual member PDFs based on the purchase Friday date.
                string weekFolder = Path.Combine(
                    reportsFolder,
                    $"MemberOrders_{loadedPurchaseFriday:yyyyMMdd}");

                Directory.CreateDirectory(weekFolder);

                foreach (MemberOrderSummary summary in summaries)
                {
                    string safeMemberId = MakeSafeFileName(summary.MemberId);
                    string filePath = Path.Combine(
                        weekFolder,
                        $"MemberOrder_{safeMemberId}_{loadedPurchaseFriday:yyyyMMdd}.pdf");

                    CreateMemberPdf(filePath, summary);
                }
                // Display a message box indicating the number of member
                // PDF files generated successfully
                // and provide the path to the folder containing the PDFs.
                MessageBox.Show(
                    $"{summaries.Count} member PDF file(s) were generated successfully.\n\n" +
                    weekFolder,
                    "Member PDFs Generated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                OpenPath(weekFolder);
            }
            catch (Exception ex)
            {
                ShowReportError("The individual member PDFs could not be generated.", ex);
            }
        }

        //=====================================================
        // Build a list of MemberOrderSummary objects from the individualOrders DataTable.
        //=====================================================
        private List<MemberOrderSummary> BuildMemberSummaries()
        {
            // Group the individualOrders DataTable by Member ID and Member Name,
            return individualOrders.AsEnumerable()
                .GroupBy(row => new
                {
                    MemberId = row.Field<string>("Member ID") ?? "",
                    MemberName = row.Field<string>("Member") ?? ""
                })
                .Select(group => new MemberOrderSummary
                {
                    MemberId = group.Key.MemberId,
                    MemberName = group.Key.MemberName,
                    Items = group.Select(row => new MemberOrderItem
                    {
                        ItemId = row.Field<string>("Item ID") ?? "",
                        Description = row.Field<string>("Item") ?? "",
                        Quantity = row.Field<int>("Quantity"),
                        Total = row.Field<decimal>("Total Amount")
                    }).ToList()
                })
                .OrderBy(summary => summary.MemberId)
                .ToList();
        }
        //=====================================================
        // Create a PDF report for an individual member's order summary.
        //=====================================================
        private void CreateMemberPdf(
            string filePath,
            MemberOrderSummary summary)
        {
            // Calculate the total amount for the member's order summary.
            decimal memberTotal = summary.Items.Sum(item => item.Total);

            // Prepare the rows for the PDF table by converting each MemberOrderItem into a string array.
            List<string[]> rows = summary.Items
                .Select(item => new[]
                {
                    item.ItemId,
                    item.Description,
                    item.Quantity.ToString(SouthAfricanCulture),
                    item.Total.ToString("N2", SouthAfricanCulture)
                })
                .ToList();

            SimplePdfWriter.WriteTableReport(
                filePath,
                "GROCERY ORDER STATEMENT",
                new[]
                {
                    $"Member: {summary.MemberId} - {summary.MemberName}",
                    $"Purchase Friday: {loadedPurchaseFriday:dddd, dd MMMM yyyy}"
                },
                new[] { "Item ID", "Item", "Qty", "Total (R)" },
                rows,
                new[] { 75f, 285f, 65f, 100f },
                "TOTAL AMOUNT DUE: " +
                memberTotal.ToString("C2", SouthAfricanCulture));
        }

        //=====================================================
        // MIS: ITEM ORDERING TRENDS
        //=====================================================
        private void btnItemTrends_Click(object sender, EventArgs e)
        {
            if (!ValidateManagementDates(out DateTime fromDate, out DateTime toDate))
                return;

            try
            {
                // SQL query to retrieve item ordering trends within the specified date range,
                const string sql = @"
                    SELECT
                        gi.ItemID AS [Item ID],
                        gi.Description,
                        COUNT(*) AS [Order Lines],
                        SUM(mo.Quantity) AS [Total Quantity],
                        CAST(SUM(mo.TotalPrice) AS decimal(18, 2)) AS [Total Amount]
                    FROM dbo.tblMemberOrders AS mo
                    INNER JOIN dbo.tblGroceryItems AS gi
                        ON gi.ItemID = mo.ItemID
                    WHERE mo.OrderDate >= @FromDate
                      AND mo.OrderDate < @ToExclusive
                    GROUP BY gi.ItemID, gi.Description
                    ORDER BY
                        SUM(mo.Quantity) DESC,
                        SUM(mo.TotalPrice) DESC,
                        gi.Description;";

                // Fill a DataTable with all items ordered within the specified date range.
                DataTable allItems = FillTable(
                    sql,
                    new SqlParameter("@FromDate", SqlDbType.Date)
                    {
                        Value = fromDate
                    },
                    new SqlParameter("@ToExclusive", SqlDbType.Date)
                    {
                        Value = toDate.AddDays(1)
                    });

                // Build a trend table that includes the top and low ordered items based on the specified counts.
                DataTable trends = BuildTrendTable(
                    allItems,
                    Decimal.ToInt32(nudTop.Value),
                    Decimal.ToInt32(nudLow.Value));

                dgvItemTrends.DataSource = trends;
                FormatCurrencyColumn(dgvItemTrends, "Total Amount");

                int totalUnits = allItems.AsEnumerable()
                    .Sum(row => row.Field<int>("Total Quantity"));

                decimal totalValue = allItems.AsEnumerable()
                    .Sum(row => row.Field<decimal>("Total Amount"));

                lblTrendSummary.Text = string.Format(
                    SouthAfricanCulture,
                    "{0:dd MMM yyyy} – {1:dd MMM yyyy}: " +
                    "{2} ordered items, {3} units, {4:C2}",
                    fromDate,
                    toDate,
                    allItems.Rows.Count,
                    totalUnits,
                    totalValue);

                // If no items were found in the specified date range,
                // display an informational message to the user.

                if (allItems.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No grocery orders were found in the selected date range.",
                        "No Trend Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowReportError("The item ordering trend report could not be created.", ex);
            }
        }

        // Build a DataTable that includes the top and low ordered items based on the specified counts.

        private static DataTable BuildTrendTable(
            DataTable allItems,
            int topCount,
            int lowCount)
        {
            // Create a new DataTable to hold the trend data, cloning the structure of the allItems DataTable.
            DataTable result = allItems.Clone();
            result.Columns.Add("Trend", typeof(string));

            List<DataRow> orderedRows = allItems.AsEnumerable().ToList();
            HashSet<string> addedItemIds =
                new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (DataRow row in orderedRows.Take(topCount))
            {
                AddTrendRow(result, row, "TOP");
                addedItemIds.Add(row.Field<string>("Item ID") ?? "");
            }

            foreach (DataRow row in orderedRows
                         .OrderBy(item => item.Field<int>("Total Quantity"))
                         .ThenBy(item => item.Field<decimal>("Total Amount"))
                         .ThenBy(item => item.Field<string>("Description"))
                         .Where(item => !addedItemIds.Contains(
                             item.Field<string>("Item ID") ?? ""))
                         .Take(lowCount))
            {
                AddTrendRow(result, row, "LOW");
            }

            return result;
        }
        // Add a row to the trend DataTable with the specified trend label (TOP or LOW).
        private static void AddTrendRow(
            DataTable destination,
            DataRow source,
            string trend)
        {
            DataRow newRow = destination.NewRow();

            foreach (DataColumn column in source.Table.Columns)
            {
                newRow[column.ColumnName] = source[column];
            }

            newRow["Trend"] = trend;
            destination.Rows.Add(newRow);
        }

        //=====================================================
        // MIS: MEMBER PARTICIPATION
        //=====================================================
        private void btnMemberActivity_Click(object sender, EventArgs e)
        {
            // Validate the selected date range for the member participation report.
            if (!ValidateManagementDates(out DateTime fromDate, out DateTime toDate))
                return;

            try
            {
                // Count the number of Fridays (opportunities) in the specified date range.
                int opportunities = CountFridays(fromDate, toDate);

                // If there are no Fridays in the date range, display a warning message and exit the method.
                if (opportunities == 0)
                {
                    MessageBox.Show(
                        "The selected range does not contain a Friday purchasing cycle.",
                        "No Weekly Opportunities",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Participation is measured by purchasing cycle. Include the
                // Monday-Thursday ordering period belonging to each Friday in
                // the selected date range, even when From is itself a Friday.
                DateTime firstPurchaseFriday =
                    GetFirstFridayOnOrAfter(fromDate);
                DateTime lastPurchaseFriday =
                    GetLastFridayOnOrBefore(toDate);
                DateTime firstOrderingMonday =
                    firstPurchaseFriday.AddDays(-4);
                DateTime endExclusive =
                    lastPurchaseFriday.AddDays(1);

                // SQL query to retrieve member participation data,
                // including the number of orders made and total ordered amount.
                const string sql = @"
                    SELECT
                        m.MemberID AS [Member ID],
                        m.Name + ' ' + m.Surname AS [Member],
                        COUNT(DISTINCT CASE
                            WHEN mo.OrderID IS NOT NULL
                            THEN DATEDIFF(DAY, 0, mo.OrderDate) / 7
                        END) AS [Orders Made],
                        CAST(COALESCE(SUM(mo.TotalPrice), 0)
                            AS decimal(18, 2)) AS [Total Ordered]
                    FROM dbo.tblMembers AS m
                    LEFT JOIN dbo.tblMemberOrders AS mo
                        ON mo.MemberID = m.MemberID
                       AND mo.OrderDate >= @FromDate
                       AND mo.OrderDate < @ToExclusive
                    GROUP BY
                        m.MemberID,
                        m.Name,
                        m.Surname
                    ORDER BY
                        [Orders Made] DESC,
                        m.MemberID;";

                DataTable activity = FillTable(
                    sql,
                    new SqlParameter("@FromDate", SqlDbType.Date)
                    {
                        Value = firstOrderingMonday
                    },
                    new SqlParameter("@ToExclusive", SqlDbType.Date)
                    {
                        Value = endExclusive
                    });

                activity.Columns.Add("Opportunities", typeof(int));
                activity.Columns.Add("Participation %", typeof(decimal));

                // Calculate the participation percentage for each
                // member based on the number of orders made and the total opportunities.
                foreach (DataRow row in activity.Rows)
                {
                    int ordersMade = row.Field<int>("Orders Made");
                    decimal participation =
                        Math.Round(ordersMade * 100m / opportunities, 1);

                    row["Opportunities"] = opportunities;
                    row["Participation %"] = participation;
                }

                // Display opportunities before the percentage.
                activity.Columns["Opportunities"].SetOrdinal(3);
                activity.Columns["Participation %"].SetOrdinal(4);

                dgvMemberActivity.DataSource = activity;
                FormatCurrencyColumn(dgvMemberActivity, "Total Ordered");

                // Format the "Participation %" column to display as a percentage with one decimal place.
                if (dgvMemberActivity.Columns.Contains("Participation %"))
                {
                    dgvMemberActivity.Columns["Participation %"]
                        .DefaultCellStyle.Format = "0.0'%'";
                }

                // Count the number of members who participated (made at least one order) in
                // the specified date range.
                int participatingMembers = activity.AsEnumerable()
                    .Count(row => row.Field<int>("Orders Made") > 0);

                decimal averageParticipation = activity.Rows.Count == 0
                    ? 0m
                    : activity.AsEnumerable()
                        .Average(row => row.Field<decimal>("Participation %"));

                lblParticipationSummary.Text = string.Format(
                    SouthAfricanCulture,
                    "{0} weekly opportunities • {1} of {2} members participated • " +
                    "Average participation: {3:0.0}%",
                    opportunities,
                    participatingMembers,
                    activity.Rows.Count,
                    averageParticipation);
            }
            catch (Exception ex)
            {
                ShowReportError("The member participation report could not be created.", ex);
            }
        }

        //=====================================================
        // Count the number of Fridays (opportunities) in the specified date range.
        //=====================================================
        private static int CountFridays(DateTime fromDate, DateTime toDate)
        {
            DateTime firstFriday = GetFirstFridayOnOrAfter(fromDate);

            // If the first Friday is after the end date, return 0 as there are no Fridays in the range.
            if (firstFriday > toDate.Date)
                return 0;

            return 1 + (int)((toDate.Date - firstFriday).TotalDays / 7);
        }

        private static DateTime GetFirstFridayOnOrAfter(DateTime date)
        {
            int daysUntilFriday =
                ((int)DayOfWeek.Friday - (int)date.DayOfWeek + 7) % 7;

            return date.Date.AddDays(daysUntilFriday);
        }

        private static DateTime GetLastFridayOnOrBefore(DateTime date)
        {
            int daysSinceFriday =
                ((int)date.DayOfWeek - (int)DayOfWeek.Friday + 7) % 7;

            return date.Date.AddDays(-daysSinceFriday);
        }

        //=====================================================
        // DATABASE AND DISPLAY HELPERS
        //=====================================================
        private DataTable FillTable(
            string sql,
            params SqlParameter[] parameters)
        {
            // Use the database connection to execute the SQL query and fill a DataTable with the results.
            using (SqlConnection con = db.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddRange(parameters);

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        //=====================================================
        // Format the specified column in the DataGridView to display currency values with two decimal places.
        //=====================================================
        private static void FormatCurrencyColumn(
            DataGridView grid,
            string columnName)
        {
            // Check if the specified column exists in the DataGridView before attempting to format it.
            if (!grid.Columns.Contains(columnName))
                return;

            grid.Columns[columnName].DefaultCellStyle.Format = "C2";
            grid.Columns[columnName].DefaultCellStyle.FormatProvider =
                SouthAfricanCulture;
        }

        //=====================================================
        // Make a string safe for use as a file name by replacing invalid characters with underscores.
        //=====================================================
        private static string MakeSafeFileName(string value)
        {
            string safeValue = value;

            foreach (char invalidCharacter in Path.GetInvalidFileNameChars())
            {
                safeValue = safeValue.Replace(invalidCharacter, '_');
            }

            return safeValue;
        }

        //=====================================================
        // Show a message box indicating that the PDF was generated successfully and provide the file path.
        //=====================================================
        private void ShowPdfSuccess(string filePath)
        {
            MessageBox.Show(
                "PDF generated successfully.\n\n" + filePath,
                "PDF Generated",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            OpenPath(filePath);
        }

        //=====================================================
        // Open the specified path (file or folder) using the default application.
        //=====================================================
        private static void OpenPath(string path)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
        }

        //=====================================================
        // Open the reports folder in the default file explorer.
        //=====================================================
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(reportsFolder);
                OpenPath(reportsFolder);
            }
            catch (Exception ex)
            {
                ShowReportError("The reports folder could not be opened.", ex);
            }
        }

        //=====================================================
        // Show a message box indicating that an error occurred while generating a report,
        // including the exception message.
        //=====================================================
        private static void ShowReportError(string message, Exception ex)
        {
            MessageBox.Show(
                message + "\n\n" + ex.Message,
                "Report Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //=====================================================
        // PDF REPORT WRITER
        //=====================================================
        private static class SimplePdfWriter
        {
            // Constants for PDF page dimensions and margins.
            private const float PageWidth = 595f;
            private const float PageHeight = 842f;
            private const float LeftMargin = 35f;
            private const float BottomMargin = 55f;
            private const float RowHeight = 22f;

            //=====================================================
            // Write a simple PDF report with a table of data, including headers and totals.
            //====================================================
            public static void WriteTableReport(
                string filePath,
                string title,
                string[] details,
                string[] headers,
                IEnumerable<string[]> reportRows,
                float[] columnWidths,
                string totalText)
            {
                // Validate that the number of headers matches the number of column widths.
                if (headers.Length != columnWidths.Length)
                {
                    throw new ArgumentException(
                        "The PDF header count must match the column-width count.");
                }

                float tableWidth = columnWidths.Sum();

                // Validate that the total width of the table does not exceed the printable page area.
                if (tableWidth > PageWidth - (LeftMargin * 2))
                {
                    throw new ArgumentException(
                        "The PDF table is wider than the printable page area.");
                }

                // Convert the report rows to a list for easier
                // processing and initialize a list to hold the pages of the PDF.
                List<string[]> rows = reportRows.ToList();
                List<StringBuilder> pages = new List<StringBuilder>();

                StringBuilder currentPage = CreateReportPage(
                    title,
                    details,
                    headers,
                    columnWidths,
                    false,
                    out float currentY);

                pages.Add(currentPage);

                // Iterate through each row of data and add it to the PDF report, creating new pages as needed.
                foreach (string[] row in rows)
                {
                    if (row.Length != headers.Length)
                    {
                        throw new ArgumentException(
                            "Every PDF data row must contain the same number of columns as the header.");
                    }

                    if (currentY - RowHeight < BottomMargin)
                    {
                        currentPage = CreateReportPage(
                            title,
                            details,
                            headers,
                            columnWidths,
                            true,
                            out currentY);

                        pages.Add(currentPage);
                    }

                    DrawTableRow(
                        currentPage,
                        ref currentY,
                        row,
                        columnWidths,
                        false);
                }

                // Check if there is enough space for the total text, otherwise create a new page.
                if (currentY - 35f < BottomMargin)
                {
                    currentPage = CreateReportPage(
                        title,
                        details,
                        headers,
                        columnWidths,
                        true,
                        out currentY);

                    pages.Add(currentPage);
                }

                DrawRightAlignedText(
                    currentPage,
                    totalText,
                    PageWidth - LeftMargin,
                    currentY - 27f,
                    12f,
                    true);
                // Add page numbers to each page of the PDF report.
                for (int index = 0; index < pages.Count; index++)
                {
                    DrawCentredText(
                        pages[index],
                        $"Grocery Ordering Management System - Page {index + 1} of {pages.Count}",
                        28f,
                        9f,
                        false);
                }

                WritePdfFile(filePath, pages);
            }

            //=====================================================
            // Create a new page for the PDF report with the specified
            // title, details, headers, and column widths.
            //=====================================================
            private static StringBuilder CreateReportPage(
                string title,
                string[] details,
                string[] headers,
                float[] columnWidths,
                bool continued,
                out float currentY)
            {
                // Initialize a new StringBuilder to hold the content of the PDF page.
                StringBuilder page = new StringBuilder();
                page.AppendLine("0 G");
                page.AppendLine("0 g");

                string heading = continued ? title + " (continued)" : title;

                DrawCentredText(page, heading, PageHeight - 52f, 18f, true);

                float detailY = PageHeight - 79f;

                foreach (string detail in details)
                {
                    DrawText(page, detail, LeftMargin, detailY, 10f, false);
                    detailY -= 15f;
                }

                currentY = detailY - 10f;

                DrawTableRow(
                    page,
                    ref currentY,
                    headers,
                    columnWidths,
                    true);

                return page;
            }

            //=====================================================
            // Draw a single row of the table in the PDF report, including the cell borders and text.
            //=====================================================
            private static void DrawTableRow(
                StringBuilder page,
                ref float currentY,
                string[] values,
                float[] columnWidths,
                bool isHeader)
            {
                // Calculate the top and bottom Y coordinates for the row,
                // as well as the right X coordinate based on the column widths.
                float top = currentY;
                float bottom = currentY - RowHeight;
                float right = LeftMargin + columnWidths.Sum();

                page.AppendLine("0.6 w");
                DrawLine(page, LeftMargin, top, right, top);
                DrawLine(page, LeftMargin, bottom, right, bottom);

                float x = LeftMargin;
                DrawLine(page, x, top, x, bottom);

                // Draw each cell in the row, fitting the text within the specified column width
                // and drawing the right border for each cell.
                for (int index = 0; index < columnWidths.Length; index++)
                {
                    float width = columnWidths[index];
                    string value = FitText(values[index], width, 9f);

                    DrawText(
                        page,
                        value,
                        x + 4f,
                        bottom + 7f,
                        9f,
                        isHeader);

                    x += width;
                    DrawLine(page, x, top, x, bottom);
                }

                currentY = bottom;
            }

            //=====================================================
            // Draw a line in the PDF report from (x1, y1) to (x2, y2).
            //=====================================================
            private static void DrawLine(
                StringBuilder page,
                float x1,
                float y1,
                float x2,
                float y2)
            {
                page.AppendLine(
                    $"{Number(x1)} {Number(y1)} m " +
                    $"{Number(x2)} {Number(y2)} l S");
            }

            //=====================================================
            // Draw text in the PDF report at the specified position, size, and font style (bold or regular).
            //=====================================================
            private static void DrawText(
                StringBuilder page,
                string value,
                float x,
                float y,
                float size,
                bool bold)
            {
                string font = bold ? "F2" : "F1";

                page.AppendLine(
                    $"BT /{font} {Number(size)} Tf " +
                    $"{Number(x)} {Number(y)} Td " +
                    $"({EscapePdfText(value)}) Tj ET");
            }

            //=====================================================
            // Draw centered text in the PDF report at the specified Y position, size,
            // and font style (bold or regular).
            //=====================================================
            private static void DrawCentredText(
                StringBuilder page,
                string value,
                float y,
                float size,
                bool bold)
            {
                string safeText = NormaliseText(value);
                float estimatedWidth = safeText.Length * size * 0.52f;
                float x = Math.Max(LeftMargin, (PageWidth - estimatedWidth) / 2f);

                DrawText(page, safeText, x, y, size, bold);
            }

            //=====================================================
            // Draw right-aligned text in the PDF report at the specified Y position, size,
            // and font style (bold or regular).
            //=====================================================
            private static void DrawRightAlignedText(
                StringBuilder page,
                string value,
                float right,
                float y,
                float size,
                bool bold)
            {
                string safeText = NormaliseText(value);
                float estimatedWidth = safeText.Length * size * 0.52f;
                float x = Math.Max(LeftMargin, right - estimatedWidth);

                DrawText(page, safeText, x, y, size, bold);
            }

            //=====================================================
            // Fit the text within the specified available width by truncating and adding ellipsis if necessary.
            //=====================================================
            private static string FitText(
                string value,
                float availableWidth,
                float fontSize)
            {
                string safeText = NormaliseText(value);
                int maximumCharacters = Math.Max(
                    3,
                    (int)((availableWidth - 8f) / (fontSize * 0.52f)));

                if (safeText.Length <= maximumCharacters)
                    return safeText;

                return safeText.Substring(0, maximumCharacters - 3) + "...";
            }

            //=====================================================
            // Escape special characters in the text for PDF compatibility.
            //=====================================================
            private static string EscapePdfText(string value)
            {
                return NormaliseText(value)
                    .Replace("\\", "\\\\")
                    .Replace("(", "\\(")
                    .Replace(")", "\\)");
            }

            //=====================================================
            // Normalise the text by replacing special characters with their standard equivalents.
            //=====================================================
            private static string NormaliseText(string value)
            {
                StringBuilder result = new StringBuilder();

                // Iterate through each character in the input string and replace
                // special characters with their standard equivalents.
                foreach (char character in value ?? string.Empty)
                {
                    switch (character)
                    {
                        case '\u2013':
                        case '\u2014':
                            result.Append('-');
                            break;
                        case '\u2018':
                        case '\u2019':
                            result.Append('\'');
                            break;
                        case '\u201C':
                        case '\u201D':
                            result.Append('"');
                            break;
                        case '\u2022':
                            result.Append('-');
                            break;
                        default:
                            result.Append(character >= 32 && character <= 255
                                ? character
                                : '?');
                            break;
                    }
                }

                return result.ToString();
            }

            //=====================================================
            // Convert a float value to a string with two decimal places using invariant culture.
            //=====================================================
            private static string Number(float value)
            {
                return value.ToString("0.##", CultureInfo.InvariantCulture);
            }

            //=====================================================
            // Write the PDF file to the specified file path using the provided pages of content.
            //=====================================================
            private static void WritePdfFile(
                string filePath,
                List<StringBuilder> pages)
            {
                // Calculate the total number of pages in the PDF report.
                int pageCount = pages.Count;
                List<string> objects = new List<string>();

                objects.Add("<< /Type /Catalog /Pages 2 0 R >>");

                // Create a string that references each page object in the PDF report.
                string pageReferences = string.Join(
                    " ",
                    Enumerable.Range(0, pageCount)
                        .Select(index => $"{5 + (index * 2)} 0 R"));

                // Add the page tree object to the PDF report, including the page references and total page count.
                objects.Add(
                    $"<< /Type /Pages /Kids [{pageReferences}] " +
                    $"/Count {pageCount} >>");

                objects.Add(
                    "<< /Type /Font /Subtype /Type1 " +
                    "/BaseFont /Helvetica /Encoding /WinAnsiEncoding >>");

                objects.Add(
                    "<< /Type /Font /Subtype /Type1 " +
                    "/BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>");

                // Add each page object to the PDF report, including the page content and resources.
                for (int index = 0; index < pageCount; index++)
                {
                    int contentObjectNumber = 6 + (index * 2);

                    objects.Add(
                        "<< /Type /Page /Parent 2 0 R " +
                        "/MediaBox [0 0 595 842] " +
                        "/Resources << /Font << /F1 3 0 R /F2 4 0 R >> >> " +
                        $"/Contents {contentObjectNumber} 0 R >>");

                    string content = pages[index].ToString();
                    int length = Encoding.Latin1.GetByteCount(content);

                    objects.Add(
                        $"<< /Length {length} >>\nstream\n" +
                        content +
                        "endstream");
                }

                // Write the PDF file to disk, including the header, objects, cross-reference table, and trailer.
                using (MemoryStream output = new MemoryStream())
                {
                    WriteLatin1(output, "%PDF-1.4\n");

                    List<long> offsets = new List<long> { 0L };


                    // Write each object to the PDF file and record its offset for the cross-reference table.
                    for (int index = 0; index < objects.Count; index++)
                    {
                        offsets.Add(output.Position);
                        WriteLatin1(
                            output,
                            $"{index + 1} 0 obj\n{objects[index]}\nendobj\n");
                    }

                    long crossReferenceOffset = output.Position;

                    WriteLatin1(
                        output,
                        $"xref\n0 {objects.Count + 1}\n" +
                        "0000000000 65535 f \n");

                    // Write the offsets of each object in the cross-reference table.
                    for (int index = 1; index < offsets.Count; index++)
                    {
                        WriteLatin1(
                            output,
                            $"{offsets[index]:0000000000} 00000 n \n");
                    }

                    WriteLatin1(
                        output,
                        "trailer\n" +
                        $"<< /Size {objects.Count + 1} /Root 1 0 R >>\n" +
                        "startxref\n" +
                        $"{crossReferenceOffset}\n" +
                        "%%EOF\n");

                    File.WriteAllBytes(filePath, output.ToArray());
                }
            }

            private static void WriteLatin1(Stream stream, string value)
            {
                byte[] bytes = Encoding.Latin1.GetBytes(value);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        //=====================================================
        // REPORT MODELS
        //=====================================================
        private sealed class WeeklyItem
        {
            public string ItemId { get; set; } = "";
            public string Description { get; set; } = "";
            public int Quantity { get; set; }
            public decimal Total { get; set; }
        }


        private sealed class MemberOrderItem
        {
            public string ItemId { get; set; } = "";
            public string Description { get; set; } = "";
            public int Quantity { get; set; }
            public decimal Total { get; set; }
        }

        private sealed class MemberOrderSummary
        {
            public string MemberId { get; set; } = "";
            public string MemberName { get; set; } = "";
            public List<MemberOrderItem> Items { get; set; } =
                new List<MemberOrderItem>();
        }
    }
}
