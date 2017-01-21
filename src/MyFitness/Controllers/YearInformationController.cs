using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFitness.Models;
using Microsoft.AspNetCore.Identity;
using MyFitness.Data;

namespace MyFitness.Controllers
{
    public class YearInformationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext context;

        public YearInformationController(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx)
        {
            context = ctx;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        public async Task<double[]> WeightLostBreakdown()
        {
            double[] NutritionInformation = new double[12];
            ApplicationUser CurrentUser = await GetCurrentUserAsync();

            for(int i = 1; i < 12; i++)
            {
                List<DailyNutrition> MonthNutritions = context.DailyNutrition.Where(dn => dn.DailyNutritionDate.Month == i && dn.User == CurrentUser).ToList();
                NutritionInformation[i - 1] = MonthNutritions.Sum(mn => mn.WeightLostToday);
            }

            return NutritionInformation;
        }

        [HttpGet]
        public async Task<double[,]> MacronutrientBreakdown()
        {
            double[,] NutritionInformation = new double[12, 4];
            ApplicationUser CurrentUser = await GetCurrentUserAsync();

            for(int i = 1; i <= 12; i++)
            {
                List<DailyNutrition> MonthNutritions = context.DailyNutrition.Where(dn => dn.DailyNutritionDate.Month == i && dn.User == CurrentUser).ToList();
                MonthNutritions.ForEach(mn => mn.DailyFoods = context.Foods.Where(f => f.DailyNutritionId == mn.DailyNutritionId).ToList());

                if (MonthNutritions.Count > 0)
                {
                    NutritionInformation[i - 1, 0] = (MonthNutritions.Sum(mn => mn.DailyFoods.Sum(f => f.Calories)) / MonthNutritions.Count);
                    NutritionInformation[i - 1, 1] = Math.Round((MonthNutritions.Sum(mn => mn.DailyFoods.Sum(f => f.FoodFat)) / MonthNutritions.Count) * 9);
                    NutritionInformation[i - 1, 2] = Math.Round((MonthNutritions.Sum(mn => mn.DailyFoods.Sum(f => f.FoodCarbs)) / MonthNutritions.Count) * 4);
                    NutritionInformation[i - 1, 3] = Math.Round((MonthNutritions.Sum(mn => mn.DailyFoods.Sum(f => f.FoodProtein)) / MonthNutritions.Count) * 4);
                }
                else
                {
                    NutritionInformation[i - 1, 0] = 0;
                    NutritionInformation[i - 1, 1] = 0;
                    NutritionInformation[i - 1, 2] = 0;
                    NutritionInformation[i - 1, 3] = 0;
                }
            }

            return NutritionInformation;
        }
    }
}