using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
namespace FlightPlanner.Services.Validators
{
    public class CityValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request?.To?.City) &&
                   !string.IsNullOrEmpty(request?.From?.City);
        }
    }
}
