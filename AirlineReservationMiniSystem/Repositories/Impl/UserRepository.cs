using AirlineReservationMiniSystem.Areas.Identity.Data;
using AirlineReservationMiniSystem.Data;
using AirlineReservationMiniSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineReservationMiniSystem.Repositories;

namespace AirlineReservationMiniSystem
{
	public class UserRepository : IUserRepository 
	{
		private readonly IdentityContext _identityContext;

		public UserRepository(IdentityContext identityContext)
		{
			_identityContext = identityContext;
		}

		public Task<List<ApplicationUser>> AllUsers => _identityContext.Users.ToListAsync();

		public async Task<ApplicationUser> GetUserById(string userId)
		{
			return _identityContext.Users.FirstOrDefaultAsync(u => u.Id == userId).Result;
		}

		public ApplicationUser GetUserByEmailAndPassword(string email, string password)
		{
			return _identityContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
		}

		public ApplicationUser GetUserByEmail(string email)
		{
			return _identityContext.Users.FirstOrDefault(u => u.Email == email);

		}

		public async Task<ApplicationUser> Add(ApplicationUser user)
		{
			return _identityContext.Users.Add(user).Entity;

		}

		public Task<int> Update(ApplicationUser user)
		{
			_identityContext.Users.Update(user);
			return _identityContext.SaveChangesAsync();
		}

		public async Task<ApplicationUser> Delete(string userId)
		{
			var user = await GetUserById(userId);
			
			return _identityContext.Users.Remove(user).Entity;
		}

		public Task<int> SaveChangesAsync()
		{
			return _identityContext.SaveChangesAsync();

		}
	}
}