using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
namespace FlightPlanner.Services.Validators
{
    public class SearchValidator : ISearchValidator
    {
        public bool IsValid(SearchFlight search)
        {
            return search.From != search.To;
        }
    }
}