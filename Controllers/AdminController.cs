using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Web.Storage;
using FlightPlanner.Web.Models;
using FlightPlanner.Web.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Web.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public AdminController(FlightPlannerDbContext context)
        {
            _context = context;
        }
        private static readonly object _locker = new();
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {

            lock (_locker)
            {
                var flight = _context.Flights
                    .Include(a => a.To)
                    .Include(a => a.From)
                    .SingleOrDefault(f => f.Id == id);
                //var flight = FlightStorage.GetById(id);
                if (flight == null)
                    return NotFound();
                return Ok(flight);
            }
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_locker)
            {
                if (!FlightStorage.IsValid(flight))
                    return BadRequest();

                if (FlightStorage.Exists(flight))
                    return Conflict();

                //FlightStorage.AddFlight(flight);
                _context.Flights.Add(flight);
                _context.SaveChanges();

                return Created("", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_locker)
            {
                //var flight = _context.Flights.SingleOrDefault(f => f.Id == id);

                var flight = _context.Flights
                    .Include(a => a.To)
                    .Include(a => a.From)
                    .SingleOrDefault(f => f.Id == id);

                if (flight != null)
                {
                    _context.Airports.Remove(flight.To);
                    _context.Airports.Remove(flight.From);
                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                }

                return Ok();
                //FlightStorage.DeleteFlight(id);
                //return Ok();
            }
        }
    }
}
