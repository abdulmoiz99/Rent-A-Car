using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRC
{
    class CRM
    {
        List<Customer> customers;

        public CRM()
        {
            customers = new List<Customer>();
        }

        public int GetCustomerCount()
        {
            return customers.Count;
        }
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
            WriteDataToCSV( customer);
        }
        public void ModifyCustomer(int customerID)
        {

        }
        public void DeleteCustomer(int customerID)
        {
            foreach (Customer customer in customers)
            {
                if (customer.CustomerID==customerID)
                {
                    customers.Remove(customer);
                }
            }
        }
        public void WriteDataToCSV(Customer customer)
        {
            var csv = new StringBuilder();
            var newLine = string.Format("{0},{1},{2},{3},{4},{5}", customer.CustomerID, customer.CustomerTitle, customer.FirstName,customer.LastName,customer.Gender, customer.DateOfBirth);
            csv.AppendLine(newLine);
            File.AppendAllText(@"C:\Users\ACCER\Desktop\CAB 201 A1\customers(1).csv", csv.ToString());
        }
    }
}
