using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services
{
    public class RestaurantService : IRestaurantService
    {
        private RestaurantDbContext _context;
        public RestaurantService(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateRestaurant(RestaurantCreate model)
        {
            RestaurantEntity restaurant = new RestaurantEntity()
            {
                Name = model.Name,
                Location = model.Location
            };
            _context.Restaurants.Add(restaurant);
            return await _context.SaveChangesAsync() == 1;
        }


        public async Task<List<RestaurantListItem>> GetAllRestaurants()
        {
            List<RestaurantListItem> restaurants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score
            }).ToListAsync();
            return restaurants;
        }

        public async Task<RestaurantDetail> GetRestaurantById(int id)
        {
            RestaurantEntity restaurant = await _context.Restaurants
            .Include(r => r.Ratings)
            .FirstOrDefaultAsync(r => r.Id == id);
            if (restaurant == null)
                return null;

            RestaurantDetail restaurantDetail = new RestaurantDetail()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                Score = restaurant.Score,
            };
            return restaurantDetail;
        }

        public async Task<bool> UpdateRestaurant(RestaurantEdit model)
        {
            RestaurantEntity restaurant = await _context.Restaurants.FindAsync(model.Id);
            if (restaurant == null)
                return false;
            restaurant.Location = model.Location;
            restaurant.Name = model.Name;

            return await _context.SaveChangesAsync() == 1;
        }
        public async Task<bool> DeleteRestaurant(int id)
        {
            RestaurantEntity restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
                return false;

            _context.Restaurants.Remove(restaurant);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}