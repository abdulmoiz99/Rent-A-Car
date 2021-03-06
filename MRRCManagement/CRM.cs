﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MRRCManagement
{
    public class CRM
    {

        //all needed variables defined 
        List<Customer> customersCollection = new List<Customer>();
      
        private string crmFile = @"..\..\..\Data\customer.csv";
        

        //CRM constructor that checks whether there are any customer.csv file. If there are none, a new empty csv file is created. Otherwise, data
        //from file is loaded into the list.

        public CRM()
        {
            if (!File.Exists(crmFile))
            {
                File.Create(crmFile);
            }
            else
            {
                LoadFromFile();
               
            }
        }

        //Automatically Generates ID
        public int GenerateID()
        {
            string[] clients = File.ReadAllLines(crmFile);
            string[] subset = clients[clients.Length - 1].Split(',');
            int id = int.Parse(subset[0]);
            return id + 1;
        }

        //reading from csv file and putting the customer objects into a list customersCollection

        public void LoadFromFile()
        {
            customersCollection.Clear();
            string[] clients = File.ReadAllLines(crmFile);
            for (int i = 1; i < clients.Length; i++)
            {
                string[] subset = clients[i].Split(',');
                int id = int.Parse(subset[0]);
                string heading = subset[1];
                string first = subset[2];
                string last = subset[3];
                Gender sex;
                if (subset[4] == "Male")
                {
                    sex = Gender.Male;
                }
                else
                {
                    sex = Gender.Female;
                }
                string dob = subset[5];
                customersCollection.Add(new Customer(id, heading, first, last, sex, dob));
            }
        }
       
        //returning the list of current customersCollection

        public List<Customer> GetCustomers()
        {
            return customersCollection;
        }
       
        //wrting the customer objects in the customersCollection list into the customer.csv file

        public void SaveToFile()
        {
            var csv = new StringBuilder();
            var header = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"", "CustomerID", "Title", "FirstName", "LastName", "Gender", "DOB");
            csv.AppendLine(header);
            foreach (var item in customersCollection)
            {
                var newline = item.ToCSVString();
                csv.AppendLine(newline);
            }
            File.WriteAllText(crmFile, csv.ToString());
        }
        //adds the provided customer into the customersCollection list if the customerID doesn't exist in the CSV. Returns
        //true upon successful addition and false otherwise.


        public bool AddCustomer(Customer customer)
        {
            bool presence;
            int counter = 0;
            foreach (var item in customersCollection)
            {
                if (item.CustomerID == customer.CustomerID)
                {
                    counter++;
                }
            }

            if (counter > 0)
            {
                presence = false;
            }
            else
            {
                presence = true;
                customersCollection.Add(customer);
            }
            return presence;
        }

        // This method removes the customer from the CRM if they are not currently renting a vehicele.
        // However, for this remove we need all of the customers information in order to delete them.

        public bool RemoveCustomer(Customer customer, Fleet fleet)
        {
            if (fleet.IsRenting(customer.CustomerID))
            {
                return false;
            }
            else
            {
                foreach (var item in customersCollection.ToList())
                {
                    if (item.CustomerID == customer.CustomerID)
                    {
                        customersCollection.Remove(item);
                    }
                }
                return true;


            }
        }
        //This method removes the customer from the CRM if they are not currently renting a
        //vehicle.It returns true if the removal was successful, otherwise it returns false. Remove based on customer ID.

        public bool RemoveCustomer(int customerID, Fleet fleet)
        {

            if (fleet.IsRenting(customerID))
            {

                return false;
            }
            else
            {
                foreach (var item in customersCollection.ToList())
                {
                    if (item.CustomerID == customerID)
                    {
                        customersCollection.Remove(item);
                    }
                }
                return true;
            }
        }

        public bool FindCustomer(int customerID)
        {
            foreach (var item in customersCollection.ToList())
            {
                if (item.CustomerID == customerID)
                {
                    return true;
                }
            }
            return false;
        }

        public void ModifyCustomer(int customerID, int columnIndex, string value)
        {
            int index;
            Gender sex;
            string[] clients = File.ReadAllLines(crmFile);
            clients = clients.Skip(1).ToArray();
            for (index = 0; index < customersCollection.Count; index++)
            {
                if (customersCollection[index].CustomerID == customerID)
                {
                    break;
                }
            }
            string[] subset = clients[index].Split(',');
            if (subset[4] == "Male")
            {
                sex = Gender.Male;
            }
            else
            {
                sex = Gender.Female;
            }
            if (columnIndex == 4)
            {
                if (value == "male") sex = Gender.Male;
                else sex = Gender.Female;
            }
            else
            {
                subset[columnIndex] = value;
            }
            customersCollection[index] = new Customer(customerID, subset[1], subset[2], subset[3], sex, subset[5]);
        }
    }
}
