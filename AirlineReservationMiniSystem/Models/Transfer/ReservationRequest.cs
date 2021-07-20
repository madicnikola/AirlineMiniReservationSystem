using AirlineReservationMiniSystem.Models;

namespace AirlineReservationMiniSystem.ViewModels
{
	public class ReservationRequest
	{
		public int FlightId { get; set; }
		public int NumberOfSeats { get; set; }
	}
}