using System;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        //=====================================================
        // FORM LOAD AND ACCESS CONTROL
        //=====================================================
        private void frmDashboard_Load(object sender, EventArgs e)
        {
            if (!UserSession.IsMember && !UserSession.IsAdministrator)
            {
                MessageBox.Show(
                    "Please log in before opening the dashboard.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                BeginInvoke(new Action(Close));
                return;
            }

            lblWelcome.Text = "Welcome " + UserSession.Username;
            btnAdminFunctions.Visible = UserSession.IsAdministrator;
            btnPassword.Visible = UserSession.IsMember;
            btnMembers.Text = UserSession.IsMember
                ? "Place / Update Order"
                : "Member Orders";
        }

        //=====================================================
        // MEMBER ORDERING
        //=====================================================
        private void btnMembers_Click(object sender, EventArgs e)
        {
            using (frmOrders orderForm = new frmOrders())
            {
                orderForm.ShowDialog(this);
            }
        }

        //=====================================================
        // MEMBER PASSWORD
        //=====================================================
        private void btnPassword_Click(object sender, EventArgs e)
        {
            using (frmMemberPassword passwordForm = new frmMemberPassword())
            {
                passwordForm.ShowDialog(this);
            }
        }

        //=====================================================
        // ADMINISTRATION
        //=====================================================
        private void btnAdminFunctions_Click(object sender, EventArgs e)
        {
            if (!UserSession.IsAdministrator)
            {
                MessageBox.Show(
                    "Administrator access is required.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using (frmAdmin admin = new frmAdmin())
            {
                admin.ShowDialog(this);
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
        // LOGOUT
        //=====================================================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Log out of the Grocery Ordering Management System?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Close();
        }
    }
}
