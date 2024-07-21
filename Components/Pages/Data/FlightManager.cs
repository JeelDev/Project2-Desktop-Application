using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignment2Group10.Components.Pages.Data
{
    internal class FlightManager
    {
        // Constants for weekdays
        public static string WEEKDAY_ANY = "Any";
        public static string WEEKDAY_SUNDAY = "Sunday";
        public static string WEEKDAY_MONDAY = "Monday";
        public static string WEEKDAY_TUESDAY = "Tuesday";
        public static string WEEKDAY_WEDNESDAY = "Wednesday";
        public static string WEEKDAY_THURSDAY = "Thursday";
        public static string WEEKDAY_FRIDAY = "Friday";
        public static string WEEKDAY_SATURDAY = "Saturday";

        // File paths for flights and airports data
        public static string FLIGHTS_TEXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\Files\flights.csv");
        public static string AIRPORTS_TEXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\Files\airports.csv"); // TODO (Update the path)

        // Lists to store flights and airports data
        public static List<Flight> flights = new List<Flight>();
        public static List<string> airports = new List<string>();

        // Constructor to populate airports and flights
        public FlightManager()
        {
            populateAirports();
            populateFlights();
        }

        // Method to get the list of airports
        public List<string> getAirports()
        {
            return airports;
        }

        // Static method to get the list of flights
        public static List<Flight> getFlights()
        {
            return flights;
        }

        // Method to find an airport by its code
        public string findAirportByCode(string code)
        {
            foreach (string airport in airports)
            {
                if (airport.Equals(code))
                    return airport;
            }
            return null;
        }

        // Static method to find a flight by its code
        public static Flight findFlightByCode(string code)
        {
            foreach (Flight flight in flights)
            {
                if (flight.Code.Equals(code))
                    return flight;
            }
            return null;
        }

        // Method to populate the flights list from a CSV file
        private void populateFlights()
        {
            flights.Clear();
            try
            {
                int counter = 0;
                Flight flight;
                foreach (string line in File.ReadLines(FLIGHTS_TEXT))
                {
                    Console.WriteLine(line);

                    string[] parts = line.Split(",");

                    string code = parts[0];
                    string airline = parts[1];
                    string from = parts[2];
                    string to = parts[3];
                    string weekday = parts[4];
                    string time = parts[5];
                    int seatsAvailable = short.Parse(parts[6]);
                    double pricePerSeat = double.Parse(parts[7]);
                    string fromAirport = findAirportByCode(from);
                    string toAirport = findAirportByCode(to);

                    try
                    {
                        flight = new Flight(code, airline, fromAirport, toAirport, weekday, time, seatsAvailable, pricePerSeat);
                        flights.Add(flight);
                    }
                    catch (Exception e) // Catching InvalidFlightCodeException or any other exception
                    {
                        // Handle exception (e.g., log the error)
                    }

                    counter++;
                }
            }
            catch (Exception e)
            {
                // Handle exception (e.g., log the error)
            }
        }

        // Method to find flights based on criteria
        public List<Flight> findFlights(string from, string to, string weekday)
        {
            List<Flight> found = new List<Flight>();
            this.populateFlights();

            if (from != "Any" && to != "Any" && weekday != "Any")
            {
                found.Add(flights.Find(f => (f.From == from && f.To == to && f.Weekday == weekday)));
            }
            else if (from == "Any" && to == "Any" && weekday == "Any")
            {
                found = flights;
            }
            else
            {
                if (from == "Any" && to == "Any")
                {
                    found = flights.FindAll(f => f.Weekday == weekday);
                }
                else if (from == "Any" && weekday == "Any")
                {
                    found = flights.FindAll(f => f.To == to);
                }
                else if (to == "Any" && weekday == "Any")
                {
                    found = flights.FindAll(f => f.From == from);
                }
                else if (from == "Any")
                {
                    found = flights.FindAll(f => (f.To == to && f.Weekday == weekday));
                }
                else if (to == "Any")
                {
                    found = flights.FindAll(f => (f.From == from && f.Weekday == weekday));
                }
                else if (weekday == "Any")
                {
                    found = flights.FindAll(f => (f.From == from && f.To == to));
                }
            }

            return found;
        }

        // Method to populate the airports list from a CSV file
        private void populateAirports()
        {
            try
            {
                int counter = 0;
                foreach (string line in File.ReadLines(AIRPORTS_TEXT))
                {
                    string[] parts = line.Split(",");

                    string code = parts[0];
                    string name = parts[1];
                    airports.Add(code);
                    counter++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); // Log the error message
            }
        }
        //Over riding the ToString method to stringify an object of FlightManager
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
