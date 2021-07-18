using AirlineReservationMiniSystem.Areas.Identity.Data;
using AirlineReservationMiniSystem.Models;
using AirlineReservationMiniSystem.Repositories;
using AirlineReservationMiniSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Controllers
{
	public class UserController : Controller
	{
		private readonly List<UserViewModel> userViewModels = new List<UserViewModel>();
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;

		[BindProperty] public UserViewModel Input { get; set; }


		public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
		}

		public async Task<IActionResult> Index()
		{
			GroupedUserViewModel model = await GetAllUsers();

			return View(model);
		}

		//[Authorize(Policy = "IsAdmin"]
		public async Task<IActionResult> Add()
		{

			//create
			return LocalRedirect("/Identity/Account/Register");
		}

		public async Task<IActionResult> Edit(string id)
		{
			var userViewModel = new UserViewModel
			{
				User = await _userRepository.GetUserById(id),
				Role = await _roleRepository.GetRoleByUserId(id)
			};
			if (userViewModel.User == null)
			{
				return NotFound();
			}
			return View(userViewModel);
		}

		public IActionResult Details(string id)
		{
			var viewModel = new UserViewModel
			{
				User = _userRepository.GetUserById(id).Result,
				Role = _roleRepository.GetRoleByUserId(id).Result
			};
			if (viewModel.User == null)
			{
				return NotFound();
			}
			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Update()
		{
			if (ModelState.IsValid)
			{
				if (Input.User.Id == null)
				{
					return View("Details");
				}
				else
				{
					// set all user fields
					var user = await _userRepository.GetUserById(Input.User.Id);
					user.Name = Input.User.Name;
					user.Surname = Input.User.Surname;
					user.Email = Input.User.Email;
					await _userRepository.Update(user);
					await _roleRepository.Update(user, Input.Role.Id);
				}
			}
			var routeValues = new RouteValueDictionary {
				{ "id", Input.User.Id} };
			string url = "Details";

			return RedirectToAction(url, routeValues);
		}



		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			var user = _userRepository.Delete(id);
			if (user == null)
			{
				return Json(new { success = false, message = "Error while Deleting" });
			}
			await _userRepository.SaveChangesAsync();
			return RedirectToAction("Index");
		}




		private async Task<GroupedUserViewModel> GetAllUsers()
		{
			List<ApplicationUser> users = _userRepository.AllUsers.Result;
			foreach (var u in users)
			{
				userViewModels.Add(
					new UserViewModel
					{
						User = u,
						Role = await _roleRepository.GetRoleByUserId(u.Id)
					}
					);
			}
			var model = new GroupedUserViewModel
			{
				Users = userViewModels
			};
			return model;
		}

		//var allusers = _userRepository.AllUsers.ToList();
		//var users = allusers.Where(x => x.Roles.Select(role => role.Name).Contains("Client")).ToList();
		//var userVM = users.Select(user => new UserViewModel { Username = user.FullName, Roles = string.Join(",", user.Roles.Select(role => role.Name)) }).ToList();

		//var admins = allusers.Where(x => x.Roles.Select(role => role.Name).Contains("Admin")).ToList();
		//var adminsVM = admins.Select(user => new UserViewModel { Username = user.FullName, Roles = string.Join(",", user.Roles.Select(role => role.Name)) }).ToList();
		//var model = new GroupedUserViewModel { Clients = userVM, Admins = adminsVM };
	}
}
