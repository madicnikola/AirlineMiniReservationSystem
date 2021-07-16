using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public class ReservationRepository : IReservationRepository
	{
		private readonly AppDbContext _appDbContext;

		public ReservationRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public IEnumerable<Reservation> AllReservations {
			get
			{
				return _appDbContext.Reservations.Include(r => r.User).Include(r => r.Flight);
			}
		
		}

		public IEnumerable<Reservation> ReservationsByStatus(ReservationStatus status)
		{
				return _appDbContext.Reservations.Include(r => r.User).Include(r => r.Flight).Where(r => r.Status == status);
		}

	public Reservation GetReservationById(int reservationId)
		{
			return _appDbContext.Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
		}
	}
}
