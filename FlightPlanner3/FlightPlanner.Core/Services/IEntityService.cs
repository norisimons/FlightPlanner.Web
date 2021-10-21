using FlightPlanner.Core.Models;
using System.Linq;

namespace FlightPlanner.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        IQueryable<T> Query();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
