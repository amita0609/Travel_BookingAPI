using AutoMapper;
using TravelBookingAPI.Models;
using TravelBookingAPI.Models.Dto;

namespace TravelBookingAPI
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
           
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
    }
}
