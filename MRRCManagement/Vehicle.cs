using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRCManagement
{
    
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
    }
}
