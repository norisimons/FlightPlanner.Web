using FlightPlanner.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Web.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 1;
        private static readonly object _locker = new();

        public static Flight GetById(int id)
        {
            return _flights.SingleOrDefault(f => f.Id == id);
        }

        public static void ClearFlight()
        {
            _flights.Clear();
        }

        public static Flight AddFlight(Flight flight)
        {
            lock (_locker)
            {
                flight.Id = _id;
                _flights.Add(flight);
                _id++;
                return flight;
            }
        }

        public static bool Exists(Flight flight)
        {
            lock (_locker)
            {
                return _flights.Any(f => f.ArrivalTime == flight.ArrivalTime
                && f.DepartureTime == flight.DepartureTime
                && f.Carrier == flight.Carrier
                && f.From.AirportCode == flight.From.AirportCode
                && f.To.AirportCode == flight.To.AirportCode);
            }
        }

        public static void DeleteFlight(int id)
        {
            lock (_locker)
            {
                var flight = _flights.SingleOrDefault(f => f.Id == id);
                if (flight != null)
                    _flights.Remove(flight);
            }
        }

        public static bool IsValid(Flight flight)
        {
            if (flight?.To == null)
                return false;

            if (flight?.From == null)
                return false;

            if (string.IsNullOrEmpty(flight.To?.AirportCode) ||
                string.IsNullOrEmpty(flight.To?.City) ||
                string.IsNullOrEmpty(flight.To?.Country))
                return false;

            if (string.IsNullOrEmpty(flight.From?.AirportCode) ||
                string.IsNullOrEmpty(flight.From?.City) ||
                string.IsNullOrEmpty(flight.From?.Country))
                return false;

            if (string.IsNullOrEmpty(flight.ArrivalTime) ||
                string.IsNullOrEmpty(flight.DepartureTime) ||
                string.IsNullOrEmpty(flight.Carrier))
                return false;

            if (flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower())
            {
                return false;
            }

            if (DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime))
            {
                return false;
            }
            return true;
        }

        public static Airport[] GetAirportName(string name)
        {
            var names = name.Replace(" ", "").ToUpper();
            var airportDescript = _flights.Select(a => a.From).Where(a => a.AirportCode.ToUpper().Contains(names)
                || a.City.ToUpper().Contains(names)
                || a.Country.ToUpper().Contains(names));
            return airportDescript.ToArray();
        }

        public static PageResult FindFlight(SearchFlightsRequest flight)
        {
            var flights = _flights.Where(f =>
                f.From.AirportCode.Trim().ToLower() == flight.From.Trim().ToLower() &&
                f.To.AirportCode.Trim().ToLower() == flight.To.Trim().ToLower() &&
                f.DepartureTime.Substring(0, 10) == flight.DepartureDate).ToList();
            var result = new PageResult()
            {
                Page = flights.Count > 1 ? 1 : 0,
                TotalItems = flights.Count,
                Items = new List<Flight>(flights)
            };
            return result;
        }
    }
}
