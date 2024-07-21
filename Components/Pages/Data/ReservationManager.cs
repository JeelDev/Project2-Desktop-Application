using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignment2Group10.Components.Pages.Data
{
    internal class ReservationManager
    {
        // Path to the reservation data file
        private static string Reservation_TXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\Files\reservation.csv");

        // Random number generator for reservation code generation
        private static Random random = new Random();

        // List to store reservations
        private static List<Reservation> reservations = new List<Reservation>();

        // Method to find reservations based on provided criteria
        public List<Reservation> FindReservations(string reservationCode, string airline, string name)
        {
            List<Reservation> found = new List<Reservation>();

            foreach (Reservation reservation in reservations)
            {
                if (reservation.Code.Contains(reservationCode) && reservation.Airline.Contains(airline) && reservation.Name.Contains(name))
                {
                    found.Add(reservation);
                }
                else if (reservation.Code.Contains(reservationCode))
                {
                    found.Add(reservation);
                }
                else if (reservation.Airline.Contains(airline))
                {
                    found.Add(reservation);
                }
                else if (reservation.Name.Contains(name))
                {
                    found.Add(reservation);
                }
            }

            return found;
        }

        // Method to generate a unique reservation code
        public static string GenerateReservationCode()
        {
            string reservationCode;

            do
            {
                char letter = (char)('A' + random.Next(26));
                string numbers = random.Next(1000, 10000).ToString();
                reservationCode = letter + numbers;
            } while (IsCodeGenerated(reservationCode, Reservation_TXT));

            return reservationCode;
        }

        // Method to check if a reservation code is already generated
        private static bool IsCodeGenerated(string reservationCode, string Reservation_TXT)
        {
            if (!File.Exists(Reservation_TXT))
            {
                return false;
            }

            List<string> existingCodes = File.ReadAllLines(Reservation_TXT).ToList();

            return existingCodes.Any(line => line.Split(',')[0] == reservationCode);
        }

        // Method to get all reservations from the data file
        public static List<Reservation> GetReservations()
        {
            List<Reservation> res = new List<Reservation>();
            foreach (string line in File.ReadLines(Reservation_TXT))
            {
                string[] parts = line.Split(",");
                string reservationCode = parts[0];
                string flightCode = parts[1];
                string airline = parts[2];
                double cost = double.Parse(parts[3]);
                string name = parts[4];
                string citizenship = parts[5];
                string status = parts[6];

                Reservation newReservation = new Reservation(reservationCode, flightCode, airline, cost, name, citizenship, status);
                res.Add(newReservation);
            }
            reservations = res;
            return res;
        }

        // Method to make a new reservation
        public Reservation makeReservation(Flight flight, string passengerName, string citizenship)
        {
            Reservation res = new Reservation()
            {
                Code = GenerateReservationCode(),
                Airline = flight.Airline,
                FlightCode = flight.Code,
                Name = passengerName,
                Citizenship = citizenship,
                Cost = flight.CostPerSeat,
                Active = "Active"
            };
            this.Persist(res);
            return res;
        }

        // Method to persist the reservation to the data file
        private void Persist(Reservation res)
        {
            File.AppendAllText(Reservation_TXT, $"{res.Code},{res.FlightCode},{res.Airline},{res.Cost},{res.Name},{res.Citizenship},{res.Active}\n");
        }

        // Method to update an existing reservation
        public void UpdateReservation(Reservation res)
        {
            var lines = File.ReadAllLines(Reservation_TXT).ToList();
            bool reservationFound = false;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split(",");
                if (parts[0] == res.Code)
                {
                    parts[1] = res.FlightCode;
                    parts[2] = res.Airline;
                    parts[3] = res.Cost.ToString();
                    parts[4] = res.Name;
                    parts[5] = res.Citizenship;
                    parts[6] = res.Active;
                    lines[i] = string.Join(",", parts);
                    reservationFound = true;
                    break;
                }
            }
            if (reservationFound)
            {
                File.WriteAllLines(Reservation_TXT, lines);
            }
            else
            {
                throw new Exception("Reservation not found.");
            }
        }
        //Over riding the ToString method to stringify an object of FlightManager
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
