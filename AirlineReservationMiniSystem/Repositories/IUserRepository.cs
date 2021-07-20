using AirlineReservationMiniSystem.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AirlineReservationMiniSystem.Models
{
	public interface IUserRepository
	{
		Task<List<ApplicationUser>> AllUsers { get; }
		Task<ApplicationUser> GetUserById(string userId);
		ApplicationUser GetUserByEmailAndPassword(string email, string password);
		ApplicationUser GetUserByEmail(string email);

		Task<ApplicationUser> Add(ApplicationUser user);

		Task<int> Update(ApplicationUser user);
		Task<ApplicationUser> Delete(string userId);

		Task<int> SaveChangesAsync();
	}
}
