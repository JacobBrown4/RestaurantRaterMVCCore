using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantRaterMVC.Models.Rating
{
    public class RatingCreate
    {
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        [Range(0,5)]
        public double FoodScore { get; set; }
        [Required]
        [Range(0,5)]
        public double AtmosphereScore { get; set; }
        [Required]
        [Range(0,5)]
        public double CleanlinessScore { get; set; }

        public IEnumerable<SelectListItem> RestaurantOptions {get; set;} = new List<SelectListItem>();
    }
}