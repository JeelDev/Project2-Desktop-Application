using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignment2Group10.Components.Pages.Data
{
    internal class Reservation
    {
        // Constant representing the size of a reservation record
        public const int RECORD_SIZE = 201;

        // Private fields for reservation details
        private string? code;
        private string? flightCode;
        private string? airline;
        private string? name;
        private string? citizenship;
        private double? cost;
        private string? active;
        private Flight? flight;

        // Default constructor
        public Reservation() { }

        // Parameterized constructor to initialize a reservation with specific details
        public Reservation(string code, string flightCode, string airline, double cost, string name, string citizenship, string active)
        {
            this.code = code;
            this.flightCode = flightCode;
            this.airline = airline;
            this.name = name;
            this.citizenship = citizenship;
            this.cost = cost;
            this.active = active;
        }

        // Copy constructor to create a new reservation from an existing one
        public Reservation(Reservation res)
        {
            this.code = res.code;
            this.flightCode = res.flightCode;
            this.airline = res.airline;
            this.name = res.name;
            this.citizenship = res.citizenship;
            this.cost = res.cost;
            this.active = res.active;
        }

        // Parameterized constructor to initialize a reservation with flight details
        public Reservation(string reservationCode, Flight flight, string name, string citizenship)
        {
            this.code = reservationCode;
            this.flight = flight;
            this.name = name;
            this.citizenship = citizenship;
        }

        // Public properties for accessing and modifying reservation details
        public string Code { get => code; set => code = value; }
        public string FlightCode { get => flightCode; set => flightCode = value; }
        public string Airline { get => airline; set => airline = value; }
        public string Name { get => name; set => name = value; }
        public string Citizenship { get => citizenship; set => citizenship = value; }
        public double Cost { get; set; }
        public string Active { get => active; set => active = value; }

        // Method to set the name, throwing an exception if the name is null or empty
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            this.name = name;
        }

        // Method to set the citizenship, throwing an exception if the citizenship is null or empty
        public void setCitizenship(string citizenship)
        {
            if (string.IsNullOrEmpty(citizenship))
            {
                throw new ArgumentException("Citizenship cannot be null or empty.");
            }
            this.citizenship = citizenship;
        }

        //Over riding the ToString method to stringify an object of FlightManager
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
