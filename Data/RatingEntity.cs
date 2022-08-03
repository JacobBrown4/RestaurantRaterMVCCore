using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Data
{
    public class RatingEntity
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public double FoodScore { get; set; }
        public double CleanlinessScore { get; set; }
        public double AtmosphereScore { get; set; }
        public virtual RestaurantEntity Restaurant { get; set; }
    }
}