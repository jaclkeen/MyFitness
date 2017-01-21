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
            //FOR WEEKLY NUTRITION TABLE INFO
            model.WeeklyFoodInfo = (from dn in context.DailyNutrition
                                    join f in context.Foods on dn.DailyNutritionId equals f.DailyNutritionId
                                    where dn.User == CurrentUser && dn.DailyNutritionDate >= today.AddDays(-7) && dn.DailyNutritionDate <= today
                                    select dn).Distinct().ToList();
            model.WeeklyFoodInfo.ForEach(dn => dn.DailyFoods = context.Foods.Where(f => f.DailyNutritionId == dn.DailyNutritionId).ToList());

            //FOR MONTHLY NUTRITION TABLE INFO
            model.MonthlyFoodInfo = (from dn in context.DailyNutrition
                                    join f in context.Foods on dn.DailyNutritionId equals f.DailyNutritionId
                                    where dn.User == CurrentUser && dn.DailyNutritionDate >= today.AddDays(-30) && dn.DailyNutritionDate <= today
                                    select dn).Distinct().ToList();
            model.MonthlyFoodInfo.ForEach(dn => dn.DailyFoods = context.Foods.Where(f => f.DailyNutritionId == dn.DailyNutritionId).ToList());

            //FOR YEARLY NUTRITION TABLE INFO ORGANIZED BY EACH INDIVIDUAL MONTH
            model.YearlyCalCount = new List<double> { };
            model.YearlyCarbCount = new List<double> { };
            model.YearlyFatCount = new List<double> { };
            model.YearlyProteinCount = new List<double> { };
            model.MonthsForYearlyData = new List<string> { };

            for (int i = 1; i <= 12; i++)
            {
                double TotalCal = 0; double TotalFat = 0; double Protein = 0; double Carbs = 0; int Year = 0;

                List<DailyNutrition> MonthNutrition = context.DailyNutrition.Where(nut => nut.User == CurrentUser && nut.DailyNutritionDate.Month == i).ToList();

                foreach(DailyNutrition nu in MonthNutrition)
                {
                    nu.DailyExercises = context.Exercise.Where(e => e.DailyNutritionId == nu.DailyNutritionId).ToList();
                    nu.DailyFoods = context.Foods.Where(f => f.DailyNutritionId == nu.DailyNutritionId).ToList();

                    TotalCal += nu.DailyFoods.Sum(c => c.Calories) / MonthNutrition.Count;
                    TotalFat += nu.DailyFoods.Sum(f => f.FoodFat) / MonthNutrition.Count;
                    Protein += nu.DailyFoods.Sum(p => p.FoodProtein) / MonthNutrition.Count;
                    Carbs += nu.DailyFoods.Sum(c => c.FoodCarbs) / MonthNutrition.Count;
                    Year = nu.DailyNutritionDate.Year;
                }

                if (TotalCal != 0 && TotalFat != 0 && Protein != 0 && Carbs != 0)
                {
                    model.MonthsForYearlyData.Add(new DateTime(Year, i, 10).ToString("MMMM, yyyy"));
                    model.YearlyCalCount.Add(TotalCal);
                    model.YearlyCarbCount.Add(Carbs);
                    model.YearlyFatCount.Add(TotalFat);
                    model.YearlyProteinCount.Add(Protein);
                }
            }

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
            FoodBeingAdded.Servings = FoodBeingAdded.Servings == 0 ? 1 : FoodBeingAdded.Servings;
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
