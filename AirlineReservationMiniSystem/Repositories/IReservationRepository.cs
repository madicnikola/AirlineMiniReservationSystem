using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public interface IReservationRepository
	{
		IEnumerable<Reservation> AllReservations { get; }

		IEnumerable<Reservation> ReservationsByStatus(ReservationStatus status);

		Reservation GetReservationById(int reservationId);
	}
}
