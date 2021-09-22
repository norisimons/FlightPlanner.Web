using FlightPlanner.Web.DbContext;
using FlightPlanner.Web.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
            FlightStorage.ClearFlight(); //jānodzeesh visi flaigti un airporti
            return Ok();
        }
    }
}
