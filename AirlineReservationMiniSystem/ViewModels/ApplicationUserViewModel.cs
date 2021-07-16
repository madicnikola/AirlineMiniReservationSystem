using AirlineReservationMiniSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.ViewModels
{
	public class GroupedUserViewModel
	{
		public List<UserViewModel> Users { get; set; }

		public List<UserViewModel> Clients { get; set; }

		public List<UserViewModel> Agents { get; set; }

		public List<UserViewModel> Admins { get; set; }
	}

	public class UserViewModel
	{
		public ApplicationUser User { get; set; }
		public IdentityRole Role { get; set; }
	}
}
