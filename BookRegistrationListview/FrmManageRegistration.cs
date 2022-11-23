using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRegistrationListview
{
    public partial class FrmManageRegistration : Form
    {
        public FrmManageRegistration()
        {
            InitializeComponent();
        }

        private void ManageRegistration_Load(object sender, EventArgs e)
        {
            PoplulateRegistrationListBox();
            PoplulateCustomerListBox();

            // disable tab index of some controls
            lblRegistrationManagingTool.TabStop = false;
            grbCustomersAndBooks.TabStop = false;
            grbRegistrations.TabStop = false;
        }

        /// <summary>
        /// Populates a listbox for all Registrations
        /// </summary>
        private void PoplulateRegistrationListBox()
        {
            lstRegistrations.Items.Clear();

            List<Registration> registrations = BookRegistrationDB.GetAllRegistrations();

            foreach (Registration currReg in registrations)
            {
                // Add entire Registration object to listbox
                lstRegistrations.Items.Add(currReg);
            }

            btnRemoveRegistration.Enabled = false;

            // onload or when re-populating listbox after user's activities 
            // enable Add button
            // Update button and Delete button are disabled until an item in listbox selected
            /*
            txtISBN.Enabled = true;
            btnAddBook.Enabled = true;
            btnUpdateBook.Enabled = false;
            btnDeleteBook.Enabled = false;
            */
        }

        /// <summary>
        /// Populates a listbox of all Customers who have registrations
        /// </summary>
        private void PoplulateCustomerListBox()
        {
            lstCustomers.Items.Clear();

            List<Customer> customersWithRegistrations = CustomerDB.GetCustomersWithRegistrations();

            foreach (Customer currCus in customersWithRegistrations)
            {
                // Add entire Customer object to listbox
                lstCustomers.Items.Add(currCus);
            }

            btnRemoveRegisteredBook.Enabled = false;
        }

        /// <summary>
        /// Populates a listbox of registered Books of a selected Customer 
        /// </summary>
        /// <param name="customerID"></param>
        private void PoplulateBooksAndRegDateListBox(int customerID)
        {
            lstBooksAndRegDate.Items.Clear();

            List<Registration> registrationsByID = BookRegistrationDB.GetRegistrationsByCustomerID(customerID);

            foreach (Registration currReg in registrationsByID)
            {
                // Add entire book object to listbox
                lstBooksAndRegDate.Items.Add(BookDB.GetBook(currReg.ISBN));
            }
        }

        private void lstCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show a message when users click on blank part of listbox
            if (lstCustomers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer to view registered books!");
                return;
            }
            btnRemoveRegisteredBook.Enabled = false;
            Customer selectedCustomer = lstCustomers.SelectedItem as Customer;
            PoplulateBooksAndRegDateListBox(selectedCustomer.CustomerID);
        }

        private void lstRegistrations_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show a message when users click on blank part of listbox
            if (lstRegistrations.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a registration to remove!");
                return;
            }

            btnRemoveRegistration.Enabled = true;
        }

        private void btnRemoveRegistration_Click(object sender, EventArgs e)
        {
            Registration selectedReg = lstRegistrations.SelectedItem as Registration;

            try
            {
                BookRegistrationDB.Delete(selectedReg.CustomerID, selectedReg.ISBN);
                MessageBox.Show($"{selectedReg} has been removed succesfully!");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("That registration no longer exists");
                PoplulateRegistrationListBox();
                PoplulateCustomerListBox();
                lstBooksAndRegDate.Items.Clear();
            }
            catch (SqlException)
            {
                MessageBox.Show("We are having server issues. Please try again later!");
            }

            PoplulateRegistrationListBox();
            PoplulateCustomerListBox();
            lstBooksAndRegDate.Items.Clear();
            lstRegistrations.Focus();
        }

        private void btnRemoveRegisteredBook_Click(object sender, EventArgs e)
        {
            Customer selectedCus = (Customer)lstCustomers.SelectedItem;
            Book selectedBook = (Book)lstBooksAndRegDate.SelectedItem;

            try
            {
                BookRegistrationDB.Delete(selectedCus.CustomerID, selectedBook.ISBN);
                MessageBox.Show($"Registration of {selectedCus.FullName} for {selectedBook.Title} has been removed succesfully!");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("That registration no longer exists");
                PoplulateRegistrationListBox();
                PoplulateCustomerListBox();
                lstBooksAndRegDate.Items.Clear();
            }
            catch (SqlException)
            {
                MessageBox.Show("We are having server issues. Please try again later!");
            }

            PoplulateRegistrationListBox();
            PoplulateCustomerListBox();
            lstBooksAndRegDate.Items.Clear();
            lstCustomers.Focus();
        }

        private void lstBooksAndRegDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show a message when users click on blank part of listbox
            if (lstBooksAndRegDate.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a book to remove the registration!");
                btnRemoveRegisteredBook.Enabled = false;
                return;
            }

            btnRemoveRegisteredBook.Enabled = true;
        }
    }
}
