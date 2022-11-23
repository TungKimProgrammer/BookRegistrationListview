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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BookRegistrationListview
{
    public partial class FrmManageBook : Form
    {
        public FrmManageBook()
        {
            InitializeComponent();
        }

        private void FrmManageBook_Load(object sender, EventArgs e)
        {
            lviBooks.Columns.Add("ISBN", 120, HorizontalAlignment.Left);
            lviBooks.Columns.Add("Title", 450, HorizontalAlignment.Left);
            lviBooks.Columns.Add("Price", 80, HorizontalAlignment.Left);

            PoplulateBookListView();
            //PoplulateBookListBox();

            // disable tab index of some controls
            lblBookForm.TabStop = false;
            lblISBN.TabStop = false;
            lblPrice.TabStop = false;
            lblErrMsg.TabStop = false;

        }

        private void PoplulateBookListView()
        {
            lviBooks.Items.Clear();
            
            List<Book> books = BookDB.GetAllBooks();

            foreach (Book currBook in books)
            {
                ListViewItem item = new(new[] { currBook.ISBN, currBook.Title, currBook.Price.ToString("C") });
                Tag = currBook;
                lviBooks.Items.Add(item);
            }

            // onload or when re-populating listbox after user's activities 
            // enable Add button
            // Update button and Delete button are disabled until an item in listbox selected
            txtISBN.Enabled = true;
            btnAddBook.Enabled = true;
            btnUpdateBook.Enabled = false;
            btnDeleteBook.Enabled = false;
        }

        /// <summary>
        /// Populates a listbox of Books from database
        /// </summary>
        private void PoplulateBookListBox()
        {
            lstBooks.Items.Clear();
            List<Book> books = BookDB.GetAllBooks();

            foreach (Book currBook in books)
            {
                // Add entire book object to listbox
                lstBooks.Items.Add(currBook);
            }

            // onload or when re-populating listbox after user's activities 
            // enable Add button
            // Update button and Delete button are disabled until an item in listbox selected
            txtISBN.Enabled = true;
            btnAddBook.Enabled = true;
            btnUpdateBook.Enabled = false;
            btnDeleteBook.Enabled = false;
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                string isbn = Validator.ToStandardISBN(txtISBN.Text);
                double price = Convert.ToDouble(txtPrice.Text);
                string title = Validator.FormalizeName(txtTitle.Text);

                Book newBook = new Book(isbn, title, price);

                BookDB.Add(newBook);
                MessageBox.Show($"'{newBook.Title}' has been added succesfully!");
                PoplulateBookListView();
                //PoplulateBookListBox();
                clearTextbox();
                //System.Threading.Thread.Sleep(3000);
                //lblNotice.Text = "";
            }
        }

        /// <summary>
        /// Validates input before adding to database
        /// </summary>
        /// <returns>True when all conditions are met</returns>
        private bool IsValid()
        {
            if (IsValidInput())
            {
                if (Validator.IsExistedISBN(txtISBN.Text))
                {
                    lblErrMsg.Text = "This ISBN already exists in the database!";
                    return false;
                }
                lblErrMsg.Text = "";
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validates input đata
        /// </summary>
        /// <returns>True when all input textboxes are filled with valid data: 
        /// Price should be a number equals to or greater than 0
        /// ISBN should contain dashes and maximum of 13 digits
        /// </returns>
        private bool IsValidInput()
        {
            // all textboxes are filled
            if (Validator.IsPresent(txtISBN) &&
                Validator.IsPresent(txtTitle) &&
                Validator.IsPresent(txtPrice))
            {
                // ISBN is valid and not existed, and price is a number
                if (Validator.IsValidISBN(txtISBN.Text) &&
                    double.TryParse(txtPrice.Text, out double price))
                {
                    if (price < 0)
                    {
                        lblErrMsg.Text = "Price should be greater than or equal to 0!";
                        return false;
                    }
                    lblErrMsg.Text = "";
                    return true;
                }
                else
                {
                    if (!Validator.IsValidISBN(txtISBN.Text))
                    {
                        lblErrMsg.Text = "ISBN should contain dashes and maximum 13 digits!";
                    }
                    if (!double.TryParse(txtPrice.Text, out _))
                    {
                        lblErrMsg.Text = "Price should be a number!";
                    }
                    return false;
                }
            }

            lblErrMsg.Text = "All textboxes shouldn't be empty!";
            return false;
        }

        /// <summary>
        /// Clears all textboxes
        /// </summary>
        private void clearTextbox()
        {
            txtISBN.Text = "";
            txtISBN.Focus();
            txtTitle.Text = "";
            txtPrice.Text = string.Empty;
            txtISBN.Enabled = true;
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            
            var selectedBooks = lviBooks.SelectedItems;

            foreach (ListViewItem currItem in selectedBooks)
            {
                Book currBook = new(currItem.Text, currItem.SubItems[1].Text, Convert.ToDouble(currItem.SubItems[2].Text.Substring(1)));

                // count Registrations of selected Book
                int countRegistrationsGroupByISBN = BookRegistrationDB.CountRegistrationsGroupByISBN(currBook.ISBN);

                try
                {
                    BookDB.Delete(currBook);
                    clearTextbox();
                    lblErrMsg.Text = "";
                    MessageBox.Show($"'{currBook.Title}' has been deleted succesfully!");
                }
                catch (ArgumentException)
                {
                    MessageBox.Show($"'{currBook.Title}' no longer exists");
                    PoplulateBookListView();
                    //PoplulateBookListBox();
                }
                catch (SqlException)
                {
                    if (countRegistrationsGroupByISBN > 0)
                    {
                        MessageBox.Show($"'{currBook.Title}' already has Registrations. \n" +
                                        $"Please remove all Registrations for '{currBook.Title}' first!");
                    }
                    else
                    {
                        MessageBox.Show("We are having server issues. Please try again later!");
                    }
                    clearTextbox();
                }
            }

            PoplulateBookListView();
            //PoplulateBookListBox();
        }

        private void bnUpdateBook_Click(object sender, EventArgs e)
        {
            if (IsValidInput())
            {
                string isbn = Validator.ToStandardISBN(txtISBN.Text);
                double price = Convert.ToDouble(txtPrice.Text);
                string title = Validator.FormalizeName(txtTitle.Text);

                Book updateBook = new(isbn, title, price);

                BookDB.Update(updateBook);
                MessageBox.Show($"'{updateBook.Title}' has been updated successfully!");

                PoplulateBookListView();
                clearTextbox();
            }
        }

        private void lstBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblErrMsg.Text = "";

            // show a message when users click on blank part of listbox
            if (lstBooks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a book to Update or Delete!");
                return;
            }

            // when users select an item
            // enable Update button and Delete button
            // disable Add button
            btnUpdateBook.Enabled = true;
            btnDeleteBook.Enabled = true;
            btnAddBook.Enabled = false;

            Book selectedBook = (Book)lstBooks.SelectedItem;

            // Tung's code:
            // users are not allowed to update/modify ISBN of a book as it is unique
            // in case of ISBN typos, best solution is to delete the item and add again
            txtISBN.Enabled = false;

            // show info to input textboxes when users select an item
            txtISBN.Text = selectedBook.ISBN;
            txtPrice.Text = selectedBook.Price.ToString();
            txtTitle.Text = selectedBook.Title;
        }

        private void lviBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lviBooks.SelectedItems.Count == 0)
            {
                // MessageBox.Show("Please add or select a Book to update or delete!");
                btnDeleteBook.Enabled = false;
                btnUpdateBook.Enabled = false;
                btnAddBook.Enabled = true;
                clearTextbox();

                foreach (ListViewItem currItem in lviBooks.Items)
                {
                    currItem.Checked = false;
                }
                return;
            }
            else
            {
                // enable Update and Delete buttons when 1 item is selected
                if (lviBooks.SelectedItems.Count == 1)
                {
                    btnUpdateBook.Enabled = true;
                    txtISBN.Enabled = false;
                    ListViewItem selectedBook = lviBooks.SelectedItems[0];
                    txtISBN.Text = selectedBook.Text;
                    txtTitle.Text = selectedBook.SubItems[1].Text;
                    txtPrice.Text = selectedBook.SubItems[2].Text.Substring(1);
                }

                // disable Update button when multiple items are selected
                else
                {
                    btnUpdateBook.Enabled = false;
                    clearTextbox();
                }
                btnDeleteBook.Enabled = true;
                btnAddBook.Enabled = false;
            }

            /* item(s)'s checkbox instantly set checked when item(s) selected
             * working on backwards: item(s) not set selected/highlighted instantly when checked/unchecked
             * right now, 1 click delayed and some bugs on check/uncheck repeatedly on 1 item
             
            foreach (ListViewItem currItem in lviBooks.Items)
            {
                
                // check and uncheck as item selected or deselected
                if (currItem.Selected)
                {
                    currItem.Checked = true;
                }
                else
                {
                    currItem.Checked = false;
                }
                
                // select or deselect as item checked or unchecked
                if (currItem.Checked)
                {
                    currItem.Selected = true;
                }
                else
                {
                    currItem.Selected = false;
                }
                
            }
            */
        }


        public void lviBooks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            /* item(s)'s checkbox instantly set checked when item(s) selected
             * working on backwards: item(s) not set selected/highlighted instantly when checked/unchecked
             * right now, 1 click delayed and some bugs on check/uncheck repeatedly on 1 item
           
            
            foreach (ListViewItem currItem in lviBooks.Items)
            {
                // select or deselect as item checked or unchecked
                if (currItem.Checked)
                {
                    currItem.Selected = true;
                }
                else
                {
                    currItem.Selected = false;
                }
            }
            */
        }
    }
}
