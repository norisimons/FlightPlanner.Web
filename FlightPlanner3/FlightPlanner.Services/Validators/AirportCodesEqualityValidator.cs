using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
namespace FlightPlanner.Services.Validators
{
    public class AirportCodesEqualityValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return request?.From?.Airport?.Trim().ToLower() !=
                request?.To?.Airport?.Trim().ToLower();
        }
    }
}
