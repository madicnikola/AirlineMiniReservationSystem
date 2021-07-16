using AirlineReservationMiniSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Repositories
{
	public interface IRoleRepository
	{
		List<IdentityRole> AllRoles { get; }

		Task<IdentityRole> GetRoleByUserId(string userId);
		Task<IdentityRole> GetRoleById(string roleId);
		Task<IdentityUserRole<string>> AddUserRole(ApplicationUser user);
		Task<int> Update(ApplicationUser user, string roleId);
		Task<int> SaveChangesAsync();
	}
}
