using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RestaurantRaterMVC.Models.Rating;
using RestaurantRaterMVC.Services;

namespace RestaurantRaterMVC.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService _service;
        private readonly IRestaurantService _restaurantService;

        public RatingController(IRatingService service, IRestaurantService restaurantService)
        {
            _service = service;
            _restaurantService = restaurantService;
        }

        public async Task<IActionResult> Create()
        {
            var restaurants = await _restaurantService.GetAllRestaurants();
            IEnumerable<SelectListItem> restaurantOptions = restaurants.Select(r => new SelectListItem()
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToList();
            RatingCreate model = new RatingCreate();
            model.RestaurantOptions = restaurantOptions;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RatingCreate model)
        {
            if (!ModelState.IsValid) return View(ModelState);

            bool isRated = await _service.RateRestaurant(model);

            if (!isRated)
                return View(model);
            else
                return RedirectToAction(nameof(Restaurant), new { id = model.RestaurantId });
        }
        public async Task<IActionResult> Index()
        {
            var ratings = await _service.GetAllRatings();
            return View(ratings);
        }

        public async Task<IActionResult> Restaurant(int id)
        {
            var ratings = await _service.GetRatingsForRestaurant(id);
            return View(ratings);
        }
    }
}