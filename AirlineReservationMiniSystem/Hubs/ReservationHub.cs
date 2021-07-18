using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AirlineReservationMiniSystem.Hubs
{
	public class ReservationHub: Hub
	{
		public Task JoinRoom(string roomName)
		{
			return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
		}
		
		public Task LeaveRoom(string roomName)
		{
			return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
		}
	}
}
