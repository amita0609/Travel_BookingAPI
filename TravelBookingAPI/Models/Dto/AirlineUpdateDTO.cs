using System.ComponentModel.DataAnnotations;

namespace TravelBookingAPI.Models.Dto
{
    public class AirlineUpdateDTO
    {
        public int Id { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string AirlineCode { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only Alphabets allowed.")]
        public string AirlineName { get; set; }
    }
}
