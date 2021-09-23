using FlightPlanner.Web.DbContext;
using FlightPlanner.Web.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Web.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public TestingController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            //FlightStorage.ClearFlight(); //jānodzeesh visi flaigti un airporti
            //return Ok();
            foreach (var entity in _context.Airport)
                _context.Airport.Remove(entity);

            foreach (var entity in _context.Flights)
                _context.Flights.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}
