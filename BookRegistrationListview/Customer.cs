using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRegistrationListview
{
    public class Customer
    {
        public Customer(string title, string fName, string lName, DateTime dob)
        {
            Title = title;
            FirstName = fName;
            LastName = lName;
            DateOfBirth = dob;
        }

        public int CustomerID { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        public override string ToString()
        {
            return Title + " " + LastName + ", " + FirstName + " " + DateOfBirth.ToShortDateString();
        }
    }
}
