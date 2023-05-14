using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingAPI.Models.Dto
{
    public class JourneyDTO
    {
        public int JourneyId { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime TravelDate { get; set; }

       
        public int AirlineId { get; set; }

      
        public int FlightId { get; set; }
        public int NumberOfPassengers { get; set; }

        public AirlineDTO Airline { get; set; }
        public FlightDTO Flight { get; set; }
    }
}
