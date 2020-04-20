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


        //Kirsten Moylan, n9948210
        //All needed variables defined.

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
        public void LoadFromFile()
        {
            vehicleCollection.Clear();
            string[] clients = File.ReadAllLines(fleetFile);
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
        }
        //constructor for fleet
        //Kevin Gunawan, n9812482

        //load the state of fleet file into a list called vehicleCollection - defined earlier.
        // Kevin Gunawan, n9812482
        //public void LoadFromFile()
        //{
        //    vehicleCollection.Clear();
        //    rentals.Clear();
        //    string[] cars = File.ReadAllLines(fleetFile);
        //    string[] booking = File.ReadAllLines(rentalFile);
        //    for (int i = 1; i < cars.Length; i++)
        //    {
        //        string[] section = cars[i].Split(',');
        //        string reg = section[0];
        //        string make = section[1];
        //        string model = section[2];
        //        int year = int.Parse(section[3]);
        //        VehicleClass classes;
        //        if (section[4] == "Economy")
        //        {
        //            classes = VehicleClass.Economy;
        //        }
        //        else if (section[4] == "Luxury")
        //        {
        //            classes = VehicleClass.Luxury;
        //        }
        //        else if (section[4] == "Family")
        //        {
        //            classes = VehicleClass.Family;
        //        }
        //        else
        //        {
        //            classes = VehicleClass.Commercial;
        //        }
        //        int seats = int.Parse(section[5]);
        //        TransmissionType gears;
        //        FuelType engine;
        //        if (section[6] == "Manual")
        //        {
        //            gears = TransmissionType.Manual;
        //        }
        //        else
        //        {
        //            gears = TransmissionType.Automatic;
        //        }
        //        if (section[7] == "Petrol")
        //        {
        //            engine = FuelType.Petrol;
        //        }
        //        else
        //        {
        //            engine = FuelType.Diesel;
        //        }
        //        bool gps = bool.Parse(section[8]);
        //        bool roof = bool.Parse(section[9]);
        //        double rate = double.Parse(section[11]);
        //        string color = section[10];
        //        vehicleCollection.Add(new Vehicle(reg, classes, make, model, year, seats, gears, engine, gps, roof, rate, color));
        //    }

        //    for (int i = 1; i < booking.Length; i++)
        //    {
        //        string[] parts = booking[i].Split(',');
        //        int value = int.Parse(parts[1]);
        //        rentals.Add(parts[0], value);
        //    }
        //}
        //This method returns the current state of the fleet
        //Kevin Gunawan, n9812482

        //This method saves list and dict current state into there respective files.
        // Kevin Gunawan, n9812482
        public void SaveToFile()
        {
            //This method saves all information in the customer collection to fleet file
            if (!File.Exists(fleetFile))
            {
                File.Create(fleetFile);
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

        }
        // this method returns a CSV of vehicles in the data file. Kirsten Moylan, n9948210

        //This method adds a whole vehicle to the Vehicle collection List, 
        //Kirsten Moylan , n9948210
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

        //This method removes a vehicle (based on the whole vehicle) from the vehicle collection list if the vehicle exists.
        //Kevin Gunawan, n9812482
        //public bool RemoveVehicle(Vehicle vehicle)
        //{
        //    if (rentals.ContainsKey(vehicle.VehicleRego))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        foreach (var item in vehicleCollection.ToList())
        //        {
        //            if (item.VehicleRego == vehicle.VehicleRego)
        //            {
        //                vehicleCollection.Remove(item);
        //            }
        //        }
        //        return true;
        //    }
        //}

        //This method  similar to the one above removes a vehicle based on the user entering the vehicle rego.
        //Kevin Gunawan, n9812482
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
        //This method Gets the fleet of either rented or non rented vehicles depending if the user enters in true or false.
        //Kevin Gunawan, n9812482
        //public List<Vehicle> GetFleet(bool rented)
        //{
        //    List<Vehicle> result = new List<Vehicle>();
        //    if (rented)
        //    {
        //        foreach (var item in vehicleCollection)
        //        {
        //            if (rentals.ContainsKey(item.VehicleRego))
        //            {
        //                result.Add(item);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (var item in vehicleCollection)
        //        {
        //            if (!rentals.ContainsKey(item.VehicleRego))
        //            {
        //                result.Add(item);
        //            }
        //        }

        //    }
        //    return result;
        //}

        //public bool IsRented(string vehicleRego)
        //{
        //    if (rentals.ContainsKey(vehicleRego))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        //public bool IsRenting(int customerID)
        //{
        //    if (rentals.ContainsValue(customerID))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public int RentedBy(string vehicleRego)
        //{
        //    int id;
        //    if (rentals.ContainsKey(vehicleRego))
        //    {
        //        id = rentals[vehicleRego];
        //    }
        //    else
        //    {
        //        id = -1;
        //    }
        //    return id;

        //}
        //Kirsten Moylan, n9948210
        //public bool RentCar(string vehicleRego, int customerID)
        //{
        //    int counter = 0;
        //    foreach (var item in vehicleCollection)
        //    {
        //        if (item.VehicleRego == vehicleRego)
        //        {
        //            counter++;
        //        }
        //    }
        //    if (rentals.ContainsKey(vehicleRego))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (counter > 0)
        //        {
        //            rentals.Add(vehicleRego, customerID);
        //            return true;


        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //}

        //public int ReturnCar(string vehicleRego)
        //{
        //    int ID;
        //    if (!rentals.ContainsKey(vehicleRego))
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        ID = rentals[vehicleRego];
        //        rentals.Remove(vehicleRego);
        //        return ID;
        //    }

        //}


        //This is the first query where it searches for anyQ1 Any:
        //public List<Vehicle> Any()
        //{
        //    List<Vehicle> result = new List<Vehicle>();
        //    foreach (var item in vehicleCollection)
        //    {
        //        if (!IsRented(item.VehicleRego))
        //        {
        //            result.Add(item);
        //        }
        //    }
        //    return result;
        //}

        //Q2 Any with given cost range:
        //public List<Vehicle> CostRange(int min, int max)
        //{
        //    List<Vehicle> search = new List<Vehicle>();
        //    foreach (var item in vehicleCollection)
        //    {
        //        if (!IsRented(item.VehicleRego))
        //        {
        //            if (item.DailyRate >= min && item.DailyRate <= max)
        //            {
        //                search.Add(item);
        //            }
        //        }

        //    }
        //    return search;
        //}

        //Q3 Single attribute query:
        //public List<Vehicle> SingleQuery(string query)
        //{
        //    List<Vehicle> result = new List<Vehicle>();
        //    foreach (var item in vehicleCollection)
        //    {
        //        List<string> convertString = new List<string>();
        //        convertString = item.GetAttributeList();
        //        if (convertString.Contains(query, StringComparer.OrdinalIgnoreCase))
        //        {
        //            if (!IsRented(item.VehicleRego))
        //            {
        //                result.Add(item);
        //            }
        //        }
        //    }
        //    return result;
        //}

        ////Q4 Choice between any number  of attributes query (OR): Apparently it is space-sensitive
        //public List<Vehicle> OrQuery(string query)
        //{
        //    string[] separator = new string[] { " OR ", "OR", "Or", "oR", " Or ", " oR " };
        //    string[] subset = query.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        //    List<Vehicle> result = new List<Vehicle>();
        //    List<Vehicle> noDupes = new List<Vehicle>();
        //    foreach (var item in subset)
        //    {
        //        result.AddRange(SingleQuery(item));
        //    }
        //    noDupes = result.Distinct().ToList();

        //    return noDupes;
        //}

        //Q5 A combination of attributes:
        //public List<Vehicle> AndQuery(string query)
        //{
        //    string[] separator = new string[] { " AND ", "AND", "And", " And ", " aNd ", "aNd", " anD ", "anD" };
        //    string[] subset = query.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        //    List<Vehicle> test = new List<Vehicle>();
        //    List<Vehicle> dupes = new List<Vehicle>();
        //    foreach (var item in subset)
        //    {
        //        test.AddRange(SingleQuery(item));
        //    }
        //    dupes = test.GroupBy(x => x)
        //                .Where(g => g.Count() > 1 && g.Count() == subset.Length)
        //                .Select(g => g.Key)
        //                .ToList();
        //    return dupes;
        //    //return test;
        //}

        ////Q8 Combo of AND and OR
        //public List<Vehicle> AndOrQuery(string query)
        //{
        //    List<Vehicle> result = new List<Vehicle>();
        //    string pattern = @"([A-Za-z0-9-])*\S";
        //    query = query.Trim();
        //    //result.AddRange(SingleQuery(test.Value));
        //    while (query != "")
        //    {
        //        Match test = Regex.Match(query, pattern);
        //        if (test.Value == "OR" || test.Value == "Or" || test.Value == "oR" || test.Value == "or")
        //        {
        //            int place = query.IndexOf(test.Value);
        //            query = query.Remove(place, test.Value.Length).Insert(place, "");
        //            //query=query.Replace(test.Value, "");
        //            test = test.NextMatch();
        //            result.AddRange(SingleQuery(test.Value));
        //            result = result.Distinct().ToList();
        //        }
        //        else if (test.Value == "AND" || test.Value == "And" || test.Value == "anD" || test.Value == "and")
        //        {
        //            int position = query.IndexOf(test.Value);
        //            query = query.Remove(position, test.Value.Length).Insert(position, "");
        //            //query = query.Replace(test.Value, "");
        //            test = test.NextMatch();
        //            result.AddRange(SingleQuery(test.Value));
        //            result = result.GroupBy(x => x)
        //                  .Where(g => g.Count() > 1)
        //                  .Select(g => g.Key).ToList();
        //        }
        //        else
        //        {
        //            result.AddRange(SingleQuery(test.Value));
        //        }
        //        int index = query.IndexOf(test.Value);
        //        query = query.Remove(index, test.Value.Length).Insert(index, "");
        //        //query=query.Replace(test.Value, "");
        //        query = query.Trim();
        //        //query = query.Replace(" ", "");
        //    }
        //    return result;
        //}
    }
}
