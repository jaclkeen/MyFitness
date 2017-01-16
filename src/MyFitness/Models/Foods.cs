using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations;

namespace MyFitness.Models
{
    public class Foods
    {
        [Key]
        public int FoodId { get; set; }

        [Required]
        public string FoodName { get; set; }

        [Required]
        public int Calories { get; set; }

        [Required]
        public int Servings { get; set; }

        [Required]
        public DateTime DateEaten { get; set; }

        [Required]
        public int DailyNutritionId { get; set; }
        public DailyNutrition DailyNutrition { get; set; }

        [Required]
        public double FoodProtein { get; set; }

        [Required]
        public double FoodFat { get; set; }

        [Required]
        public double FoodCarbs { get; set; }

        public ApplicationUser User { get; set; }
    }
}
