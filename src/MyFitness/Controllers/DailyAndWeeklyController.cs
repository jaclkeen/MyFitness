using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MyFitness.Models;
using MyFitness.Data;

namespace MyFitness.Controllers
{
    public class DailyAndWeeklyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext context;

        public DailyAndWeeklyController(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx)
        {
            context = ctx;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        public async Task<int[]> CaloriesEatenAndRemaining()
        {
            int[] Calories = new int[2];

            ApplicationUser CurrentUser = await GetCurrentUserAsync();
            DailyNutrition n = context.DailyNutrition.Where(dn => dn.DailyNutritionDate == DateTime.Today && dn.User == CurrentUser).SingleOrDefault();
            int CaloriesEaten = context.Foods.Where(f => f.DailyNutritionId == n.DailyNutritionId).ToList().Sum(c => c.Calories);

            Calories[0] = CaloriesEaten;
            Calories[1] = Convert.ToInt16(n.TotalCaloriesRemaining) - CaloriesEaten;

            return Calories;
        }

        [HttpGet]
        public async Task<double[]> NutritionInformation()
        {
            double[] NInfo = new double[4];
            ApplicationUser CurrentUser = await GetCurrentUserAsync();
            DailyNutrition n = context.DailyNutrition.Where(dn => dn.DailyNutritionDate == DateTime.Today && dn.User == CurrentUser).SingleOrDefault();
            NInfo[0] = context.Foods.Where(f => f.DailyNutritionId == n.DailyNutritionId).ToList().Sum(c => c.Calories);
            NInfo[1] = Math.Round(context.Foods.Where(f => f.DailyNutritionId == n.DailyNutritionId).ToList().Sum(c => c.FoodProtein * 4));
            NInfo[2] = Math.Round(context.Foods.Where(f => f.DailyNutritionId == n.DailyNutritionId).ToList().Sum(c => c.FoodCarbs * 4));
            NInfo[3] = Math.Round(context.Foods.Where(f => f.DailyNutritionId == n.DailyNutritionId).ToList().Sum(c => c.FoodFat * 9));

            return NInfo;
        }

        [HttpPost]
        public async Task<int[,]> CaloriesConsumedInDateRange([FromBody] int DayRange)
        {
            int[,] Values = new int[3, DayRange];
            DateTime Today = DateTime.Today;
            DateTime Then = Today.AddDays(-DayRange);
            ApplicationUser CurrentUser = await GetCurrentUserAsync();

            List<double> DailyCaloricAllowance = (from nd in context.DailyNutrition
                                                  where nd.DailyNutritionDate <= Today && nd.DailyNutritionDate >= Then && nd.User == CurrentUser
                                                  select nd.StartingCaloriesToday).ToList();

            List<int> CaloriesBurned = (from nd in context.DailyNutrition
                                        where nd.DailyNutritionDate <= Today && nd.DailyNutritionDate >= Then && nd.User == CurrentUser
                                        select nd.DailyExercises.Sum(exercises => exercises.CaloriesBurned)).ToList();

            List<int> Cals = (from nd in context.DailyNutrition
                              where nd.DailyNutritionDate <= Today && nd.DailyNutritionDate >= Then && nd.User == CurrentUser
                              select nd.DailyFoods.Sum(foods => foods.Calories)).ToList();

            DailyCaloricAllowance.Reverse();
            if (CaloriesBurned.Count < DayRange)
            {
                int MissingDays = DayRange - Cals.Count;

                for (int i = 0; i < MissingDays; i++)
                {
                    CaloriesBurned.Insert(i, 0);
                    Cals.Insert(i, 0);
                    DailyCaloricAllowance.Insert(i, 0);
                }
            }

            for (int i = 0; i < Cals.Count; i++)
            {
                Values[0, i] = Cals[i];
                Values[1, i] = Convert.ToInt16(DailyCaloricAllowance[i]);
                Values[2, i] = CaloriesBurned[i];
            }

            return Values;
        }

        [HttpPost]
        public async Task<double[,]> WeightLossInformationInRange([FromBody] int DateRange)
        {
            double[,] WWLArray = new double[4, DateRange];
            ApplicationUser CurrentUser = await GetCurrentUserAsync();

            List<double> WeeklyWeightLost = (from dn in context.DailyNutrition
                                             where dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-DateRange) && dn.DailyNutritionDate <= DateTime.Today
                                             select dn.WeightLostToday).ToList();

            List<double> WeeklyCarbsEat = (from dn in context.DailyNutrition
                                           where dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-DateRange) && dn.DailyNutritionDate <= DateTime.Today
                                           select dn.DailyFoods.Sum(f => f.FoodCarbs)).ToList();

