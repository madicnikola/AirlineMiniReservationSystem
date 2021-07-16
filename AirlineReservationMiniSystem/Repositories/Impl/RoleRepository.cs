using AirlineReservationMiniSystem.Areas.Identity.Data;
using AirlineReservationMiniSystem.Data;
using AirlineReservationMiniSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Repositories.Impl
{
	public class RoleRepository : IRoleRepository
	{
		private readonly IdentityContext _identityContext;
		private readonly IUserRepository _userRepository;


		public RoleRepository(IdentityContext identityContext, IUserRepository userRepository)
		{
			_identityContext = identityContext;
			_userRepository = userRepository;
		}

		public List<IdentityRole> AllRoles => _identityContext.Roles.ToList();

		public async Task<IdentityUserRole<string>> AddUserRole(ApplicationUser user)
		{

				var entity = new IdentityUserRole<string>
				{
					UserId = _userRepository.GetUserByEmail(user.Email).Id,
					RoleId = user.Role.Id
				};
			return _identityContext.UserRoles.Add(entity).Entity;
		}

		public async Task<IdentityRole> GetRoleById(string roleId)
		{
			return _identityContext.Roles.FirstOrDefault(r => r.Id == roleId);
		}

		public async Task<IdentityRole> GetRoleByUserId(string userId)
		{
			var userRole = await _identityContext.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId);
			return _identityContext.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId).Result;
		}

		public Task<int> SaveChangesAsync()
		{
			return _identityContext.SaveChangesAsync();
		}

		public async Task<int> Update(ApplicationUser user, string roleId)
		{
			var userRole = await _identityContext.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == user.Id);
			_identityContext.Remove(userRole);
			var entity = new IdentityUserRole<string>
			{
				UserId  = user.Id,
				RoleId = roleId
			};
			_identityContext.UserRoles.Add(entity);
			return await _identityContext.SaveChangesAsync();
		}

	}
}
