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
    }
}