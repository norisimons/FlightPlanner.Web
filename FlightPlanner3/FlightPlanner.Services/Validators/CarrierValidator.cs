using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
namespace FlightPlanner.Services.Validators
{
    public class CarrierValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request.Carrier);
        }
    }
}
