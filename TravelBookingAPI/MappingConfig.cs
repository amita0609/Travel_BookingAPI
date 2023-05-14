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
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();

            CreateMap<Airline, AirlineDTO>().ReverseMap();
            CreateMap<Airline, AirlineCreateDTO>().ReverseMap();
            CreateMap<Airline, AirlineUpdateDTO>().ReverseMap();

            CreateMap<Flight, FlightDTO>().ReverseMap();
            CreateMap<Flight, FlightCreateDTO>().ReverseMap();
            CreateMap<Flight, FlightUpdateDTO>().ReverseMap();

            CreateMap<Journey, JourneyDTO>().ReverseMap();
            CreateMap<Journey, JourneyCreateDTO>().ReverseMap();
            CreateMap<Journey, JourneyUpdateDTO>().ReverseMap();
        }
    }
}
