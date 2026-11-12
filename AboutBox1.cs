using System;
using System.Windows.Forms;

namespace GroceryOrderingManagementSystem
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();

            //=====================================================
            // APPLICATION AND STUDENT INFORMATION
            //=====================================================
            Text = "About Grocery Ordering Management System";

            labelProductName.Text =
                "Product Name: Grocery Ordering Management System";

            labelVersion.Text = "Version: 1.0";

            labelCopyright.Text =
                "© 2026 Emmanuel Mpwanga";

            labelCompanyName.Text =
                "University of South Africa (UNISA)";

            textBoxDescription.Text =
                "Student: Emmanuel Mpwanga" + Environment.NewLine +
                "Student Number: 17945968" + Environment.NewLine +
                "Module: ICT3715" + Environment.NewLine +
                "About: The Grocery Ordering and Management System aims to deliver a structured, dependable, and easy-to-use solution for managing weekly grocery orders in an old age home. It is designed to improve day-to-day efficiency and support management decision-making through accurate data collection and reporting. " + Environment.NewLine +
                "Year: 2026";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //=====================================================
        // DESIGNER EVENT HANDLERS
        //=====================================================
        
        private void labelProductName_Click(object sender, EventArgs e)
        {
        }

        private void logoPictureBox_Click(object sender, EventArgs e)
        {
        }

        private void labelVersion_Click(object sender, EventArgs e)
        {
        }

        private void labelCompanyName_Click(object sender, EventArgs e)
        {
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
