using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRaterMVC.Models;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services
{
    public interface IRestaurantService
    {
        Task<bool> CreateRestaurant(RestaurantCreate model);
        Task<List<RestaurantListItem>> GetAllRestaurants();
        Task<RestaurantDetail> GetRestaurantById(int id);
        Task<bool> UpdateRestaurant(RestaurantEdit model);
        Task<bool> DeleteRestaurant(int id);
    }
}