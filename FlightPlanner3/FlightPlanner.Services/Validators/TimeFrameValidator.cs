using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using System;
namespace FlightPlanner.Services.Validators
{
    public class TimeFrameValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            try
            {
                return DateTime.Parse(request.ArrivalTime) > DateTime.Parse(request.DepartureTime);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
