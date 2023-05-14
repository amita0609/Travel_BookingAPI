using TravelBookingAPI.Models;

namespace TravelBookingAPI.Repository.IRepository
{
    public interface IJourneyRepository : IRepository<Journey>
    {

        Task<Journey> UpdateAsync(Journey entity);
    }
}
