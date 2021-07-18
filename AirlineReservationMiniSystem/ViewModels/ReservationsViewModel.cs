using AirlineReservationMiniSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.ViewModels
{
	public class ReservationsViewModel
	{
		public IEnumerable<Reservation> Reservations{ get; set; }

	}
}
