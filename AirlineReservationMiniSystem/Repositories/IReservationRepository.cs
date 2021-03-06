using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public interface IReservationRepository
	{
		Task<List<Reservation>> AllReservations();

		Task<List<Reservation>> ReservationsByStatus(ReservationStatus status);

		Task<Reservation> GetReservationById(int reservationId);
		
		Task<int> Add(Reservation reservation);
		
		Task<int> Update(Reservation reservation);

		Task<Reservation> GetReservationByUserAndFLightAndDate(int flightFlightId, string userId, DateTime reservationDateOfReservation);

		Task<List<Reservation>> GetReservationsByUserId(string userId);

		Task<List<Reservation>> GetReservationsByAgentId(string agentId);
		
		Task<int> CountReservationsByAgentId(string agentId);


		Task<int> DeleteReservationsByFlightId(int id);
	}
}
