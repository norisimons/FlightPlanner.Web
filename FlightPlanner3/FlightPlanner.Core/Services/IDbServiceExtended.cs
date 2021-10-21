using FlightPlanner.Core.Models;
using FlightPlanner.Core.Servives;

namespace FlightPlanner.Core.Services
{
    public interface IDbServiceExtended : IDbService
    {
        void DeleteAll<T>() where T : Entity;
    }
}
