using AirlineReservationMiniSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.ViewModels
{
	public class FlightDetailsViewModel
	{
		public Flight Flight { get; set; }
		[Required]
		[DisplayName("Number of Seats to Reserve")]
		public int NumberOfSeats { get; set; }
	}
}
