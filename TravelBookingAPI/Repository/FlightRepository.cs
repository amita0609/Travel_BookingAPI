using TravelBookingAPI.Data;
using TravelBookingAPI.Models;
using TravelBookingAPI.Repository.IRepository;

namespace TravelBookingAPI.Repository
{
    public class FlightRepository: Repository<Flight>, IFlightRepository
    {
        private readonly ApplicationDbContext _db;

    public FlightRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }


  

        public async Task<Flight> UpdateAsync(Flight flight)
        {
            _db.Flights.Update(flight);
            await _db.SaveChangesAsync();
            return flight;
        }
    }
}
