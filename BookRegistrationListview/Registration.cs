using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRegistrationListview
{
    internal class Registration
    {
        public Registration(int customerID, string isbn, DateTime regDate)
        {
            CustomerID = customerID;
            ISBN = isbn;
            RegDate = regDate;
        }

        public int CustomerID { get; set; }

        public string ISBN { get; set; }

        public DateTime RegDate { get; set; }

        public override string ToString()
        {
            return CustomerDB.GetCustomer(CustomerID).FullName + " - " + BookDB.GetBook(ISBN).Title + " - " + RegDate.ToShortDateString();
        }
    }
}
