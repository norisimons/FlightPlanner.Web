using System.Collections.Generic;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }
        public List<Airport> SearchAirport(string flight)
        {
            List<Airport> response = new();
            foreach (Airport airport in _context.Airport)
            {
                if (airport.City.ToLower().Contains(flight.ToLower().Trim()) ||
                    airport.Country.ToLower().Contains(flight.ToLower().Trim()) ||
                    airport.AirportCode.ToLower().Contains(flight.ToLower().Trim()) ||
                    airport.City.ToLower().Contains(flight.ToLower().Trim()) ||
                    airport.Country.ToLower().Contains(flight.ToLower().Trim()) ||
                    airport.AirportCode.ToLower().Contains(flight.ToLower().Trim()))
                {
                    response.Add(airport);
                }
            }
            return response;
        }
    }
}