using System.Linq.Expressions;
using TravelBookingAPI.Models;

namespace TravelBookingAPI.Repository.IRepository
{
    public interface IUserRepository:IRepository<User>
    {
       
        Task<User> UpdateAsync( User entity);
       
    }
}
