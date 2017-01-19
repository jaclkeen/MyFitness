using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFitness.Models;
using Microsoft.AspNetCore.Identity;
using MyFitness.Data;
using Microsoft.AspNetCore.Authorization;
using MyFitness.Models.AppViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyFitness.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext context;
        private IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment env, UserManager<ApplicationUser> userManager, ApplicationDbContext ctx)
        {
            _environment = env;
            context = ctx;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        public async Task<IActionResult> Index()
        {
            DateTime today = DateTime.Today;
            ApplicationUser CurrentUser = await GetCurrentUserAsync();

            HomeIndexViewModel model = new HomeIndexViewModel();
            model.CurrentUser = CurrentUser;
            model.Today = model.FormatTodayDate(today);

            DailyNutrition n = context.DailyNutrition.Where(dn => dn.DailyNutritionDate == today && dn.User == CurrentUser).SingleOrDefault();

            if(n == null)
            {
                int HInInches = (CurrentUser.HeightFeet * 12) + CurrentUser.HeightInches;

                double UserCalories = (CurrentUser.Gender == "Male") ? UserCalories = Convert.ToInt32((66.5 + (6.23 * CurrentUser.CurrentWeight) + (12.7 * HInInches) - (6.8 * CurrentUser.Age) * 1.2) - 250) :
                    UserCalories = Convert.ToInt32((655 + (4.35 * CurrentUser.CurrentWeight) + (4.7 * HInInches) - (4.7 * CurrentUser.Age) * 1.2) - 250);

                DailyNutrition NewNutrition = new DailyNutrition
                {
                    DailyNutritionDate = today,
                    TotalCaloriesRemaining = UserCalories,
                    User = CurrentUser,
                    StartingCaloriesToday = UserCalories,
                    WeightLostToday = 0
                };

                context.Add(NewNutrition);
                context.SaveChanges();

                n = NewNutrition;
            }

            //USED TO GET TOTALS OF FOOD NUTRITION
            n.DailyFoods = context.Foods.Where(f => f.DailyNutritionId == n.DailyNutritionId).ToList();
            n.DailyFoods.ForEach(f => n.TotalCaloriesRemaining -= f.Calories);
            model.CalorieTotal = n.DailyFoods.Sum(df => df.Calories);
            model.CarbTotal = n.DailyFoods.Sum(df => df.FoodCarbs);
            model.FatTotal = n.DailyFoods.Sum(df => df.FoodFat);
            model.ProteinTotal = n.DailyFoods.Sum(df => df.FoodProtein);

            //USED TO GET DAILY EXERCISE TOTALS
            n.DailyExercises = context.Exercise.Where(e => e.DailyNutritionId == n.DailyNutritionId).ToList();
            model.CaloriesBurnedTotal = n.DailyExercises.Sum(de => de.CaloriesBurned);
            model.ExerciseLengthInHoursTotal = n.DailyExercises.Sum(de => de.ExerciseLengthInHours);
            model.RepsTotal = n.DailyExercises.Sum(de => de.Reps);
            model.SetsTotal = n.DailyExercises.Sum(de => de.Sets);
            model.DistanceTraveledTotal = n.DailyExercises.Sum(de => de.DistanceTraveled);
            model.WeightLiftedTotal = n.DailyExercises.Sum(de => de.WeightLifted);
            model.ExerciseTypeTotal = n.DailyExercises.Count();

            //GET WEIGHT LOST VALUES
            model.TotalWeightLost = context.DailyNutrition.Where(dn => dn.User == CurrentUser).ToList().Sum(wl => wl.WeightLostToday);
            model.YearlyWeightLost = context.DailyNutrition.Where(dn => dn.User == CurrentUser && dn.DailyNutritionDate.Year == today.Year).ToList().Sum(wl => wl.WeightLostToday);
            model.MonthlyWeightLost = context.DailyNutrition.Where(dn => dn.User == CurrentUser && dn.DailyNutritionDate.Month == today.Month).ToList().Sum(wl => wl.WeightLostToday);
            model.WeeklyWeightLost = context.DailyNutrition.Where(dn => dn.User == CurrentUser && dn.DailyNutritionDate >= today.AddDays(-7) && dn.DailyNutritionDate <= today).ToList().Sum(wl => wl.WeightLostToday);

            model.TodayNutrition = n;
            return View(model);
        }

        public async Task<IActionResult> ChangeProfileImage(IFormFile ProfileImg)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "images");
            ApplicationUser CurrentDbUser = await GetCurrentUserAsync();
            
            if (ProfileImg != null && ProfileImg.ContentType.Contains("image"))
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, ProfileImg.FileName), FileMode.Create))
                {
                    await ProfileImg.CopyToAsync(fileStream);
                    CurrentDbUser.ProfileImg = $"/images/{ProfileImg.FileName}";
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddExercise(Exercise ExerciseBeingAdded)
        {
            DateTime today = DateTime.Today;
            ApplicationUser CurrentUser = await GetCurrentUserAsync();
            DailyNutrition n = context.DailyNutrition.Where(dn => dn.DailyNutritionDate == today && dn.User == CurrentUser).SingleOrDefault();

            n.TotalCaloriesRemaining += ExerciseBeingAdded.CaloriesBurned;
            ExerciseBeingAdded.User = CurrentUser;
            ExerciseBeingAdded.DailyNutritionId = n.DailyNutritionId;

            if (ModelState.IsValid)
            {
                context.Exercise.Add(ExerciseBeingAdded);
            }

            try
            {
                await context.SaveChangesAsync();
            }

            catch
            {
                throw;
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddEatenFoods(Foods FoodBeingAdded)
        {
            DateTime today = DateTime.Today;
            ApplicationUser CurrentUser = await GetCurrentUserAsync();
            DailyNutrition n = context.DailyNutrition.Where(dn => dn.DailyNutritionDate == today && dn.User == CurrentUser).SingleOrDefault();

            Foods NewEatenFood = new Foods
            {
                Calories = FoodBeingAdded.Calories * FoodBeingAdded.Servings,
                FoodFat = FoodBeingAdded.FoodFat * FoodBeingAdded.Servings,
                FoodCarbs = FoodBeingAdded.FoodCarbs * FoodBeingAdded.Servings,
                FoodProtein = FoodBeingAdded.FoodProtein * FoodBeingAdded.Servings,
                FoodName = FoodBeingAdded.FoodName,
                DateEaten = today,
                Servings = FoodBeingAdded.Servings,
                DailyNutritionId = n.DailyNutritionId,
                User = CurrentUser,
            };
            context.Foods.Add(NewEatenFood);

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }


            return RedirectToAction("Index");
        }

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
                }
            }

            if (Cals.Count < DayRange)
            {
                int MissingDays = DayRange - Cals.Count;

                for(int i = 0; i < MissingDays; i++)
                {
                    Cals.Insert(i, 0);
                }
            }

            if(DailyCaloricAllowance.Count < DayRange)
            {
                int MissingDays = DayRange - DailyCaloricAllowance.Count;

                for (int i = 0; i < MissingDays; i++)
                {
                    DailyCaloricAllowance.Insert(i, 0);
                }
            }

            for(int i = 0; i < Cals.Count; i++)
            {
                Values[0, i] = Cals[i];
                Values[1, i] = Convert.ToInt16(DailyCaloricAllowance[i]);
                Values[2, i] = CaloriesBurned[i];
            }

            return Values;
        }

        [HttpGet]
        public async Task<double[,]> WeeklyGoalWeightLossInformation()
        {
            double[,] WWLArray = new double[4, 7];
            ApplicationUser CurrentUser = await GetCurrentUserAsync();

            List<double> WeeklyWeightLost = (from dn in context.DailyNutrition
                                             where dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-7) && dn.DailyNutritionDate <= DateTime.Today
                                             select dn.WeightLostToday).ToList();

            List<double> WeeklyCarbsConsumed = (from dn in context.DailyNutrition
                                                where dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-7) && dn.DailyNutritionDate <= DateTime.Today
                                                select dn.DailyFoods.Sum(f => f.FoodCarbs)).ToList();

            List<double> WeeklyFatsConsumed = (from dn in context.DailyNutrition
                                                where dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-7) && dn.DailyNutritionDate <= DateTime.Today
                                                select dn.DailyFoods.Sum(f => f.FoodFat)).ToList();

            List<double> WeeklyProteinConsumed = (from dn in context.DailyNutrition
                                                where dn.User == CurrentUser && dn.DailyNutritionDate >= DateTime.Today.AddDays(-7) && dn.DailyNutritionDate <= DateTime.Today
                                                select dn.DailyFoods.Sum(f => f.FoodProtein)).ToList();

            if (WeeklyWeightLost.Count < 7)
            {
                int MissingValues = 7 - WeeklyWeightLost.Count;
                for(int i = 0; i < MissingValues; i++)
                {
                    WeeklyWeightLost.Insert(i, 0);
                    WeeklyCarbsConsumed.Insert(i, 0);
                    WeeklyFatsConsumed.Insert(i, 0);
                    WeeklyProteinConsumed.Insert(i, 0);
                }
            }

            for(int i = 0; i < 7; i++)
            {
                WWLArray[0, i] = WeeklyWeightLost[i];
                WWLArray[1, i] = WeeklyFatsConsumed[i];
                WWLArray[2, i] = WeeklyProteinConsumed[i];
                WWLArray[3, i] = WeeklyCarbsConsumed[i];
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

        [HttpPatch]
        public async Task UserHeight([FromBody] EditUserInformation model)
        {
            ApplicationUser CurrentUser = await GetCurrentUserAsync();
            CurrentUser.HeightFeet = model.feet;
            CurrentUser.HeightInches = model.inches;

            var result = await _userManager.UpdateAsync(CurrentUser);
        }

        [HttpPatch]
        public async Task UserInformation([FromBody] EditUserInformation model)
        {
            ApplicationUser CurrentUser = await GetCurrentUserAsync();
            DailyNutrition n = context.DailyNutrition.Where(dn => dn.DailyNutritionDate == DateTime.Today && dn.User == CurrentUser).SingleOrDefault();

            if (model.EditType == "CurrentWeight")
            {
                n.WeightLostToday += CurrentUser.CurrentWeight - model.CurrentWeight;
                CurrentUser.CurrentWeight = model.CurrentWeight;
            }
            else if(model.EditType == "GoalWeight")
            {
                CurrentUser.GoalWeight = Convert.ToInt16(model.GoalWeight);
            }
            else
            {
                CurrentUser.Age = Convert.ToInt16(model.Age);
            }

            await context.SaveChangesAsync();
            var result = await _userManager.UpdateAsync(CurrentUser);
        }
    }
}
