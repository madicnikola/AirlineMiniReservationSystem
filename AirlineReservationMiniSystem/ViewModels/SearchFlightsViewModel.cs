using AirlineReservationMiniSystem.Models;
using System;
using System.Collections.Generic;

namespace AirlineReservationMiniSystem.ViewModels
{
	public class SearchFlightsViewModel
	{
		public City DepartureCity { get; set; }
		public City DestinationCity { get; set; }
		public DateTime DepartureDateTime { get; set; }
		public bool DirectFlight { get; set; }
		public IEnumerable<Flight> Flights { get; set; }

	}
}
