using System.Threading.Tasks;
using AirlineReservationMiniSystem.Models;
using Microsoft.AspNetCore.SignalR;

namespace AirlineReservationMiniSystem.Hubs
{
	public class ReservationHub: Hub
	{
		private readonly IReservationRepository _reservationRepository;

		public ReservationHub(IReservationRepository reservationRepository)
		{
			_reservationRepository = reservationRepository;
		}


		public async Task GetUpdateForReservation(int reservationId)
		{
			var reservation = await _reservationRepository.GetReservationById(reservationId);
			await Clients.All.SendAsync("ReceiveReservationUpdate",
				reservation);
		}
		public Task JoinRoom(string roomName)
		{
			return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
		}
		
		public Task LeaveRoom(string roomName)
		{
			return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
		}

		public async Task NotifyClient(int reservationId, ReservationStatus status)
		{
			await Clients.Others.SendAsync("ReceiveReservationUpdate",
							new {ReservationId = reservationId, ReservationStatus = status}
							);
		}
	}
}
