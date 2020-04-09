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

                        }
                        else if (customerInput.Key == ConsoleKey.B) 
                        {

                        }
                        else if (customerInput.Key == ConsoleKey.C)
                        {

                        }
                        else if (customerInput.Key == ConsoleKey.D)
                        {

                        }
                        else if (customerInput.Key == ConsoleKey.Backspace)
                        {
                            break;
                        }
                        else if (customerInput.Key == ConsoleKey.Escape)
                        {
                            Environment.Exit(0);
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
