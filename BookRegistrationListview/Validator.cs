using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookRegistrationListview
{
    internal static class Validator
    {
        /// <summary>
        /// Checks if a textbox is null or empty or whitespaced
        /// </summary>
        /// <param name="box">The textbox to check</param>
        /// <returns>True if <paramref name="box"/> is filled</returns>
        public static bool IsPresent(TextBox box)
        {
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a string is a valid datetime format
        /// </summary>
        /// <param name="dateString">The string to validate</param>
        /// <returns>True if <paramref name="dateString"/>input is a valid datetime</returns>
        public static bool IsValidDate(string dateString)
        {
            // The "_" is the discard variable. Meaning we are ignoring
            if (DateTime.TryParse(dateString, out _))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// checks if a isbn is valid
        /// </summary>
        /// <param name="isbn">isbn to be checked</param>
        /// <returns>true if <paramref name="isbn"/>contains only digits and dashes</returns>
        public static bool IsValidISBN(string isbn)
        {
            isbn = RemoveAllWhiteSpaces(isbn);
            // remove any dashes within the isbn
            isbn = Regex.Replace(isbn, "-", "");
            return IsDigitsOnly(isbn) && isbn.Length <= 13;
        }

        public static bool IsExistedISBN(string isbn)
        {
            bool isExisted = false;
            List<Book> books = BookDB.GetAllBooks();

            foreach (Book currBook in books)
            {
                isbn = ToStandardISBN(isbn);
                if (isbn == currBook.ISBN)
                {
                    isExisted = true;
                }
            }
            return isExisted;
        }

        /// <summary>
        /// Standardizes the ISBN
        /// Removes dashes and white spaces
        /// Adds prefix 0 to make 13 char length
        /// </summary>
        /// <param name="isbn">ISBN to make standard</param>
        /// <returns>Standard ISBN</returns>
        public static string ToStandardISBN(string isbn)
        {
            // remove all leading and trailing white-space characters
            isbn = isbn.Trim();

            // remove any dashes within the isbn
            isbn = Regex.Replace(isbn, "-", "");

            // add prefix 0 to ISBN if less than 13 digits
            while (isbn.Length < 13)
            {
                isbn = "0" + isbn;
            }

            return isbn;
        }

        /// <summary>
        /// checks if a string contains only digits and dashes
        /// </summary>
        /// <param name="str">string to be checked</param>
        /// <returns>true if <paramref name="str"/> contains only digits or dashes</returns>
        public static bool IsDigitsOnly(string str)
        {
            // remove any dashes within the string
            str = Regex.Replace(str, "-", "");


            foreach (char c in str)
            {
                if (c < '0' || c > '9') // can use c.IsDigit() but slower
                    return false;
            }

            return true;
        }

        /// <summary>
        /// removes all leading and trailing white-space characters
        /// removes all spaces of a string
        /// </summary>
        /// <param name="input">string to remove all white spaces</param>
        /// <returns>solid string without any white spaces</returns>
        public static string RemoveAllWhiteSpaces(string input)
        {
            // remove all leading and trailing white-space characters
            input = input.Trim();

            // remove any spaces within the string
            input = Regex.Replace(input, " ", "");

            // Efficient way to remove ALL whitespace from a string
            // Regex.Replace(input, @"\s+", "");
            return input;
        }

        /// <summary>
        /// fomalize a single/multiple-word name/title
        /// by removing all leading and trailing white-space characters
        /// and replacing multiple spaces by single space within a name/title
        /// </summary>
        /// <param name="name">Name/Title to be formalized</param>
        /// <returns>Formalized Name/Title with properly capitalized and unnecessary spaces </returns>
        public static string FormalizeName(string name)
        {
            // remove all leading and trailing white-space characters
            name = name.Trim();

            // replace multiple spaces with single space within a name
            name = Regex.Replace(name, " {2,}", " ");

            // properly capitalize multiple-word name
            name = Regex.Replace(name, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
            return name;
        }
    }
}
