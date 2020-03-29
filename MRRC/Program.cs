using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRC
{
    class Program
    {
        static void Main(string[] args)
        {
            var cRM = new CRM();
            Customer customer = new Customer();
            customer.CustomerID = 1;
            customer.FirstName = "A";
            customer.LastName = "B";
            customer.DateOfBirth = DateTime.Now;
            customer.Gender = Customer.GenderType.Male;
            cRM.AddCustomer(customer);
        }
    }
}
