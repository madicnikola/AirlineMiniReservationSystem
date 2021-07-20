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
	
	// Include this ->
	// [Authorize(Policy = "IsAdmin")]
	public class UserController : Controller
	{
		private readonly List<UserViewModel> userViewModels = new List<UserViewModel>();
		private readonly IUserRepository _userRepository;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRoleRepository _roleRepository;
		private readonly IReservationRepository _reservationRepository;

		[BindProperty] public UserViewModel Input { get; set; }


		public UserController(IUserRepository userRepository, IRoleRepository roleRepository, IReservationRepository reservationRepository, UserManager<ApplicationUser> userManager)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_reservationRepository = reservationRepository;
			_userManager = userManager;
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

		public async Task<IActionResult> ViewAgentStats()
		{
			var agentsViewModelList = new List<UserViewModel>();
			
			var agents = await _userManager.GetUsersInRoleAsync("Agent");
			var identityRole = await _roleRepository.GetRoleByUserId(agents.First().Id);
			
			if (identityRole == null)
			{
				return Json(new { success = false, message = "Error while retrieving the role" });
			}
			
			foreach (var agent in agents)
			{
				agentsViewModelList.Add(
					new UserViewModel
					{
						User = agent,
						Role = identityRole,
						NumberOfApprovedReservations = await _reservationRepository.CountReservationsByAgentId(agent.Id)
					});
			}
			
			var viewModel = new GroupedUserViewModel
			{
				Agents = agentsViewModelList
			};
			
			return View("AgentStatistics",viewModel);
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
	}
}
