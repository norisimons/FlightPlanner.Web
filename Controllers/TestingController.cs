using FlightPlanner.Web.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Web.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        [Route("clear")]
        [HttpPost]
        public IActionResult Clear()
        {
            FlightStorage.ClearFlight();
            return Ok();
        }
    }
}
