using AirlineReservationMiniSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.ViewModels
{
	public class FlightListViewModel
	{
		public IEnumerable<Flight> Flights { get; set; }

		public string CurrentUser { get; set; }

		public bool DirectFlightsOnly { get; set; }

		public SearchFlightsViewModel SearchFlightsViewModel { get; set; }

	}
}
