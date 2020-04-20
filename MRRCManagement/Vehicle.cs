using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRCManagement
{
    //all the methods in this section were done by Kirsten Moylan, n9948210
    //setting enum variables: Kevin Gunawan, n9812482
    //public enum VehicleClass
    //{
    //    Economy,
    //    Family,
    //    Luxury,
    //    Commercial
    //};

    //public enum FuelType
    //{
    //    Petrol,
    //    Diesel
    //};

    //public enum TransmissionType
    //{
    //    Manual,
    //    Automatic
    //};

    public class Vehicle
    {
        public string Registration { get; set; }
        public string Grade { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int NumSeats { get; set; }
        public string Transmission { get; set; }
        public string Fuel { get; set; }
        public bool GPS { get; set; }
        public bool SunRoof { get; set; }
        public float DailyRate { get; set; }
        public string Colour { get; set; }

        List<Vehicle> vehicleCollection = new List<Vehicle>();
        private string fleetFile = @"..\..\..\Data\fleet.csv";
        public List<Vehicle> GetFleet()
        {
            return vehicleCollection;
        }
        public Vehicle(string Registration, string Grade, string Make, string Model, int Year, int NumSeats, string Transmission, string Fuel, bool GPS, bool SunRoof, float DailyRate, string Colour)
        {

            this.Registration = Registration;
            this.Grade = Grade;
            this.Make = Make;
            this.Model = Model;
            this.Year = Year;
            this.NumSeats = NumSeats;
            this.Transmission = Transmission;
            this.Fuel = Fuel;
            this.GPS = GPS;
            this.SunRoof = SunRoof;
            this.DailyRate = DailyRate;
            this.Colour = Colour;

        }
        public string ToCSVString()
        {
            string csvString = "";
            csvString = csvString + Registration + "," + Grade + "," + Make + "," + Model + "," + Year.ToString() + "," + NumSeats.ToString() + "," + Transmission + "," + Fuel + "," + GPS.ToString() + "," + SunRoof.ToString() + "," + DailyRate.ToString() + "," + Colour + "";
            return csvString;

        }
        ////setting private variables: Kevin Gunawan, n9812482

        //private string vehicleRego;
        //private string make;
        //private string model;
        //private int year;
        //private VehicleClass vehicleClas;
        //private int numSeats;
        //private TransmissionType trans;
        //private FuelType fuelz;
        //private bool GPS;
        //private bool sunRoof;
        //private double dailyRate;
        //private string colour;

        ////setting the get;set properties for the private variables: Kevin Gunawan, n9812482

        //public string VehicleRego { get => vehicleRego; set => vehicleRego = value; }
        //public string Make { get => make; set => make = value; }
        //public string Model { get => model; set => model = value; }
        //public int Year { get => year; set => year = value; }
        //public VehicleClass VehicleClas { get => vehicleClas; set => vehicleClas = value; }
        //public int NumSeats { get => numSeats; set => numSeats = value; }
        //public TransmissionType Trans { get => trans; set => trans = value; }
        //public FuelType Fuelz { get => fuelz; set => fuelz = value; }
        //public bool GPS1 { get => GPS; set => GPS = value; }
        //public bool SunRoof { get => sunRoof; set => sunRoof = value; }
        //public double DailyRate { get => dailyRate; set => dailyRate = value; }
        //public string Colour { get => colour; set => colour = value; }

        ////1st constructor for vehicle. Values for all parametres of vehicle: Kevin Gunawan, n9812482
        //public Vehicle(string vehicleRego, VehicleClass vehicleClass, string make, string model, int year, int numSeats, TransmissionType transmissionType, FuelType fuelType, bool GPS, bool sunRoof, double dailyRate, string colour)
        //{
        //    VehicleRego = vehicleRego;
        //    VehicleClas = vehicleClass;
        //    Make = make;
        //    Model = model;
        //    Year = year;
        //    NumSeats = numSeats;
        //    Trans = transmissionType;
        //    Fuelz = fuelType;
        //    GPS1 = GPS;
        //    SunRoof = sunRoof;
        //    DailyRate = dailyRate;
        //    Colour = colour;

        //}

        ////Creating defaults for non-mandatory parameters. Kirsten Moylan, n9942810
        //public Vehicle(string vehicleRego, VehicleClass vehicleClass, string make, string model, int year)
        //{
        //    VehicleRego = vehicleRego;
        //    VehicleClas = vehicleClass;
        //    Make = make;
        //    Model = model;
        //    Year = year;
        //    NumSeats = 4;
        //    Trans = TransmissionType.Automatic;
        //    Fuelz = FuelType.Petrol;
        //    GPS1 = false;
        //    SunRoof = false;
        //    Colour = "Black";
        //    if (VehicleClas == VehicleClass.Economy)
        //    {
        //        Trans = TransmissionType.Automatic;
        //        DailyRate = 50.0;

        //    }
        //    else if (vehicleClass == VehicleClass.Family)
        //    {
        //        DailyRate = 80.0;

        //    }
        //    else if (vehicleClass == VehicleClass.Luxury)
        //    {
        //        GPS1 = true;
        //        SunRoof = true;
        //        DailyRate = 120.0;

        //    }
        //    else if (vehicleClass == VehicleClass.Commercial)
        //    {
        //        Fuelz = FuelType.Diesel;
        //        DailyRate = 130.0;

        //    }
        //}



        //// This method returns a string of the attributes of the vehicle. Kirsten Moylan, n9948210
        //public override string ToString()
        //{
        //    string TooString = "";
        //    TooString = TooString + vehicleRego + " " + VehicleClas + " " + make + " " + model + " " + year.ToString() + " " + numSeats.ToString() + " " + Trans + " " + Fuelz + " " + GPS.ToString() + " " + sunRoof.ToString() + " " + dailyRate.ToString() + " " + colour;
        //    return TooString;
        //    //return base.ToString();
        //}

        ////This method return a list of every unique value, of a vehicle. Kirsten Moylan, n9948210

        //public List<string> GetAttributeList()
        //{
        //    List<string> UniqueAttributes = new List<string>();
        //    UniqueAttributes.Add(vehicleRego);
        //    UniqueAttributes.Add(VehicleClas.ToString());
        //    UniqueAttributes.Add(make);
        //    UniqueAttributes.Add(model);
        //    UniqueAttributes.Add(year.ToString());
        //    UniqueAttributes.Add(numSeats.ToString() + "-Seater");
        //    UniqueAttributes.Add(Trans.ToString());
        //    UniqueAttributes.Add(Fuelz.ToString());
        //    if (GPS == true)
        //    {
        //        UniqueAttributes.Add("GPS");
        //    }
        //    else
        //    {
        //        UniqueAttributes.Add(" ");
        //    }
        //    if (sunRoof == true)
        //    {
        //        UniqueAttributes.Add("Sunroof");
        //    }
        //    else
        //    {
        //        UniqueAttributes.Add(" ");
        //    }
        //    UniqueAttributes.Add("$" + dailyRate.ToString());
        //    UniqueAttributes.Add(colour);
        //    return new List<string>(UniqueAttributes.Distinct());
        //}
    }
}
