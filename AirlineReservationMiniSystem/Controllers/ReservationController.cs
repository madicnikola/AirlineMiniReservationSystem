using System;
using System.Linq;
using System.Security.Claims;
using AirlineReservationMiniSystem.Hubs;
using AirlineReservationMiniSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using AirlineReservationMiniSystem.Areas.Identity.Data;
using AirlineReservationMiniSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace AirlineReservationMiniSystem.Controllers
{
	[Authorize]
	public class ReservationController : Controller
	{
		private readonly IHubContext<ReservationHub> _reservationHub;
		private readonly IReservationRepository _reservationRepository;
		private readonly IFlightRepository _flightRepository;
		private readonly IUserRepository _userRepository;
		

		public ReservationController(IHubContext<ReservationHub> reservationHub, IFlightRepository flightRepository, IReservationRepository reservationRepository, IUserRepository userRepository)
		{
			_reservationHub = reservationHub;
			_flightRepository = flightRepository;
			_reservationRepository = reservationRepository;
			_userRepository = userRepository;
		}

		public async Task<IActionResult> Index()
		{
			var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
			var viewModel = new ReservationsViewModel
			{
				Reservations = await _reservationRepository.getReservationsByUserId(userId)
			};
			
			return View(viewModel);
		 }


		[HttpPost]
		[Authorize(Policy = "isClient")]
		public async Task<IActionResult> RequestReservation([FromBody]ReservationRequest reservationRequest)
		{
			var reservation = new Reservation
			{
				Flight = await _flightRepository.GetFlightById(reservationRequest.FlightId),
				UserId =  User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value,
				NumberOfReservedSeats = reservationRequest.NumberOfSeatsToReserve,
				DateOfReservation = DateTime.Now,
				Status = ReservationStatus.PENDING
			};
			await _reservationRepository.Add(reservation);
			reservation = await _reservationRepository.GetReservationByUserAndFLightAndDate(reservation.Flight.FlightId,
				reservation.UserId, reservation.DateOfReservation);
			
			
			await _reservationHub.Clients.Group("Agent").SendAsync("NewReservation", reservation);

			return Accepted(reservation.ReservationId);
		}


	}
}
