
using Microsoft.AspNetCore.Authorization;
using FlightPlanner.Web.Storage;
using FlightPlanner.Web.Models;
namespace FlightPlanner.Web.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private static readonly object _locker = new();
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (_locker)
            {
                var flight = FlightStorage.GetById(id);
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

                FlightStorage.AddFlight(flight);
                return Created("", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_locker)
            {
                FlightStorage.DeleteFlight(id);
                return Ok();
            }
        }
    }
}
