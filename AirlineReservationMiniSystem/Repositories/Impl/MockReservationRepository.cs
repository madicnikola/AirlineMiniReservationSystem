using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public class MockReservationRepository : IReservationRepository
	{
		public IFlightRepository FlightRepository { get; set; }
		public IUserRepository UserRepository { get; set; }
		public MockReservationRepository(IFlightRepository flightRepository, IUserRepository userRepository)
		{
			FlightRepository = flightRepository;
			UserRepository = userRepository;
		}

		IEnumerable<Reservation> IReservationRepository.AllReservations => throw new NotImplementedException();

		public Reservation GetReservationById(int reservationId)
		{
			throw new NotImplementedException();
		}

		IEnumerable<Reservation> IReservationRepository.ReservationsByStatus(ReservationStatus status)
		{
			throw new NotImplementedException();
		}
	}
}
