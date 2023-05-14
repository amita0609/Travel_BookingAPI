using System.Linq.Expressions;
using TravelBookingAPI.Models;
using TravelBookingAPI.Models.Dto;

namespace TravelBookingAPI.Repository.IRepository
{
    public interface IUserRepository:IRepository<User>
    {
       
        Task<User> UpdateAsync( User user);

        //   bool IsUniqueUser(string name);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        // Task<User> Register(RegistrationRequestDTO registerationRequestDTO);

    }
}
