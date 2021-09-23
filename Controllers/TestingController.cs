using FlightPlanner.Web.DbContext;
using Microsoft.AspNetCore.Mvc;

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
            foreach (var entity in _context.Airport)
                _context.Airport.Remove(entity);

            foreach (var entity in _context.Flights)
                _context.Flights.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}
