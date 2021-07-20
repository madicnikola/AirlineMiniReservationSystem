using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public class FlightRepository : IFlightRepository
	{
		private readonly AppDbContext _appDbContext;

		public FlightRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<IEnumerable<Flight>> AllFlights()
		{
				return await _appDbContext.Flights.ToListAsync();
		}

		public IEnumerable<Flight> FlightsByDate(DateTime dateTime)
		{
				return _appDbContext.Flights.Where(f => f.DepartureDateTime == dateTime);
		}

		public Task<Flight> GetFlightById(int flightId)
		{
			return _appDbContext.Flights.FirstOrDefaultAsync(f => f.FlightId == flightId);
		}

		public Task<int> Add(Flight flight)
		{
			_appDbContext.Flights.Add(flight);
			return _appDbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Flight>> SearchFlights(City departureCity, City destinationCity, DateTime dateTime, bool directFlight)
		{
		  return _appDbContext.Flights.Where(f => f.DepartureCity == departureCity
												&& f.DestinationCity == destinationCity
												&& f.DepartureDateTime.Date == dateTime.Date
												&& f.NumberOfFreeSeats > 0
												&& (!directFlight || f.NumberOfConnections==0) 
												);
		}

		public Task<int> Update(Flight flight)
		{
			_appDbContext.Flights.Update(flight);
			return _appDbContext.SaveChangesAsync();
		}

		public Task<int> Delete(Flight flight)
		{
			_appDbContext.Flights.Remove(flight);
			return _appDbContext.SaveChangesAsync();
		}

		public Task<int> SaveChanges()
		{
			return _appDbContext.SaveChangesAsync();
		}
	}
}
