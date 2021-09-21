using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightPlanner.Web.Models
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Country { get; set; }

        public string City { get; set; }

        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}
