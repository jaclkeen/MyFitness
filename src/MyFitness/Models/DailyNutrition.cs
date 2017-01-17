using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFitness.Models
{
    public class DailyNutrition
    {
        [Key]
        public int DailyNutritionId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime DailyNutritionDate { get; set; }

        [Required]
        public double TotalCaloriesRemaining { get; set; }

        [Required]
        public double StartingCaloriesToday { get; set; }

        public List<Exercise> DailyExercises { get; set; }

        public List<Foods> DailyFoods { get; set; }
    }
}
