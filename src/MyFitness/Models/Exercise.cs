using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFitness.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required]
        public int DailyNutritionId { get; set; }

        [Required]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ExerciseType { get; set; }

        [Required]
        public double ExerciseLengthInHours { get; set; }

        [Required]
        public int CaloriesBurned { get; set; }

        public double DistanceTraveled { get; set; }

        public int WeightLifted { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }
    }
}
