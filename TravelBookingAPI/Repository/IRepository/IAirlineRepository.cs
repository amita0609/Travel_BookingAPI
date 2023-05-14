using TravelBookingAPI.Models;

namespace TravelBookingAPI.Repository.IRepository
{
    public interface IAirlineRepository : IRepository<Airline>
    {
       
        Task<Airline> UpdateAsync(Airline entity);
    }
}
