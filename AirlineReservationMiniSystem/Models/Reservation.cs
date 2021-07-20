using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AirlineReservationMiniSystem.Areas.Identity.Data;

namespace AirlineReservationMiniSystem.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        [Required]
        public Flight Flight { get; set; }
        public string UserId { get; set; }
        
        [NotMapped]
        public ApplicationUser User { get; set; }
        public DateTime DateOfReservation { get; set; }
        public ReservationStatus Status { get; set; }
        public int NumberOfReservedSeats { get; set; }

		public string AgentId { get; set; }
	}
}