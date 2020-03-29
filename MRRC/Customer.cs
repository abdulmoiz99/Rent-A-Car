using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRC
{
    class Customer
    {
        private  int customerID;
        private string customerTitle;
        private string firstName;
        private string lastName;
        private GenderType gender;
        private DateTime dateOfBirth;

        public  int CustomerID { get => customerID; set => customerID = value; }
        public string CustomerTitle { get => customerTitle; set => customerTitle = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public GenderType Gender { get => gender; set => gender = value; }

        public enum GenderType
        {
            Male,
            Female
        }
        public Customer()
        {

        }

    }
}
