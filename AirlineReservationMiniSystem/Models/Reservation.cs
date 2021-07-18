using AirlineReservationMiniSystem.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public class Reservation
	{
		public int ReservationId { get; set; }
		public Flight Flight { get; set; }
		public string UserId { get; set; }
		public DateTime DateOfReservation { get; set; }
		public ReservationStatus Status { get; set; }

		public int NumberOfReservedSeats { get; set; }
	}
}
