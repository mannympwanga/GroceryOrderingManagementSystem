using System;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        //=====================================================
        // FORM LOAD AND ACCESS CONTROL
        //=====================================================
        private void frmAdmin_Load(object sender, EventArgs e)
        {
            if (UserSession.IsAdministrator)
                return;

            MessageBox.Show(
                "Access denied. A valid administrator (Matron) login is required.",
                "Unauthorised Access",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            BeginInvoke(new Action(Close));
        }

        //=====================================================
        // MEMBER MAINTENANCE
        //=====================================================
        private void btnMembers_Click(object sender, EventArgs e)
        {
            using (frmMembers memberForm = new frmMembers())
            {
                memberForm.ShowDialog(this);
            }
        }

        //=====================================================
        // GROCERY ITEM MAINTENANCE
        //=====================================================
        private void btnItems_Click(object sender, EventArgs e)
        {
            using (frmGroceryItems itemForm = new frmGroceryItems())
            {
                itemForm.ShowDialog(this);
            }
        }

        //=====================================================
        // USER AND ROLE MAINTENANCE
        //=====================================================
        private void btnUsers_Click(object sender, EventArgs e)
        {
            using (frmUsers userForm = new frmUsers())
            {
                userForm.ShowDialog(this);
            }
        }

        //=====================================================
        // REPORTS
        //=====================================================
        private void btnReports_Click(object sender, EventArgs e)
        {
            using (frmReports reportForm = new frmReports())
            {
                reportForm.ShowDialog(this);
            }
        }

        //=====================================================
        // DATABASE BACKUP AND RECOVERY
        //=====================================================
        private void btnDatabase_Click(object sender, EventArgs e)
        {
            using (frmDatabaseMaintenance databaseForm =
                   new frmDatabaseMaintenance())
            {
                databaseForm.ShowDialog(this);
            }
        }

        //=====================================================
        // ABOUT
        //=====================================================
        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (frmAbout aboutForm = new frmAbout())
            {
                aboutForm.ShowDialog(this);
            }
        }

        //=====================================================
        // CLOSE ADMINISTRATION
        //=====================================================
        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAdmin_Load_1(object sender, EventArgs e)
        {

        }
    }
}
