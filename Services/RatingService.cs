using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services
{
    public class RatingService : IRatingService
    {
        private readonly RestaurantDbContext _context;
        public RatingService(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RateRestaurant(RatingCreate model)
        {
            var rating = new RatingEntity(){
                FoodScore = model.FoodScore,
                AtmosphereScore = model.AtmosphereScore,
                CleanlinessScore = model.CleanlinessScore,
                RestaurantId = model.RestaurantId
            };

            _context.Ratings.Add(rating);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<List<RatingListItem>> GetAllRatings()
        {
            var ratings = _context.Ratings
            .Select(r=> new RatingListItem(){
                Id = r.Id,
                RestaurantName = r.Restaurant.Name,
                FoodScore = r.FoodScore,
                AtmosphereScore = r.AtmosphereScore,
                CleanlinessScore = r.CleanlinessScore,
            });

            return await ratings.ToListAsync();
        }

        public async Task<List<RatingListItem>> GetRatingsForRestaurant(int id)
        {
            var ratings = _context.Ratings
            .Where(r=>r.RestaurantId == id)
            .Select(r=> new RatingListItem(){
                Id = r.Id,
                RestaurantName = r.Restaurant.Name,
                FoodScore = r.FoodScore,
                AtmosphereScore = r.AtmosphereScore,
                CleanlinessScore = r.CleanlinessScore,
            });

            return await ratings.ToListAsync();
        }
        public Task<bool> DeleteRating(int id)
        {
            throw new NotImplementedException();
        }

    }
}