namespace TravelBookingAPI.Models.Dto
{
    public class JourneyCreateDTO
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime TravelDate { get; set; }


        public int AirlineId { get; set; }


        public int FlightId { get; set; }
        public int NumberOfPassengers { get; set; }

        public Airline Airline { get; set; }
        public Flight Flight { get; set; }
    }
}
