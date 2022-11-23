using System.Data.SqlClient;

namespace BookRegistrationListview
{
    public partial class FrmBookRegistration : Form
    {
        public FrmBookRegistration()
        {
            InitializeComponent();
        }

        private void FrmBookRegistration_Load(object sender, EventArgs e)
        {
            resetForm();

            // disable tab index of some controls
            lblCustomers.TabStop = false;
            lblBooks.TabStop = false;
            lblDate.TabStop = false;
            lblBookRegistration.TabStop = false;
            lblErrMsg.TabStop = false;
        }

        /// <summary>
        /// Populates Customer Combo Box - Dropdown List
        /// </summary>
        private void PopulateCustomerComboBox()
        {
            cbxCustomerName.Items.Clear();

            List<Customer> customers = CustomerDB.GetAllCustomers();

            foreach (Customer currCus in customers)
            {
                // Add entire customer object to combo box
                cbxCustomerName.Items.Add(currCus);
            }
        }

        private void cbxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCustomerName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer to show available books to register!");
                return;
            }

            Customer selectedCus = (Customer)cbxCustomerName.SelectedItem;
            PopulateBookComboBox(selectedCus.CustomerID);
        }

        /// <summary>
        /// Populates combo box of Books onload
        /// </summary>
        private void PopulateBookComboBox()
        {
            cbxBookTitle.Items.Clear();

            List<Book> allValidBooks = BookDB.GetAllValidBooks();

            foreach (Book currBook in allValidBooks)
            {
                // Add entire book object to combo box
                cbxBookTitle.Items.Add(currBook);
            }
        }

        /// <summary>
        /// Populates combo box for Books not yet registered by a Customer
        /// when users select a customer from Customer combo box
        /// </summary>
        /// <param name="customerID">CustomerID of a Customer</param>
        private void PopulateBookComboBox(int customerID)
        {
            cbxBookTitle.Items.Clear();

            List<Book> booksNotYetRegisterByCustomerID = BookDB.GetBooksNotYetRegisterByCustomerID(customerID);

            foreach (Book currBook in booksNotYetRegisterByCustomerID)
            {
                // Add entire book object to combo box
                cbxBookTitle.Items.Add(currBook);
            }

            if (booksNotYetRegisterByCustomerID.Count == 0)
            {
                MessageBox.Show("This customer has registered all available books!");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // make sure users select a customer and a book to register
            if (cbxCustomerName.SelectedIndex == -1 ||
                cbxBookTitle.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer and a book to register!");
                return;
            }

            Customer selectedCus = (Customer)cbxCustomerName.SelectedItem;
            Book selectedBook = (Book)cbxBookTitle.SelectedItem;
            DateTime regDate = dtpRegistration.Value;

            Registration newReg = new Registration(selectedCus.CustomerID, selectedBook.ISBN, regDate);

            bool result = BookRegistrationDB.RegisterBook(newReg);
            if (!result)
            {
                MessageBox.Show("We are having server issues. Please try again later!");
            }
            MessageBox.Show($"Registration of {selectedCus.FullName} for {selectedBook.Title} has been addded succesfully!");

            /*
            try
            {
                bool result = BookRegistrationDB.RegisterBook(newReg);
                MessageBox.Show($"Registration of {selectedCus.FullName} for {selectedBook.Title} has been addded succesfully!");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("That customer or book no longer exists");
                resetForm();
            }
            catch (SqlException)
            {
                MessageBox.Show("We are having server issues. Please try again later!");
            }
            */

            // refresh combo box after users add/update/delete 
            resetForm();
        }

        private void btnManageCustomer_Click(object sender, EventArgs e)
        {
            FrmManageCustomer cusMngFrm = new FrmManageCustomer();
            cusMngFrm.ShowDialog();

            // refresh combo box after users add/update/delete 
            resetForm();
        }

        private void btnManageBook_Click(object sender, EventArgs e)
        {
            FrmManageBook bookMngFrm = new FrmManageBook();
            bookMngFrm.ShowDialog();

            // refresh combo box after users add/update/delete 
            resetForm();
        }


        private void btnManageRegistration_Click(object sender, EventArgs e)
        {
            FrmManageRegistration registrationMngFrm = new FrmManageRegistration();
            registrationMngFrm.ShowDialog();

            // refresh combo box after users add/update/delete 
            resetForm();
        }

        /// <summary>
        /// Enable Register Book Button when all input is valid
        /// Disable when all conditions not met
        /// </summary>
        private void ToggleBtnRegister()
        {
            if (IsValid())
            {
                btnRegister.Enabled = true;
            }
            else
            {
                btnRegister.Enabled = false;
            }
        }

        /// <summary>
        /// Validates input date
        /// </summary>
        /// <returns>True when all input is valid</returns>
        private bool IsValid()
        {
            if (cbxCustomerName.SelectedIndex != -1 &&
                cbxBookTitle.SelectedIndex != -1 &&
                dtpRegistration.Value >= DateTime.Today)
            {
                lblErrMsg.Text = "";
                return true;
            }
            else
            {
                if (dtpRegistration.Value < DateTime.Today)
                {
                    lblErrMsg.Text = "Date can't be in the past!";
                }
                return false;
            }

        }

        private void dtpRegistration_ValueChanged(object sender, EventArgs e)
        {
            ToggleBtnRegister();
        }

        /// <summary>
        /// Clears input 
        /// </summary>
        private void resetForm()
        {
            PopulateCustomerComboBox();
            PopulateBookComboBox();
            lblErrMsg.Text = "";
            dtpRegistration.Value = DateTime.Today;
        }

        private void cbxBookTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleBtnRegister();
        }
    }
}