using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace MRRCManagement
{
    public class Fleet 
    {
        private string rentalFile = @"..\..\..\Data\rentals.csv";
        private string fleetFile = @"..\..\..\Data\fleet.csv";
        Dictionary<string, int> rentals = new Dictionary<string, int>();
        List<Vehicle> vehicleCollection = new List<Vehicle>();

        public Fleet()
        {
            if (!File.Exists(fleetFile))
                {
                    File.Create(fleetFile);
                }
                else
                {
                    LoadFromFile();

                }
        }
        public List<Vehicle> GetFleets()
        {
            return vehicleCollection;

        }
        public Dictionary<string, int> GetRentals()
        {
            return rentals;

        }
        public void LoadFromFile()
        {
            vehicleCollection.Clear();
            string[] clients = File.ReadAllLines(fleetFile);
            string[] booking = File.ReadAllLines(rentalFile);
            for (int i = 1; i < clients.Length; i++)
            {
                string[] subset = clients[i].Split(',');
                string Registration = subset[0];
                string Grade = subset[1];
                string Make = subset[2];
                string Model = subset[3];
                int Year = int.Parse(subset[4]);
                int NumSeats = int.Parse(subset[5]);
                string Transmission = subset[6];
                string Fuel = subset[7];
                bool GPS = bool.Parse(subset[8]);
                bool SunRoof = bool.Parse(subset[9]);
                float DailyRate = float.Parse(subset[10]);
                string Colour = subset[11];

                vehicleCollection.Add(new Vehicle(Registration, Grade, Make, Model, Year, NumSeats, Transmission, Fuel, GPS, SunRoof, DailyRate, Colour));
            }
            for (int i = 1; i < booking.Length; i++)
            {
                string[] parts = booking[i].Split(',');
                int value = int.Parse(parts[1]);
                rentals.Add(parts[0], value);
            }
        }
        public void SaveToFile()
        {
            if (!File.Exists(fleetFile))
            {
                File.Create(fleetFile);
                File.Create(rentalFile);
            }
            var motors = new StringBuilder();
            var header = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\"", "Registration","Grade", "Make", "Model", "Year", "NumSeats", "Transmission", "Fuel", "GPS", "SunRoof", "DailyRate","Colour");

            motors.AppendLine(header);
            foreach (var item in vehicleCollection)
            {
                var newline = item.ToCSVString();
                motors.AppendLine(newline);
            }
            File.WriteAllText(fleetFile, motors.ToString());
            //Rentals
            var rents = new StringBuilder();
            var head = string.Format("\"{0}\",\"{1}\"", "Vehicle", "Customer");
            rents.AppendLine(head);
            foreach (var item in rentals)
            {
                rents.AppendLine(string.Format("{0},{1}", item.Key, item.Value));
            }
            File.WriteAllText(rentalFile, rents.ToString());
        }
        public bool CheckRegistration(string Registration)
        {
            foreach (var item in vehicleCollection.ToList())
            {
                if (item.Registration == Registration)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ModifyRecord(string Registration,string Grade, string Make, string Model, int Year, int  NumSeats, string Transmission, string Fuel,bool GPS, bool SunRoof, float DailyRate,string Colour)
        {
            foreach (var item in vehicleCollection.ToList())
            {
                if (item.Registration == Registration)
                {
                    item.Registration = Registration;
                    item.Grade = Grade;
                    item.Make = Make;
                    item.Model = Model;
                    item.Year = Year;
                    item.NumSeats = NumSeats;
                    item.Transmission = Transmission;
                    item.Fuel = Fuel;
                    item.GPS = GPS;
                    item.SunRoof = SunRoof;
                    item.DailyRate = DailyRate;
                    item.Colour = Colour;
                }
            }
            return false;
        }
     
        public bool AddVehicle(Vehicle vehicle)
        {
            int counter = 0;
            foreach (var item in vehicleCollection)
            {
                if (item.Registration == vehicle.Registration)
                {
                    counter++;
                }
            }

            if (counter > 0)
            {
                return false;
            }
            else
            {
                vehicleCollection.Add(vehicle);
                return true;
            }
        }

        public bool RemoveVehicle(string Registration)
        {
            if (rentals.ContainsKey(Registration))
            {
                return false;
            }
            else
            {
                foreach (var item in vehicleCollection.ToList())
                {
                    if (item.Registration == Registration)
                    {
                        vehicleCollection.Remove(item);
                    }
                }
                return true;
            }
        }

        public bool IsRenting(int customerID)
        {
            if (rentals.ContainsValue(customerID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsRented(string vehicleRego)
        {
            if (rentals.ContainsKey(vehicleRego))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int RentedBy(string vehicleRego)
        {
            int id;
            if (rentals.ContainsKey(vehicleRego))
            {
                id = rentals[vehicleRego];
            }
            else
            {
                id = -1;
            }
            return id;

        }

        public bool RentCar(string vehicleRego, int customerID)
        {
            int counter = 0;
            foreach (var item in vehicleCollection)
            {
                if (item.Registration == vehicleRego)
                {
                    counter++;
                }
            }
            if (rentals.ContainsKey(vehicleRego))
            {
                return false;
            }
            else
            {
                if (counter > 0)
                {
                    rentals.Add(vehicleRego, customerID);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int ReturnCar(string vehicleRego)
        {
            int ID;
            if (!rentals.ContainsKey(vehicleRego))
            {
                return -1;
            }
            else
            {
                ID = rentals[vehicleRego];
                rentals.Remove(vehicleRego);
                return ID;
            }
        }
    }
}
