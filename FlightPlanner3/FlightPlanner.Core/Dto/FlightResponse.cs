namespace FlightPlanner.Core.Dto
{
    public class FlightResponse
    {
        public int Id { get; set; }
        public AirportResponse To { get; set; }
        public AirportResponse From { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
