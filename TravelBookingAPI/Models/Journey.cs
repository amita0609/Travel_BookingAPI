using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelBookingAPI.Models
{
    public class Journey
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JourneyId { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime TravelDate { get; set; }
        public int NumberOfPassengers { get; set; }

        [ForeignKey("Airline")]
        public int AirlineId { get; set; }

        [ForeignKey("Flight")]
        public int FlightId { get; set; }
       

        public Airline Airline { get; set; }
        public Flight Flight { get; set; }
    }
}
