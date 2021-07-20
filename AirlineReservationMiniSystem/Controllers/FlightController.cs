using AirlineReservationMiniSystem.Models;
using AirlineReservationMiniSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationMiniSystem.Controllers
{
    [Authorize]
    public class FlightController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;

        [BindProperty] public Flight Flight { get; set; }

        public List<Flight> Flights { get; set; }

        [BindProperty] public FlightListViewModel FlightListModel { get; set; }

        public FlightController(IFlightRepository flightRepository, IReservationRepository reservationRepository,
            IUserRepository userRepository, IAuthorizationService authorizationService)
        {
            _flightRepository = flightRepository;
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _authorizationService.AuthorizeAsync(User, "IsAgent");
            if (result.Succeeded)
            {
                var allFlights = await _flightRepository.AllFlights();
                var viewModel = new SearchFlightsViewModel
                {
                    Flights = allFlights
                };
                return View("IndexAgentView", viewModel);
            }

            return View();
        }

        [HttpPost]
        [Authorize(Policy = "isAgent")]
        public async Task<IActionResult> Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Flight.FlightId == 0)
                {
                    await _flightRepository.Add(Flight);
                }
                else
                {
                    await _flightRepository.Update(Flight);
                }
            }

            var routeValues = new RouteValueDictionary
            {
                {"id", Flight.FlightId}
            };
            string url = "Details";
            return RedirectToAction(url, routeValues);
        }

        //[AllowAnonymous]
        public async Task<ViewResult> List()
        {
            FlightListViewModel flightListViewModel = new FlightListViewModel
            {
                Flights = await _flightRepository.AllFlights(),
                CurrentUser = User.Identity.Name
            };

            return View("FlightList", flightListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FindFlights(SearchFlightsViewModel searchFlightsViewModel)
        {
            this.Flights = _flightRepository.SearchFlights(searchFlightsViewModel.DepartureCity,
                searchFlightsViewModel.DestinationCity, searchFlightsViewModel.DepartureDateTime,
                searchFlightsViewModel.DirectFlight).Result.ToList();

            if (searchFlightsViewModel.DepartureCity == City.BEOGRAD &&
                searchFlightsViewModel.DestinationCity == City.BEOGRAD)
            {
                Flights = _flightRepository.AllFlights().Result.ToList();
            }
            else
            {
                Flights = _flightRepository.SearchFlights(searchFlightsViewModel.DepartureCity,
                    searchFlightsViewModel.DestinationCity, searchFlightsViewModel.DepartureDateTime,
                    searchFlightsViewModel.DirectFlight).Result.ToList();
            }

            var viewModel = new FlightListViewModel
            {
                Flights = Flights,
                DirectFlightsOnly = searchFlightsViewModel.DirectFlight,
                SearchFlightsViewModel = searchFlightsViewModel
            };

            return View("FlightList", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FilterDirectFlights(FlightListViewModel flightListModel)
        {
            var viewModel = new FlightListViewModel
            {
                Flights = _flightRepository.SearchFlights(flightListModel.SearchFlightsViewModel.DepartureCity,
                    flightListModel.SearchFlightsViewModel.DestinationCity, flightListModel.SearchFlightsViewModel.DepartureDateTime,
                    flightListModel.SearchFlightsViewModel.DirectFlight).Result.ToList()
            };


            return View("FlightList", viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var flight = await _flightRepository.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            var viewModel = new FlightDetailsViewModel
            {
                Flight = flight
            };

            return View(viewModel);
        }

        [Authorize(Policy = "IsAgent")]
        public async Task<IActionResult> Upsert(int? id)
        {
            var viewModel = new FlightDetailsViewModel
            {
            Flight = new Flight()
            };
            if (id == null)
            {
                viewModel.Flight.DepartureDateTime = DateTime.Today;
                //create
                return View(viewModel);
            }

            //update
            viewModel.Flight = await _flightRepository.GetFlightById((int) id);
            if (viewModel.Flight == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Select(int? id)
        {
            Flight = await _flightRepository.GetFlightById((int) id);
            return View("Details", new FlightDetailsViewModel
            {
                Flight = Flight,
                NumberOfSeats = 1
            });
        }
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id  == 0)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            await _reservationRepository.DeleteReservationsByFlightId(id);
            var flight = await _flightRepository.GetFlightById(id);
            await _flightRepository.Delete(flight);
            return View("FlightList", new FlightListViewModel
            {
                Flights = await _flightRepository.AllFlights()
            });
        }
        
        
        
        
        

        // [HttpPost]
        // public async Task<IActionResult> BackToList(FlightDetailsViewModel flightDetailsViewModel)
        // {
        //     var result = await _authorizationService.AuthorizeAsync(User, "IsAgent");
        //     return await FindFlights(flightDetailsViewModel.SearchFlightsViewModel);
        // }
    }
}