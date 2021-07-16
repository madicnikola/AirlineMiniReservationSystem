using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public class Flight
	{
		[Key]
		public int FlightId { get; set; }
		[Required]
		[DisplayName("Departure City")]

		public City DepartureCity { get; set; }
		[Required]

		[DisplayName("Destination City")]
		public City DestinationCity { get; set; }
		[Required]

		[DisplayName("Departure Datetime")]

		public DateTime DepartureDateTime { get; set; }
		[Required]
		[DisplayName("Total Number Of Seats")]

		public int TotalNumberOfSeats { get; set; }
		[DisplayName("Number Free Of Seats")]

		public int NumberOfFreeSeats { get; set; }
		[DisplayName("Number Of Connections")]

		public int NumberOfConnections { get; set; }
	}
}
