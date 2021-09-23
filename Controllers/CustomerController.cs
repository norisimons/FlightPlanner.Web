using FlightPlanner.Web.DbContext;
using FlightPlanner.Web.Models;
using FlightPlanner.Web.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public CustomerController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirport(string search)
        {
            //var airportDescription = FlightStorage.GetAirportName(search);
            var airportDescription = FlightStorage.GetAirportName(search, _context);
            return airportDescription.Length == 0 ? Ok(search) : Ok(airportDescription);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            //var flight = FlightStorage.GetById(id);
            ////var flight = FlightStorage.GetById(id, _context); 

            var flight = _context.Flights
                    .Include(a => a.To)
                    .Include(a => a.From)
                    .SingleOrDefault(f => f.Id == id);

            if (flight == null)
                return NotFound();
            return Ok(flight);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(SearchFlightsRequest flight)
        {
            if (flight.From == flight.To)
            {
                return BadRequest();
            }

            //var x = FlightStorage.FindFlight(flight);

            var x = FlightStorage.FindFlight(flight, _context);
            return Ok(x);
        }
    }
}

