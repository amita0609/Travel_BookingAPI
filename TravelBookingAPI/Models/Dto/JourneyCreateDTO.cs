namespace TravelBookingAPI.Models.Dto
{
    public class JourneyCreateDTO
    {
        public int JourneyId { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime TravelDate { get; set; }


        public int AirlineId { get; set; }


        public int FlightId { get; set; }
        public int NumberOfPassengers { get; set; }

    }
}
