using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public interface IFlightRepository
	{
		Task<IEnumerable<Flight>> AllFlights();
		IEnumerable<Flight> FlightsByDate(DateTime dateTime);
		Task<IEnumerable<Flight>> SearchFlights(City departureCity, City destinationCity, DateTime dateTime,bool DirentFlight);
		Flight GetFlightById(int flightId);

		Task<int> Add(Flight flight);
		Task<int> Update(Flight flight);

		Task<int> SaveChanges();
	}
}
