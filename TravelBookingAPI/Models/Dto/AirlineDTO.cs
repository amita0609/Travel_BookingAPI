using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace TravelBookingAPI.Models.Dto
{
   // [Index(nameof(AirlineCode), IsUnique = true)]
    public class AirlineDTO
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AirlineId { get; set; }

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string AirlineCode { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only Alphabets allowed.")]
        public string AirlineName { get; set; }
    }
}
