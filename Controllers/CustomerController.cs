using FlightPlanner.Web.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirport(string search)
        {
            var airportDescription = FlightStorage.GetAirportName(search);
            return airportDescription.Length == 0 ? Ok(search) : Ok(airportDescription);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetById(id);
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

            var x = FlightStorage.FindFlight(flight);
            return Ok(x);
        }
    }
}

