using AirlineReservationMiniSystem.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<Flight> Flights { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Here populate database if it has no data
			Flight f1 = new Flight { FlightId = 1, DepartureCity = City.BEOGRAD, DestinationCity = City.NIS, DepartureDateTime = new DateTime(), NumberOfFreeSeats = 100, TotalNumberOfSeats = 100 };
			Flight f2 = new Flight { FlightId = 2, DepartureCity = City.NIS, DestinationCity = City.PRISTINA, DepartureDateTime = new DateTime(), NumberOfFreeSeats = 100, TotalNumberOfSeats = 100 };
			Flight f3 = new Flight { FlightId = 3, DepartureCity = City.BEOGRAD, DestinationCity = City.KRALJEVO, DepartureDateTime = new DateTime(), NumberOfFreeSeats = 100, TotalNumberOfSeats = 100 };

			modelBuilder.Entity<Flight>().HasData(f1);
			modelBuilder.Entity<Flight>().HasData(f2);
			modelBuilder.Entity<Flight>().HasData(f3);
		//	modelBuilder.Entity<Reservation>().HasData(
		//	new Reservation
		//	{
		//		ReservationId = 1,
		//		Flight = f1,
		//		User = u1,
		//		DateOfReservation = new DateTime(),
		//		NumberOfReservedSeats = 4,
		//		Status = ReservationStatus.PENDING
		//	}
		//);
		}
	}
}
