using System.ComponentModel.DataAnnotations;

namespace TravelBookingAPI.Models.Dto
{
    public class FlightUpdateDTO
    {
        public int FlightId { get; set; }
        public string FlightCode { get; set; }

        public string FlightName { get; set; }


        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string AirlineCode { get; set; }
    }
}
