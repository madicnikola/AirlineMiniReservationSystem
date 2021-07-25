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
using AirlineReservationMiniSystem.Repositories;
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
		private readonly IAuthorizationService _authorizationService;

		

		public ReservationController(IHubContext<ReservationHub> reservationHub, IFlightRepository flightRepository, IReservationRepository reservationRepository, IUserRepository userRepository, IAuthorizationService authorizationService)
		{
			_reservationHub = reservationHub;
			_flightRepository = flightRepository;
			_reservationRepository = reservationRepository;
			_userRepository = userRepository;
			_authorizationService = authorizationService;
		}

		public async Task<IActionResult> Index()
		{
			var result = await _authorizationService.AuthorizeAsync(User, "IsClient");
			if (result.Succeeded)
			{
				var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
				var viewModel = new ReservationsViewModel
				{
					Reservations = await _reservationRepository.GetReservationsByUserId(userId)
				};
				
				return View(viewModel);
			}
			else
			{
				var viewModel = new ReservationsViewModel
				{
					Reservations = await _reservationRepository.AllReservations()
				};
			
				return View(viewModel);
			}
		}


		[HttpPost]
		[Authorize(Policy = "IsClient")]
		public async Task<IActionResult> RequestReservation([FromBody]ReservationRequest reservationRequest)
		{
			var userId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
			var reservation = new Reservation
			{
				Flight = await _flightRepository.GetFlightById(reservationRequest.FlightId),
				UserId =  userId,
				NumberOfReservedSeats = reservationRequest.NumberOfSeats,
				User = await _userRepository.GetUserById(userId),
				DateOfReservation = DateTime.Now,
				Status = ReservationStatus.PENDING
			};
			await _reservationRepository.Add(reservation);
			reservation = await _reservationRepository.GetReservationByUserAndFLightAndDate(reservation.Flight.FlightId,
				reservation.UserId, reservation.DateOfReservation);
			
			
			await _reservationHub.Clients.Group("Agent").SendAsync("NewReservation", reservation);

			return Accepted(reservation.ReservationId);
		}

		[HttpPost]
		[Authorize(Policy = "IsAgent")]
		public async Task<IActionResult> ApproveReservation([FromBody] ReservationApproveRequest reservationRequest)
		{
			var reservation = await _reservationRepository.GetReservationById(reservationRequest.ReservationId);
			var flight = reservation.Flight;
			if (flight.NumberOfFreeSeats - reservation.NumberOfReservedSeats < 0)
			{
				reservation.Status = ReservationStatus.DECLINED;
				return BadRequest("No free seats available");
			} 
			flight.NumberOfFreeSeats -= reservation.NumberOfReservedSeats;
			await _flightRepository.Update(flight);
			reservation.Flight =flight;
			reservation.Status = ReservationStatus.CONFIRMED;
			reservation.AgentId = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
			await _reservationRepository.Update(reservation);
			await notifyClients(reservation);
			
			return Accepted(reservation.ReservationId);
		}

		[HttpPost]
		[Authorize(Policy = "IsAgent")]
		public async Task<IActionResult> CancelReservation([FromBody] int reservationId)
		{
			var reservation = await _reservationRepository.GetReservationById(reservationId);
			var flight = reservation.Flight;
			if (flight.NumberOfFreeSeats + reservation.NumberOfReservedSeats > flight.TotalNumberOfSeats)
			{
				reservation.Status = ReservationStatus.DECLINED;
				return BadRequest("Something's wrong");
			} 
			flight.NumberOfFreeSeats += reservation.NumberOfReservedSeats;
			await _flightRepository.Update(flight);
			
			reservation.Flight =flight;
			reservation.Status = ReservationStatus.DECLINED;
			reservation.AgentId = null;
			await _reservationRepository.Update(reservation);
			await notifyClients(reservation);

			return Accepted(reservation.ReservationId);
		}


		private async Task notifyClients(Reservation reservation)
		{
			await _reservationHub.Clients.Group("Client").SendAsync("ReceiveReservationUpdate",
				reservation.ReservationId);
		}
	}
}
