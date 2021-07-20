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

        public Task<List<Reservation>> AllReservations()
        {
            return _appDbContext.Reservations.Include(r => r.Flight).ToListAsync();
        }

        public Task<List<Reservation>> ReservationsByStatus(ReservationStatus status)
        {
            return _appDbContext.Reservations.Where(r => r.Status == status).Include(r => r.Flight)
                .ToListAsync();
        }

        public Task<Reservation> GetReservationById(int reservationId)
        {
            return _appDbContext.Reservations.Include(r => r.Flight)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);
        }

        public Task<int> Add(Reservation reservation)
        {
            _appDbContext.Reservations.Add(reservation);
            return _appDbContext.SaveChangesAsync();
        }

        public Task<Reservation> GetReservationByUserAndFLightAndDate(int flightId, string userId,
            DateTime DateOfReservation)
        {
            return _appDbContext.Reservations.FirstOrDefaultAsync(r => r.Flight.FlightId == flightId &&
                                                                       r.UserId == userId &&
                                                                       r.DateOfReservation == DateOfReservation);
        }

        public Task<List<Reservation>> getReservationsByUserId(string userId)
        {
            return _appDbContext.Reservations.Where(r => r.UserId == userId).Include(r => r.Flight).ToListAsync();
        }

        public Task<int> DeleteReservationsByFlightId(int id)
        {
            _appDbContext.RemoveRange(_appDbContext.Reservations.Where(f => f.Flight.FlightId == id));
            return _appDbContext.SaveChangesAsync();
        }

        public Task<int> Update(Reservation reservation)
        {
            _appDbContext.Reservations.Update(reservation);
            return _appDbContext.SaveChangesAsync();
        }
    }
}