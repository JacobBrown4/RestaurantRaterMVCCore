using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Services;

namespace RestaurantRaterMVC.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RestaurantCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.CreateRestaurant(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var restaurants = await _service.GetAllRestaurants();
            return View(restaurants);
        }
        public async Task<IActionResult> Details(int id){
            RestaurantDetail restaurant = await _service.GetRestaurantById(id);
            if(restaurant == null)
                return RedirectToAction(nameof(Index));
                
            return View(restaurant);
        }
        public async Task<IActionResult> Delete(int id)
        {
            RestaurantDetail restaurant = await _service.GetRestaurantById(id);
            if (restaurant == null)
                return RedirectToAction(nameof(Index));

            return View(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, RestaurantDetail model)
        {
            bool wasDeleted = await _service.DeleteRestaurant(model.Id);
            if (wasDeleted)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

    }
}