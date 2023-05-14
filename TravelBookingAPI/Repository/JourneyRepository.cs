using TravelBookingAPI.Data;
using TravelBookingAPI.Models;
using TravelBookingAPI.Repository.IRepository;

namespace TravelBookingAPI.Repository
{
    public class JourneyRepository : Repository<Journey>, IJourneyRepository
    {
        private readonly ApplicationDbContext _db;

        public JourneyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Journey> UpdateAsync(Journey entity)
        {
            _db.JourneyTable.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}