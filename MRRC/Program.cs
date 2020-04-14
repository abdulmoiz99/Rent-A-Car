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
            CRM crm = new CRM();
            Console.WriteLine("\n### Mates-Rates Rent-a-Car Operation Menu ###\n");
            Console.WriteLine("You may press the ESC key at any menu to exit. Press the BACKSPACE key to return to the previous menu.");
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
                                    Console.WriteLine("\nInvalid Input!\n");
                                    Console.Write("Title*: ");
                                    title = Console.ReadLine();
                                }
                                Console.Write("First Name*: ");
                                firstName = Console.ReadLine();
                                while (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
                                {
                                    Console.WriteLine("\nInvalid Input!\n");
                                    Console.Write("First Name*: ");
                                    firstName = Console.ReadLine();
                                }
                                Console.Write("Last Name*: ");
                                lastName = Console.ReadLine();
                                while (!Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
                                {
                                    Console.WriteLine("\nInvalid Input!\n");
                                    Console.Write("Last Name*: ");
                                    lastName = Console.ReadLine();
                                }
                                Console.Write("Gender*: ");
                                gender = Console.ReadLine();
                                gender = gender.ToLower();
                                while (gender != "male" && gender != "female")
                                {
                                    Console.WriteLine("\nInvalid Input!\n");
                                    Console.Write("Gender*: ");
                                    gender = Console.ReadLine();
                                    gender = gender.ToLower();
                                }
                                Console.Write("DOB*: ");
                                DOB = Console.ReadLine();
                                while (!(DateTime.TryParseExact(DOB, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dateTime)))
                                {
                                    Console.WriteLine("\nIncorrect Format!\n");
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
                            string ID;
                            Console.WriteLine("\nPlease enter an ID to modify record:\n");
                            Console.Write("ID: ");
                            ID = Console.ReadLine();
                            while (!Regex.IsMatch(ID, @"^\d+$"))
                            {
                                Console.WriteLine("\nInvalid Input!\n");
                                Console.Write("ID: ");
                                ID = Console.ReadLine();
                            }
                            if (crm.FindCustomer(Convert.ToInt32(ID)))
                            {
                                while (true)
                                {
                                    ConsoleKeyInfo modifyCustomerInput;
                                    Console.WriteLine("\nPlease enter a character from the options below to modify trait:\n");
                                    Console.WriteLine("a) Title");
                                    Console.WriteLine("b) First Name");
                                    Console.WriteLine("c) Last Name");
                                    Console.WriteLine("d) Gender");
                                    Console.WriteLine("e) DOB");
                                    Console.WriteLine();
                                    Console.Write(">");
                                    modifyCustomerInput = Console.ReadKey();
                                    Console.WriteLine();
                                    if (modifyCustomerInput.Key == ConsoleKey.A)
                                    {
                                        string title;
                                        Console.Write("\nTitle: ");
                                        title = Console.ReadLine();
                                        while (!Regex.IsMatch(title, @"^[a-zA-Z]+$"))
                                        {
                                            Console.WriteLine("\nInvalid Input!\n");
                                            Console.Write("Title: ");
                                            title = Console.ReadLine();
                                        }
                                        crm.ModifyCustomer(Convert.ToInt32(ID), 1, title);
                                        crm.SaveToFile();
                                        Console.WriteLine("\nID " + ID + " is successfully modified.");
                                        break;
                                    }
                                    else if (modifyCustomerInput.Key == ConsoleKey.B)
                                    {
                                        string firstName;
                                        Console.Write("\nFirst Name: ");
                                        firstName = Console.ReadLine();
                                        while (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
                                        {
                                            Console.WriteLine("\nInvalid Input!\n");
                                            Console.Write("First Name: ");
                                            firstName = Console.ReadLine();
                                        }
                                        crm.ModifyCustomer(Convert.ToInt32(ID), 2, firstName);
                                        crm.SaveToFile();
                                        Console.WriteLine("\nID " + ID + " is successfully modified.");
                                        break;
                                    }
                                    else if (modifyCustomerInput.Key == ConsoleKey.C)
                                    {
                                        string lastName;
                                        Console.Write("\nLast Name: ");
                                        lastName = Console.ReadLine();
                                        while (!Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
                                        {
                                            Console.WriteLine("\nInvalid Input!\n");
                                            Console.Write("Last Name: ");
                                            lastName = Console.ReadLine();
                                        }
                                        crm.ModifyCustomer(Convert.ToInt32(ID), 3, lastName);
                                        crm.SaveToFile();
                                        Console.WriteLine("\nID " + ID + " is successfully modified.");
                                        break;
                                    }
                                    else if (modifyCustomerInput.Key == ConsoleKey.D)
                                    {
                                        string gender;
                                        Console.Write("\nGender: ");
                                        gender = Console.ReadLine();
                                        gender = gender.ToLower();
                                        while (gender != "male" && gender != "female")
                                        {
                                            Console.WriteLine("\nInvalid Input!\n");
                                            Console.Write("Gender: ");
                                            gender = Console.ReadLine();
                                            gender = gender.ToLower();
                                        }
                                        crm.ModifyCustomer(Convert.ToInt32(ID), 4, gender);
                                        crm.SaveToFile();
                                        Console.WriteLine("\nID " + ID + " is successfully modified.");
                                        break;
                                    }
                                    else if (modifyCustomerInput.Key == ConsoleKey.E)
                                    {
                                        string DOB;
                                        string[] format = new string[] { "dd/MM/yyyy" };
                                        DateTime dateTime;
                                        Console.Write("\nDOB: ");
                                        DOB = Console.ReadLine();
                                        while (!(DateTime.TryParseExact(DOB, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dateTime)))
                                        {
                                            Console.WriteLine("\nIncorrect Format!\n");
                                            Console.Write("DOB: ");
                                            DOB = Console.ReadLine();
                                        }
                                        crm.ModifyCustomer(Convert.ToInt32(ID), 5, DOB);
                                        crm.SaveToFile();
                                        Console.WriteLine("\nID " + ID + " is successfully modified.");
                                        break;
                                    }
                                    else if (modifyCustomerInput.Key == ConsoleKey.Escape)
                                    {
                                        Environment.Exit(0);
                                    }
                                    else if (modifyCustomerInput.Key == ConsoleKey.Backspace)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nInvalid Input!");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nID " + ID + " is not found.");
                            }
                        }
                        else if (customerInput.Key == ConsoleKey.D)
                        {
                            string ID;
                            Console.WriteLine("\nPlease enter an ID to delete record:\n");
                            Console.Write("ID: ");
                            ID = Console.ReadLine();
                            while (!Regex.IsMatch(ID, @"^\d+$"))
                            {
                                Console.WriteLine("\nInvalid Input!\n");
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
                            Console.WriteLine("\nInvalid Input!");
                        }
                    }
                }
                else if (mainInput.Key == ConsoleKey.B)
                {
                    while (true)
                    {
                        Console.WriteLine("\nPlease enter a number from the option below:\n");
                        Console.WriteLine("a) Display Fleet");
                        Console.WriteLine("b) New Vehicle");
                        Console.WriteLine("c) Modify Vehicle");
                        Console.WriteLine("d) Delete  Vehicle\n");
                        Console.Write(">");
                        ConsoleKeyInfo fleetInput;
                        fleetInput = Console.ReadKey();
                        if (fleetInput.Key == ConsoleKey.A)
                        {
                            Console.WriteLine();
                            List<Fleet> fleets = crm.GetFleets();
                            var fleetTable = new DataTable();
                            DataRow fleetRow = null;
                            fleetTable.TableName = "Fleet";
                            fleetTable.Columns.Add("Registration", typeof(string));
                            fleetTable.Columns.Add("Grade", typeof(string));
                            fleetTable.Columns.Add("Make", typeof(string));
                            fleetTable.Columns.Add("Model", typeof(string));
                            fleetTable.Columns.Add("Year", typeof(int)).AllowDBNull = false;
                            fleetTable.Columns.Add("NumSeats", typeof(int)).AllowDBNull = false;
                            fleetTable.Columns.Add("Transmission", typeof(string));
                            fleetTable.Columns.Add("Fuel", typeof(string));
                            fleetTable.Columns.Add("GPS", typeof(string));
                            fleetTable.Columns.Add("SunRoof", typeof(string));
                            fleetTable.Columns.Add("DailyRate", typeof(string));
                            fleetTable.Columns.Add("Colour", typeof(string));
                            
                            for (int i = 0; i < fleets.Count; i++)
                            {
                                fleetRow = fleetTable.NewRow(); // have new row on each iteration
                                fleetRow["Registration"] = fleets[i].Registration;
                                fleetRow["Grade"] = fleets[i].Grade;
                                fleetRow["Make"] = fleets[i].Make;
                                fleetRow["Model"] = fleets[i].Model;
                                fleetRow["Year"] = fleets[i].Year;
                                fleetRow["NumSeats"] = fleets[i].NumSeats;
                                fleetRow["Transmission"] = fleets[i].Transmission;
                                fleetRow["Fuel"] = fleets[i].Fuel;
                                fleetRow["GPS"] = fleets[i].GPS;
                                fleetRow["SunRoof"] = fleets[i].SunRoof;
                                fleetRow["DailyRate"] = fleets[i].DailyRate;
                                fleetRow["Colour"] = fleets[i].Colour;


                                fleetTable.Rows.Add(fleetRow);
                            }
                            fleetTable.Print();
                        }
                        else if (fleetInput.Key == ConsoleKey.B)
                        {
                          
                        }
                        else if (fleetInput.Key == ConsoleKey.C)
                        {

                        }
                        else if (fleetInput.Key == ConsoleKey.D)
                        {

                        }
                        else if (fleetInput.Key == ConsoleKey.Backspace)
                        {

                        }
                        else if (fleetInput.Key == ConsoleKey.Escape)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid Input!");
                        }

                    }
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
                    Console.WriteLine("\nInvalid Input!");
                }
            }
        }
    }
}
