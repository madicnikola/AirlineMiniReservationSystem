using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public class MockFlightRepository : IFlightRepository
	{
		public async Task<IEnumerable<Flight>> AllFlights()
		{
			var list = new List<Flight>
			{
				new Flight{ FlightId =1 , DepartureCity=City.BEOGRAD, DestinationCity = City.NIS,DepartureDateTime = new DateTime(), NumberOfFreeSeats=100, TotalNumberOfSeats=100},
				new Flight{ FlightId =2 , DepartureCity=City.NIS, DestinationCity = City.PRISTINA,DepartureDateTime = new DateTime(), NumberOfFreeSeats=100, TotalNumberOfSeats=100},
				new Flight{ FlightId =3 , DepartureCity=City.BEOGRAD, DestinationCity = City.KRALJEVO,DepartureDateTime = new DateTime(), NumberOfFreeSeats=100, TotalNumberOfSeats=100}
			};
			return list;
		}
		public Task<IEnumerable<Flight>> FlightsByDate {get;}

		public async Task<Flight> GetFlightById(int flightId)
		{
			var list =AllFlights();
			if (list.IsCompletedSuccessfully)
			{
				return list.Result.FirstOrDefault(p => p.FlightId == flightId);
			}
			else
			{
				return null;
			}
		}

		public IEnumerable<Flight> SearchFlights(City departureCity, City destinationCity, DateTime dateTime, bool DirentFlight)
		{
			throw new NotImplementedException();
		}

		IEnumerable<Flight> IFlightRepository.FlightsByDate(DateTime dateTime)
		{
			throw new NotImplementedException();
		}

		public Task<Flight> Add(Flight flight)
		{
			throw new NotImplementedException();
		}

		public Task<Flight> Update(Flight flight)
		{
			throw new NotImplementedException();
		}

		public Task<int> SaveChanges()
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<Flight>> IFlightRepository.SearchFlights(City departureCity, City destinationCity, DateTime dateTime, bool DirentFlight)
		{
			throw new NotImplementedException();
		}

		Task<int> IFlightRepository.Add(Flight flight)
		{
			throw new NotImplementedException();
		}

		Task<int> IFlightRepository.Update(Flight flight)
		{
			throw new NotImplementedException();
		}
	}
}
