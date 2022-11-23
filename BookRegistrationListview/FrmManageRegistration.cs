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
            // create collumns for Customer listview
            lviCustomers.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lviCustomers.Columns.Add("FullName", 140, HorizontalAlignment.Left);
            lviCustomers.Columns.Add("DateOfBirth", 75, HorizontalAlignment.Left);

            // create collumns for Books and RegDates listview
            lviBooksAndRegDate.Columns.Add("ISBN", 90, HorizontalAlignment.Left);
            lviBooksAndRegDate.Columns.Add("Book Title", 160, HorizontalAlignment.Left);
            lviBooksAndRegDate.Columns.Add("Price", 55, HorizontalAlignment.Left);
            lviBooksAndRegDate.Columns.Add("Reg Date", 75, HorizontalAlignment.Left);

            // create collumns for Registrations listview
            lviRegistrations.Columns.Add("Customer ID", 90, HorizontalAlignment.Left);
            lviRegistrations.Columns.Add("Full Name", 90, HorizontalAlignment.Left);
            lviRegistrations.Columns.Add("ISBN", 150, HorizontalAlignment.Left);
            lviRegistrations.Columns.Add("Book Title", 210, HorizontalAlignment.Left);
            lviRegistrations.Columns.Add("Reg Date", 100, HorizontalAlignment.Left);

            PoplulateRegistrationListView();
            PoplulateCustomerListView();

            // disable tab index of some controls
            lblRegistrationManagingTool.TabStop = false;
            grbCustomersAndBooks.TabStop = false;
            grbRegistrations.TabStop = false;
        }

        /// <summary>
        /// Populates a listbox for all Registrations
        /// </summary>
        private void PoplulateRegistrationListView()
        {
            lviRegistrations.Items.Clear();

            List<Registration> registrations = BookRegistrationDB.GetAllRegistrations();

            foreach (Registration currReg in registrations)
            {
                ListViewItem item = new(new[] { currReg.CustomerID.ToString(),
                                                CustomerDB.GetCustomer(currReg.CustomerID).FullName,
                                                currReg.ISBN,
                                                BookDB.GetBook(currReg.ISBN).Title,
                                                currReg.RegDate.ToShortDateString() }); ; ;
                Tag = currReg;
                lviRegistrations.Items.Add(item);
            }

            btnRemoveRegistration.Enabled = false;
        }

        /// <summary>
        /// Populates a listview of all Customers who have registrations
        /// </summary>
        private void PoplulateCustomerListView()
        {
            lviCustomers.Items.Clear();

            List<Customer> customersWithRegistrations = CustomerDB.GetCustomersWithRegistrations();

            foreach (Customer currCustomer in customersWithRegistrations)
            {
                ListViewItem item = new(new[] { currCustomer.CustomerID.ToString(),
                                                currCustomer.FullName,
                                                currCustomer.DateOfBirth.ToShortDateString() });
                Tag = currCustomer;
                lviCustomers.Items.Add(item);
            }

            btnRemoveRegisteredBook.Enabled = false;
        }

        /// <summary>
        /// Populates a listbox of registered Books of a selected Customer 
        /// </summary>
        /// <param name="customerID"></param>
        private void PoplulateBooksAndRegDateListView(int customerID)
        {
            lviBooksAndRegDate.Items.Clear();

            List<Registration> registrationsByID = BookRegistrationDB.GetRegistrationsByCustomerID(customerID);

            foreach (Registration currReg in registrationsByID)
            {
                ListViewItem item = new(new[] { currReg.ISBN,
                                                BookDB.GetBook(currReg.ISBN).Title,
                                                BookDB.GetBook(currReg.ISBN).Price.ToString("C"),
                                                currReg.RegDate.ToShortDateString() });
                Tag = currReg;
                lviBooksAndRegDate.Items.Add(item);
            }
        }

        private void btnRemoveRegistration_Click(object sender, EventArgs e)
        {
            var selectedRegs = lviRegistrations.SelectedItems;

            foreach (ListViewItem currItem in selectedRegs)
            {
                int customerID = Convert.ToInt32(currItem.Text);
                string isbn = currItem.SubItems[2].Text;

                try
                {
                    BookRegistrationDB.Delete(customerID, isbn);
                    MessageBox.Show($"Registration of '{CustomerDB.GetCustomer(customerID).FullName}' \n" +
                                    $"for '{BookDB.GetBook(isbn).Title}' has been removed succesfully!", 
                                    "Successful!", 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.None);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show($"Registration of '{CustomerDB.GetCustomer(customerID).FullName}' \n" +
                                    $"for '{BookDB.GetBook(isbn).Title}' no longer exists",
                                    "Error!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    PoplulateRegistrationListView();
                    PoplulateCustomerListView();
                    lviBooksAndRegDate.Items.Clear();
                }
                catch (SqlException)
                {
                    MessageBox.Show("We are having server issues. Please try again later!",
                                    "Error!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
            }

            PoplulateRegistrationListView();
            PoplulateCustomerListView();
            lviBooksAndRegDate.Items.Clear();
            lviRegistrations.Focus();
        }

        private void btnRemoveRegisteredBook_Click(object sender, EventArgs e)
        {
            ListViewItem selectedCustomer = lviCustomers.SelectedItems[0];
            int customerID = Convert.ToInt32(selectedCustomer.Text);

            var selectedBooks = lviBooksAndRegDate.SelectedItems;

            foreach (ListViewItem currItem in selectedBooks)
            {
                string isbn = currItem.Text;
                try
                {
                    BookRegistrationDB.Delete(customerID, isbn);
                    MessageBox.Show($"Registration of '{CustomerDB.GetCustomer(customerID).FullName}' \n" +
                                    $"for '{BookDB.GetBook(isbn).Title}' has been removed succesfully!",
                                    "Successful!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.None);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show($"Registration of '{CustomerDB.GetCustomer(customerID).FullName}' \n" +
                                    $"for '{BookDB.GetBook(isbn).Title}' no longer exists",
                                    "Error!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    PoplulateRegistrationListView();
                    PoplulateCustomerListView();
                    lviBooksAndRegDate.Items.Clear();
                }
                catch (SqlException)
                {
                    MessageBox.Show("We are having server issues. Please try again later!",
                                    "Error!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
            }



            PoplulateRegistrationListView();
            PoplulateCustomerListView();
            lviBooksAndRegDate.Items.Clear();
            lviRegistrations.Focus();
        }


        private void lviCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show a message when users click on blank part of listbox
            if (lviCustomers.SelectedItems.Count == 0)
            {
                return;
            }

            btnRemoveRegisteredBook.Enabled = false;
            ListViewItem selectedCustomer = lviCustomers.SelectedItems[0];
            int customerID = Convert.ToInt32(selectedCustomer.Text);
            PoplulateBooksAndRegDateListView(customerID);
        }

        private void lviRegistrations_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show a message when users click on blank part of listbox
            if (lviRegistrations.SelectedItems.Count == 0)
            {
                btnRemoveRegistration.Enabled = false;
                return;
            }
            else
            {
                btnRemoveRegistration.Enabled = true;
            }
            
        }

        private void lviBooksAndRegDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lviBooksAndRegDate.SelectedItems.Count == 0)
            {
                btnRemoveRegisteredBook.Enabled = false;
                return;
            }
            else
            {
                btnRemoveRegisteredBook.Enabled = true;
            }
        }
    }
}