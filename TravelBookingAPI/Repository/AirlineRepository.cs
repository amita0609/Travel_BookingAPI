using TravelBookingAPI.Data;
using TravelBookingAPI.Models;
using TravelBookingAPI.Repository.IRepository;

namespace TravelBookingAPI.Repository
{
    public class AirlineRepository : Repository<Airline>, IAirlineRepository
    {
        private readonly ApplicationDbContext _db;

        public AirlineRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public async Task<Airline> UpdateAsync(Airline entity)
        {
            _db.Airlines.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
