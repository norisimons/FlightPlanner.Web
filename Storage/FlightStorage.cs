﻿using FlightPlanner.Web.DbContext;
using FlightPlanner.Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Web.Storage
{
    public static class FlightStorage
    {
        private static int _id = 1;
        private static readonly object _locker = new();

        public static Flight GetById(int id, FlightPlannerDbContext context)
        {
            return context.Flights
            .Include(a => a.To)
            .Include(a => a.From)
            .SingleOrDefault(f => f.Id == id);
        }

        public static Flight AddFlight(Flight flight, FlightPlannerDbContext context)
        {
            lock (_locker)
            {
                flight.Id = _id;
                context.Flights.Add(flight);
                _id++;
                return flight;
            }
        }
        public static bool Exists(Flight flight, FlightPlannerDbContext context)
        {
            lock (_locker)
            {
                return context.Flights.Any(f =>
                f.ArrivalTime == flight.ArrivalTime &&
                f.DepartureTime == flight.DepartureTime &&
                f.Carrier == flight.Carrier &&
                f.From.AirportCode == flight.From.AirportCode &&
                f.To.AirportCode == flight.To.AirportCode);
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

        public static Airport[] GetAirportName(string name, FlightPlannerDbContext context)
        {
            var names = name.Replace(" ", "").ToUpper();
            var airportDescript = context.Flights.Select(a => a.From)
                .Where(a => a.AirportCode.ToUpper().Contains(names) ||
                a.City.ToUpper().Contains(names) ||
                a.Country.ToUpper().Contains(names));
            return airportDescript.ToArray();
        }

        public static PageResult FindFlight(SearchFlightsRequest flight, FlightPlannerDbContext context)
        {
            var flights = context.Flights.Where(f =>
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