            List<double> WeeklyProteinEat = (from dn in context.DailyNutrition
                                             where dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-DateRange) && dn.DailyNutritionDate <= DateTime.Today
                                             select dn.DailyFoods.Sum(f => f.FoodProtein)).ToList();

            List<double> WeeklyFatsEat = (from dn in context.DailyNutrition
                                          where dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-DateRange) && dn.DailyNutritionDate <= DateTime.Today
                                          select dn.DailyFoods.Sum(f => f.FoodFat)).ToList();


            if (WeeklyWeightLost.Count < DateRange)
            {
                int MissingValues = DateRange - WeeklyWeightLost.Count;
                for (int i = 0; i < MissingValues; i++)
                {
                    WeeklyWeightLost.Insert(i, 0);
                    WeeklyCarbsEat.Insert(i, 0);
                    WeeklyProteinEat.Insert(i, 0);
                    WeeklyFatsEat.Insert(i, 0);
                }
            }

            for (int i = 0; i < DateRange; i++)
            {
                WWLArray[0, i] = WeeklyWeightLost[i];
                WWLArray[1, i] = WeeklyCarbsEat[i];
                WWLArray[2, i] = WeeklyProteinEat[i];
                WWLArray[3, i] = WeeklyFatsEat[i];
            }

            return WWLArray;
        }

        [HttpGet]
        public async Task<double[]> NutrientGrams()
        {
            double[] NutrientArray = new double[3];
            ApplicationUser CurrentUser = await GetCurrentUserAsync();
            DailyNutrition n = context.DailyNutrition.Where(dn => dn.DailyNutritionDate == DateTime.Today && dn.User == CurrentUser).SingleOrDefault();
            List<Foods> UserFoods = context.Foods.Where(f => f.DailyNutritionId == n.DailyNutritionId).ToList();

            NutrientArray[0] = UserFoods.Sum(uf => uf.FoodFat);
            NutrientArray[1] = UserFoods.Sum(uf => uf.FoodCarbs);
            NutrientArray[2] = UserFoods.Sum(uf => uf.FoodProtein);

            return NutrientArray;
        }

        [HttpPost]
        public async Task<double[]> CaloricPercentageInformationInDateRage([FromBody] int days)
        {
            double[] CalInfo = new double[3];
            double CarbTotal = 0;
            double ProteinTotal = 0;
            double FatTotal = 0;
            double Total = 0;
            ApplicationUser CurrentUser = await GetCurrentUserAsync();
            List<DailyNutrition> UserN = context.DailyNutrition.Where(dn => dn.DailyNutritionDate <= DateTime.Today && dn.DailyNutritionDate >= DateTime.Today.AddDays(-7) && dn.User == CurrentUser).ToList();
            UserN.ForEach(n => n.DailyFoods = context.Foods.Where(f => f.DailyNutritionId == n.DailyNutritionId).ToList());

            foreach (DailyNutrition n in UserN)
            {
                CarbTotal += n.DailyFoods.Sum(df => df.FoodCarbs);
                ProteinTotal += n.DailyFoods.Sum(df => df.FoodProtein);
                FatTotal += n.DailyFoods.Sum(df => df.FoodFat);
                Total += n.DailyFoods.Sum(df => df.FoodCarbs) + n.DailyFoods.Sum(df => df.FoodProtein) + n.DailyFoods.Sum(df => df.FoodFat);
            }

            CalInfo[0] = Math.Round(((CarbTotal / Total) * 100));
            CalInfo[1] = Math.Round(((ProteinTotal / Total) * 100));
            CalInfo[2] = Math.Round(((FatTotal / Total) * 100));

            return CalInfo;
        }

        [HttpPost]
        public async Task<List<int>> CaloriesBurnedInTimeRange([FromBody] int TimeRange)
        {
            List<int> CaloriesBurned = new List<int> { };
            ApplicationUser CurrentUser = await GetCurrentUserAsync();
            List<DailyNutrition> n = context.DailyNutrition.Where(dn => dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-7) && dn.DailyNutritionDate <= DateTime.Today).ToList();
            n.ForEach(dn => dn.DailyExercises = context.Exercise.Where(e => e.DailyNutritionId == dn.DailyNutritionId).ToList());
            n.ForEach(dn => CaloriesBurned.Add(dn.DailyExercises.Sum(e => e.CaloriesBurned)));

            if (CaloriesBurned.Count < TimeRange)
            {
                int ValsMissing = TimeRange - CaloriesBurned.Count;
                for (int i = 0; i < ValsMissing; i++)
                {
                    CaloriesBurned.Insert(i, 0);
                }
            }

            return CaloriesBurned;
        }

        [HttpPost]
        public async Task<int[,]> StartingDailyCalorieInformation([FromBody] int TimeRange)
        {
            int[,] CalorieInformation = new int[2, TimeRange];
            ApplicationUser CurrentUser = await GetCurrentUserAsync();

            List<double> DailyStartingCalories = context.DailyNutrition.Where(dn => dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-TimeRange) && dn.DailyNutritionDate <= DateTime.Today).Select(c => c.StartingCaloriesToday).ToList();
            List<int> DailyCaloriesEaten = new List<int> { };

            List<DailyNutrition> nutrition = context.DailyNutrition.Where(dn => dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-TimeRange) && dn.DailyNutritionDate <= DateTime.Today).ToList();
            nutrition.ForEach(dn => dn.DailyFoods = context.Foods.Where(f => f.DailyNutritionId == dn.DailyNutritionId).ToList());
            nutrition.ForEach(n => DailyCaloriesEaten.Add(n.DailyFoods.Sum(f => f.Calories)));

            if (DailyStartingCalories.Count < TimeRange)
            {
                int ValsMissing = TimeRange - DailyStartingCalories.Count;
                for (int i = 0; i < ValsMissing; i++)
                {
                    DailyStartingCalories.Insert(i, 0);
                    DailyCaloriesEaten.Insert(i, 0);
                }
            }

            for (var i = 0; i < TimeRange; i++)
            {
                CalorieInformation[0, i] = Convert.ToInt16(DailyStartingCalories[i]);
                CalorieInformation[1, i] = DailyCaloriesEaten[i];
            }

            return CalorieInformation;
        }
    }
}