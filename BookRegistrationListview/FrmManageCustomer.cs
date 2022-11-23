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

        private void FrmManageCustomer_Load(object sender, EventArgs e)
        {
            // create collumns for Customer listview
            lviCustomers.Columns.Add("ID", 45, HorizontalAlignment.Left);
            lviCustomers.Columns.Add("Title", 50, HorizontalAlignment.Left);
            lviCustomers.Columns.Add("LastName", 100, HorizontalAlignment.Left);
            lviCustomers.Columns.Add("FirstName", 100, HorizontalAlignment.Left);
            lviCustomers.Columns.Add("DateOfBirth", 100, HorizontalAlignment.Left);

            PoplulateCustomerListView();
           // PoplulateCustomerListView();

            // disable tab index of some controls
            lblCustomerForm.TabStop = false;
            lblTitle.TabStop = false;
            lblFirstName.TabStop = false;
            lblLastName.TabStop = false;
            lblDOB.TabStop = false;
        }

        /// <summary>
        /// Populates a listview of Customers in from database
        /// </summary>
        private void PoplulateCustomerListView()
        {
            lviCustomers.Items.Clear();

            List<Customer> customers = CustomerDB.GetAllCustomers();

            foreach (Customer currCustomer in customers)
            {
                ListViewItem item = new(new[] { currCustomer.CustomerID.ToString(), currCustomer.Title, currCustomer.LastName, currCustomer.FirstName, currCustomer.DateOfBirth.ToShortDateString() });
                Tag = currCustomer;
                lviCustomers.Items.Add(item);
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
                MessageBox.Show($"'{newCus.FullName}' has been added succesfully!",
                                "Successful!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.None);
                PoplulateCustomerListView();
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
                if (dob > DateTime.Today)
                {
                    lblErrMsg.Text = "Date of birth can't be greater than today!";
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
            lblErrMsg.Text = "";
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            var selectedCustomers = lviCustomers.SelectedItems;

            foreach (ListViewItem currItem in selectedCustomers)
            {
                int customerID = Convert.ToInt32(currItem.Text);
                /* customerID is enough to delete a customer from DB
                string title = currItem.SubItems[1].Text;
                string lastName = currItem.SubItems[2].Text;
                string firstName = currItem.SubItems[3].Text;
                DateTime dob = DateTime.Parse(currItem.SubItems[4].Text);

                Customer currCus = new(customerID, title, firstName, lastName, dob);
                */

                Customer currCus = CustomerDB.GetCustomer(customerID);

               // count Registrations of selected Customer
               int countRegistrationsByCustomerID = BookRegistrationDB.CountRegistrationsGroupByCustomerID(customerID);

                try
                {
                    CustomerDB.Delete(customerID);
                    clearTextbox();
                    lblErrMsg.Text = "";
                    MessageBox.Show($"'{currCus.FullName}' has been deleted succesfully!",
                                    "Successful!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.None);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show($"'{currCus.FullName}' no longer exists",
                                    "Error!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    PoplulateCustomerListView();
                }
                catch (SqlException)
                {
                    if (countRegistrationsByCustomerID > 0)
                    {
                        MessageBox.Show($"'{CustomerDB.GetCustomer(customerID).FullName}' already has Registrations. \n" +
                                        $"Please remove all Registrations for '{currCus.FullName}' first!",
                                        "Error!",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("We are having server issues. Please try again later!",
                                        "Error!",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    }
                    clearTextbox();
                }
            }

            PoplulateCustomerListView();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (IsValidInput())
            {
                // get CustomerID from selected customer in listview
                ListViewItem selectedCustomer = lviCustomers.SelectedItems[0];
                int customerID = Convert.ToInt32(selectedCustomer.Text);
                
                string title = Validator.FormalizeName(txtTitle.Text);
                string firstName = Validator.FormalizeName(txtFirstName.Text);
                string lastName = Validator.FormalizeName(txtLastName.Text);
                DateTime dob = dtpDOB.Value.Date;

                Customer updateCustomer = new(title, firstName, lastName, dob);
                updateCustomer.CustomerID = customerID;

                CustomerDB.Update(updateCustomer);
                MessageBox.Show($"'{updateCustomer.FullName}' has been updated successfully!",
                                "Successful!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.None);
                PoplulateCustomerListView();
                clearTextbox();
            }
        }

        private void lviCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lviCustomers.SelectedItems.Count == 0)
            {
                btnDeleteCustomer.Enabled = false;
                btnUpdateCustomer.Enabled = false;
                btnAddCustomer.Enabled = true;
                clearTextbox();
                lblErrMsg.Text = "";
                return;
            }
            else
            {
                // enable Update and Delete buttons when 1 item is selected
                if (lviCustomers.SelectedItems.Count == 1)
                {
                    btnUpdateCustomer.Enabled = true;
                    ListViewItem selectedCustomer = lviCustomers.SelectedItems[0];
                    int customerID = Convert.ToInt32(selectedCustomer.Text);
                    txtTitle.Text = selectedCustomer.SubItems[1].Text;
                    txtLastName.Text = selectedCustomer.SubItems[2].Text;
                    txtFirstName.Text = selectedCustomer.SubItems[3].Text;
                    dtpDOB.Value = DateTime.Parse(selectedCustomer.SubItems[4].Text);
                }

                // disable Update button when multiple items are selected
                else
                {
                    btnUpdateCustomer.Enabled = false;
                    clearTextbox();
                }
                btnDeleteCustomer.Enabled = true;
                btnAddCustomer.Enabled = false;
            }
        }
             
        /// <summary>
        /// Enable Add Customer Button when all input is valid
        /// Disable when all conditions not met
        /// </summary>
        private void ToggleAddButton()
        {
            if (IsValidInput())
            {
                btnAddCustomer.Enabled = true;
            }
            else
            {
                btnAddCustomer.Enabled = false;
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            ToggleAddButton();
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            ToggleAddButton();
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            ToggleAddButton();
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            ToggleAddButton();
        }
    }
}
