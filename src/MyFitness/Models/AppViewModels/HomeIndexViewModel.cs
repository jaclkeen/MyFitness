﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFitness.Models.AppViewModels
{
    public class HomeIndexViewModel : BaseViewModel
    {
        public ApplicationUser CurrentUser { get; set; }

        public Foods FoodBeingAdded { get; set; }

        public DailyNutrition TodayNutrition { get; set; }

        public Exercise ExerciseBeingAdded { get; set; }

        public List<DailyNutrition> WeeklyFoodInfo { get; set; }
        public List<DailyNutrition> MonthlyFoodInfo { get; set; }

        public List<double> YearlyCalCount { get; set; }
        public List<double> YearlyFatCount { get; set; }
        public List<double> YearlyProteinCount { get; set; }
        public List<double> YearlyCarbCount { get; set; }
        public List<string> MonthsForYearlyData { get; set; }


        public string Today { get; set; }
        public double FatTotal { get; set; }
        public int CalorieTotal { get; set; }
        public double CarbTotal { get; set; }
        public double ProteinTotal { get; set; }

        public double ExerciseLengthInHoursTotal { get; set; }
        public double CaloriesBurnedTotal { get; set; }
        public double DistanceTraveledTotal { get; set; }
        public int ExerciseTypeTotal { get; set; }
        public double WeightLiftedTotal { get; set; }
        public double SetsTotal { get; set; }
        public double RepsTotal { get; set; }

        public double TotalWeightLost { get; set; }
        public double YearlyWeightLost { get; set; }
        public double MonthlyWeightLost { get; set; }
        public double WeeklyWeightLost { get; set; }

        public HomeIndexViewModel()
        {
            this.FatTotal = 0;
            this.CalorieTotal = 0;
            this.CarbTotal = 0;
            this.ProteinTotal = 0;
            this.ExerciseTypeTotal = 0;
            this.ExerciseLengthInHoursTotal = 0;
            this.CaloriesBurnedTotal = 0;
            this.DistanceTraveledTotal = 0;
            this.WeightLiftedTotal = 0;
            this.SetsTotal = 0;
            this.RepsTotal = 0;
        }

        public string FormatTodayDate(DateTime today)
        {
            string DateString = "";
            string MonthName = (today.Month == 1) ? "January" :
                    MonthName = (today.Month == 2) ? "February" :
                    MonthName = (today.Month == 3) ? "March" :
                    MonthName = (today.Month == 4) ? "April" :
                    MonthName = (today.Month == 5) ? "May" :
                    MonthName = (today.Month == 6) ? "June" :
                    MonthName = (today.Month == 7) ? "July" :
                    MonthName = (today.Month == 8) ? "August" :
                    MonthName = (today.Month == 9) ? "September" :
                    MonthName = (today.Month == 10) ? "October" :
                    MonthName = (today.Month == 11) ? "November" :
                    MonthName = (today.Month == 12) ? "December" : null;

                DateString = $"{MonthName} {today.Day}, {today.Year}";

            return DateString;
        }
    }
}
