using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRegistrationListview
{
    public partial class FrmManageCustomer : Form
    {
        public FrmManageCustomer()
        {
            InitializeComponent();
        }

        // Joe's lecture: used when using Add Customwer Form separated with Manage Customer Form
        public FrmManageCustomer(Customer c)
        {
            InitializeComponent(); // creates form controls

            /* Joe's Code
            btnAddCustomer.Text = "Update Customer";
            txtTitle.Text = c.Title;
            txtFirstName.Text = c.FirstName;
            txtLastName.Text = c.LastName;
            dtpDOB.Value = c.DateOfBirth;
            */
        }

        private void FrmManageCustomer_Load(object sender, EventArgs e)
        {
            PoplulateCustomerListBox();

            // disable tab index of some controls
            lblCustomerForm.TabStop = false;
            lblTitle.TabStop = false;
            lblFirstName.TabStop = false;
            lblLastName.TabStop = false;
            lblDOB.TabStop = false;
        }

        /// <summary>
        /// Populates a listbox of Customers in from database
        /// </summary>
        private void PoplulateCustomerListBox()
        {
            lstCustomers.Items.Clear();
            List<Customer> customers = CustomerDB.GetAllCustomers();

            foreach (Customer currCus in customers)
            {
                // Add entire customer object to listbox
                lstCustomers.Items.Add(currCus);
            }

            // onload or when re-populating listbox after user's activities 
            // enable Add button
            // Update button and Delete button are disabled until an item in listbox selected
            btnAddCustomer.Enabled = true;
            btnUpdateCustomer.Enabled = false;
            btnDeleteCustomer.Enabled = false;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (IsValidInput())
            {
                string title = Validator.FormalizeName(txtTitle.Text);
                string firsName = Validator.FormalizeName(txtFirstName.Text);
                string lastName = Validator.FormalizeName(txtLastName.Text);
                DateTime dob = dtpDOB.Value.Date;

                Customer newCus = new Customer(title, firsName, lastName, dob);

                CustomerDB.Add(newCus);
                MessageBox.Show($"{newCus.FullName} has been added succesfully!");
                PoplulateCustomerListBox();
                clearTextbox();
            }

        }

        /// <summary>
        /// Validates input đata
        /// </summary>
        /// <returns>True when all input textboxes are filled with valid data: 
        /// Date of birth should be before today's date
        /// </returns>
        private bool IsValidInput()
        {
            DateTime dob = dtpDOB.Value;

            // all textboxes are filled
            if (Validator.IsPresent(txtTitle) &&
                Validator.IsPresent(txtFirstName) &&
                Validator.IsPresent(txtLastName) &&
                dob < DateTime.Today)
            {
                lblErrMsg.Text = "";
                return true;
            }
            else
            {
                if (!Validator.IsPresent(txtTitle) ||
                    !Validator.IsPresent(txtFirstName) ||
                    !Validator.IsPresent(txtLastName))
                {
                    lblErrMsg.Text = "All textboxes shouldn't be empty!";
                }
                if (dob >= DateTime.Today)
                {
                    lblErrMsg.Text = "Date of birth can't be greater than or equal to today!";
                }
                return false;
            }

        }

        /// <summary>
        /// Clears all textboxes and set datetimepicker to today's date
        /// </summary>
        private void clearTextbox()
        {
            txtTitle.Text = "";
            txtTitle.Focus();
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dtpDOB.Value = DateTime.Today;

        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            /* not neccessary when using button enable/disable 
            // Return if no customer is selected
            if (lstCustomers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer to delete!");
                return;
            }
            */

            Customer selectedCustomer = (Customer)lstCustomers.SelectedItem;

            int countRegistrationsByCustomerID = BookRegistrationDB.CountRegistrationsGroupByCustomerID(selectedCustomer.CustomerID);

            try
            {
                CustomerDB.Delete(selectedCustomer.CustomerID);
                clearTextbox();
                lblErrMsg.Text = "";
                MessageBox.Show($"{selectedCustomer.FullName} has been deleted succesfully!");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("That customer no longer exists");
                PoplulateCustomerListBox();
            }
            catch (SqlException)
            {
                if (countRegistrationsByCustomerID > 0)
                {
                    MessageBox.Show("You can't delete the Customers that already have Registrations. \n" +
                                    "Please remove all Registrations for these Customers first!");
                }
                else
                {
                    MessageBox.Show("We are having server issues. Please try again later!");
                }
                clearTextbox();
            }

            PoplulateCustomerListBox();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            /* not neccessary when using button enable/disable 
            if (lstCustomers.SelectedIndex == -1)
            {
                MessageBox.Show("A customer must be selected!");
                return;
            }
            */

            Customer selectedCustomer = (Customer)lstCustomers.SelectedItem;

            // Tung's code:

            if (IsValidInput())
            {
                selectedCustomer.Title = Validator.FormalizeName(txtTitle.Text);
                selectedCustomer.FirstName = Validator.FormalizeName(txtFirstName.Text);
                selectedCustomer.LastName = Validator.FormalizeName(txtLastName.Text);
                selectedCustomer.DateOfBirth = dtpDOB.Value.Date;

                CustomerDB.Update(selectedCustomer);
                PoplulateCustomerListBox();
                clearTextbox();
            }


            /*
            // Joe's lecture:
            FrmManageCustomer updateCustomer = new();
            updateCustomer.ShowDialog();
            */
        }

        private void lstCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblErrMsg.Text = "";

            // show a message when users click on blank part of listbox
            if (lstCustomers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer to Update or Delete!");
                return;
            }

            // when users select an item
            // enable Update button and Delete button
            // disable Add button
            btnUpdateCustomer.Enabled = true;
            btnDeleteCustomer.Enabled = true;
            btnAddCustomer.Enabled = false;

            Customer selectedCustomer = (Customer)lstCustomers.SelectedItem;

            // Tung's code:
            // show info to input textboxes when users select an item
            txtTitle.Text = selectedCustomer.Title;
            txtFirstName.Text = selectedCustomer.FirstName;
            txtLastName.Text = selectedCustomer.LastName;
            dtpDOB.Value = selectedCustomer.DateOfBirth;


        }
    }
}
