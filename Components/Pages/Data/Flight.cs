using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Assignment2Group10.Components.Pages.Data
{
    internal class Flight
    {
        // Constant for the record size
        public const int RECORD_SIZE = 72;

        // Fields representing the properties of a flight
        private string code;
        private string airline;
        private string from;
        private string to;
        private string weekday;
        private string time;
        private int seats;
        private double costPerSeat;
        private bool isSelected;

        // Constructor to initialize a flight with only a code
        public Flight(string code)
        {
            this.code = code;
        }

        // Constructor to initialize a flight with all details
        public Flight(string code, string airline, string from, string to, string weekday, string time, int seats, double costPerSeat)
        {
            this.code = code;
            this.airline = airline;
            this.from = from;
            this.to = to;
            this.weekday = weekday;
            this.time = time;
            this.seats = seats;
            this.costPerSeat = costPerSeat;
        }

        // Properties for accessing and modifying flight details
        public string Code { get => code; set => code = value; }
        public string Airline { get => airline; set => airline = value; }
        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string Weekday { get => weekday; set => weekday = value; }
        public string Time { get => time; set => time = value; }
        public int Seats { get => seats; set => seats = value; }
        public double CostPerSeat { get => costPerSeat; set => costPerSeat = value; }

        // Override Equals method to compare two flight objects based on all properties
        public override bool Equals(object obj)
        {
            return obj is Flight flight &&
                   code == flight.code &&
                   airline == flight.airline &&
                   from == flight.from &&
                   to == flight.to &&
                   weekday == flight.weekday &&
                   time == flight.time &&
                   seats == flight.seats &&
                   costPerSeat == flight.costPerSeat;
        }

        // Override ToString method to stringify the flight object
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
