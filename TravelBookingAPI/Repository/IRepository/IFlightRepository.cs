using TravelBookingAPI.Models;

namespace TravelBookingAPI.Repository.IRepository
{
    public interface IFlightRepository : IRepository<Flight>
    {

        Task<Flight> UpdateAsync(Flight entity);
    }
}
