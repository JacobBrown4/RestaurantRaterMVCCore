using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services
{
    public interface IRatingService
    {
        Task<bool> RateRestaurant(RatingCreate model);
        Task<List<RatingListItem>> GetAllRatings();
        Task<List<RatingListItem>> GetRatingsForRestaurant(int id);
        Task<bool> DeleteRating(int id);
    }
}