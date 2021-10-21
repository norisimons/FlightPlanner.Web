using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }
        public Flight GetFullFlightById(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            return Query().Any(f =>
            f.ArrivalTime == flight.ArrivalTime &&
            f.DepartureTime == flight.DepartureTime &&
            f.Carrier == flight.Carrier &&
            f.From.AirportCode == flight.From.AirportCode &&
            f.To.AirportCode == flight.To.AirportCode);
        }

        public PageResult SearchFlight(SearchFlight data)
        {
            List<Flight> airList = _context.Flights
                .Include(f => f.To)
                .Include(f => f.From)
                .ToList();
            PageResult page = new(0, 0, new List<Flight>());
            foreach (Flight f in airList)
            {
                var depDate = f.DepartureTime.Substring(0, 10);
                if (data.From == f.From.AirportCode &&
                    data.To == f.To.AirportCode &&
                    data.DepartureDate == depDate)
                {
                    page.Items.Add(f);
                    page.Page++;
                }
            }
            page.TotalItems = page.Items.Count;
            return page;
        }

        public void DeleteFlight(int id)
        {
            var flight = _context.Flights
                .Include(f => f.To)
                .Include(f => f.From)
                .SingleOrDefault(f => f.Id == id);

            _context.Airport.Remove(flight.To);
            _context.Airport.Remove(flight.From);
            _context.Flights.Remove(flight);
            _context.SaveChanges();
        }
    }
}
