using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public double Rating 
        { 
            get 
            {
                double totalAverageRating = 0;
                foreach(var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }
                return (Ratings.Count > 0) ? totalAverageRating / Ratings.Count : 0;
            }  
        }

        // Average Food Rating:

        public double AverageFoodScore
        {
            get
            {
                double totalFoodScore = 0;
                foreach (var rating in Ratings)
                {
                    totalFoodScore += rating.FoodScore;
                }
                return (Ratings.Count > 0) ? totalFoodScore / Ratings.Count : 0;
            }
        }

        //Average Environment Rating:

        public double AverageEnvironmentScore
        {
            get
            {
                IEnumerable<double> scores = Ratings.Select(rating => rating.EnvironmentScore); // Does the same type of operation as above.
                double totalEnvironmentScore = scores.Sum();
                return (Ratings.Count > 0) ? totalEnvironmentScore / Ratings.Count : 0;
            }
        }

        // Average Cleanliness Rating:

        public double AverageCleanlinessScore
        {
            get
            {
                    return (Ratings.Count() > 0) ? Ratings.Select(r => r.CleanlinessScore).Average() : 0; // Even a more streamlined version of above.
            }
        }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}