using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AirlineReservationMiniSystem.Areas.Identity.Data;

namespace AirlineReservationMiniSystem.Models
{
    public class AgentReservation
    {
        [Key]
        public int ReservationId { get; set; }
        public string UserId { get; set; }
        public Reservation Reservation { get; set; }
        [NotMapped]
        public ApplicationUser User { get; set; }
    }
}