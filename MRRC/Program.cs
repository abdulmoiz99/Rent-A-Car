using MRRCManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MRRC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n### Mates-Rates Rent-a-Car Operation Menu ###\n");
            Console.WriteLine("You may press the ESC key at any menu to exit. Press the BACKSPACE key to return to the previous menu");
            while (true) 
            {
                ConsoleKeyInfo mainInput;
                Console.WriteLine("\nPlease enter a character from the options below:\n");
                Console.WriteLine("a) Customer Management");
                Console.WriteLine("b) Fleet Management");
                Console.WriteLine("c) Rental Management");
                Console.WriteLine();
                Console.Write(">");
                mainInput = Console.ReadKey();
                Console.WriteLine();
                if (mainInput.Key == ConsoleKey.A)
                {
                    while (true) 
                    {
                        CRM crm = new CRM();
                        ConsoleKeyInfo customerInput;
                        Console.WriteLine("\nPlease enter a character from the options below:\n");
                        Console.WriteLine("a) Display Customers");
                        Console.WriteLine("b) New Customer");
                        Console.WriteLine("c) Modify Customer");
                        Console.WriteLine("d) Delete Customer");
                        Console.WriteLine();
                        Console.Write(">");
                        customerInput = Console.ReadKey();
                        Console.WriteLine();
                        if (customerInput.Key == ConsoleKey.A)
                        {
                            Console.WriteLine();
                            List<Customer> customers = crm.GetCustomers();
                            DataTable customersTable = new DataTable();
                            DataRow customersRow = null;
                            customersTable.TableName = "Customers";
                            customersTable.Columns.Add("ID", typeof(int)).AllowDBNull = false;
                            customersTable.Columns.Add("Title", typeof(string));
                            customersTable.Columns.Add("First Name", typeof(string));
                            customersTable.Columns.Add("Last Name", typeof(string));
                            customersTable.Columns.Add("Gender", typeof(string));
                            customersTable.Columns.Add("DOB", typeof(string));
                            for (int i = 0; i < customers.Count; i++)
                            {
                                customersRow = customersTable.NewRow(); // have new row on each iteration
                                customersRow["ID"] = customers[i].CustomerID;
                                customersRow["Title"] = customers[i].Title;
                                customersRow["First Name"] = customers[i].FirstNames;
                                customersRow["Last Name"] = customers[i].LastNames;
                                if (customers[i].Gen == Gender.Male)
                                {
                                    customersRow["Gender"] = "Male";
                                }
                                else 
                                {
                                    customersRow["Gender"] = "Female";
                                }
                                customersRow["DOB"] = customers[i].DateOfBirth;
                                customersTable.Rows.Add(customersRow);
                            }
                            customersTable.Print();
                        }
                        else if (customerInput.Key == ConsoleKey.B) 
                        {
                            while (true) 
                            {
                                Customer customer;
                                int ID;
                                string title, firstName, lastName, gender, DOB;
                                string[] format = new string[] { "dd/MM/yyyy" };
                                DateTime dateTime;
                                Console.WriteLine("\nPlease fill the following fields (fields marked with * are required):\n");
                                Console.Write("Title*: ");
                                title = Console.ReadLine();
                                while (!Regex.IsMatch(title, @"^[a-zA-Z]+$")) 
                                {
                                    Console.WriteLine("\nInvalid Input\n");
                                    Console.Write("Title*: ");
                                    title = Console.ReadLine();
                                }
                                Console.Write("First Name*: ");
                                firstName = Console.ReadLine();
                                while (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
                                {
                                    Console.WriteLine("\nInvalid Input\n");
                                    Console.Write("First Name*: ");
                                    firstName = Console.ReadLine();
                                }
                                Console.Write("Last Name*: ");
                                lastName = Console.ReadLine();
                                while (!Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
                                {
                                    Console.WriteLine("\nInvalid Input\n");
                                    Console.Write("Last Name*: ");
                                    lastName = Console.ReadLine();
                                }
                                Console.Write("Gender*: ");
                                gender = Console.ReadLine();
                                gender = gender.ToLower();
                                while (gender != "male" && gender != "female")
                                {
                                    Console.WriteLine("\nInvalid Input\n");
                                    Console.Write("Gender*: ");
                                    gender = Console.ReadLine();
                                    gender = gender.ToLower();
                                }
                                Console.Write("DOB*: ");
                                DOB = Console.ReadLine();
                                while (!(DateTime.TryParseExact(DOB, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dateTime)))
                                {
                                    Console.WriteLine("\nInvalid Format\n");
                                    Console.Write("DOB*: ");
                                    DOB = Console.ReadLine();
                                }
                                Console.WriteLine();
                                ID = crm.GenerateID();
                                if (gender == "male")
                                {
                                    customer = new Customer(ID, title, firstName, lastName, Gender.Male, DOB);
                                }
                                else 
                                {
                                    customer = new Customer(ID, title, firstName, lastName, Gender.Female, DOB);
                                }
                                crm.AddCustomer(customer);
                                crm.SaveToFile();
                                Console.WriteLine("Successfully created new customer '" + ID + " - " + title + " " + firstName + " " + lastName + "' and added to customer list.");
                                break;
                            }
                        }
                        else if (customerInput.Key == ConsoleKey.C)
                        {

                        }
                        else if (customerInput.Key == ConsoleKey.D)
                        {
                            string ID;
                            Console.WriteLine("\nPlease enter an ID to delete record:\n");
                            Console.Write("ID: ");
                            ID = Console.ReadLine();
                            while (!Regex.IsMatch(ID, @"^\d+$"))
                            {
                                Console.WriteLine("\nInvalid Input\n");
                                Console.Write("ID: ");
                                ID = Console.ReadLine();
                            }
                            crm.RemoveCustomer(Convert.ToInt32(ID));
                            crm.SaveToFile();
                        }
                        else if (customerInput.Key == ConsoleKey.Backspace)
                        {
                            break;
                        }
                        else if (customerInput.Key == ConsoleKey.Escape)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid Input");
                        }
                    }
                }
                else if (mainInput.Key == ConsoleKey.B)
                {

                }
                else if (mainInput.Key == ConsoleKey.C)
                {

                }
                else if (mainInput.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
                else if (mainInput.Key == ConsoleKey.Backspace)
                {
                    //Do nothing
                }
                else 
                {
                    Console.WriteLine("\nInvalid Input");
                }
            }
        }
    }
}
