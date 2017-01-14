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

namespace MyFitness.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private ApplicationDbContext context;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx)
        {
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

            DailyNutrition n = context.DailyNutrition.Where(dn => dn.DailyNutritionDate == today).SingleOrDefault();

            if(n == null)
            {
                int HInInches = (CurrentUser.HeightFeet * 12) + CurrentUser.HeightInches;

                double UserCalories = (CurrentUser.Gender == "Male") ? UserCalories = Convert.ToInt32((66.5 + (6.23 * CurrentUser.CurrentWeight) + (12.7 * HInInches) - (6.8 * CurrentUser.Age) * 1.2) - 250) :
                    UserCalories = Convert.ToInt32((655 + (4.35 * CurrentUser.CurrentWeight) + (4.7 * HInInches) - (4.7 * CurrentUser.Age) * 1.2) - 250);

                DailyNutrition NewNutrition = new DailyNutrition
                {
                    DailyNutritionDate = today,
                    TotalCaloriesRemaining = UserCalories,
                    User = CurrentUser
                };

                context.Add(NewNutrition);
                context.SaveChanges();

                model.TodayNutrition = NewNutrition;
                return View(model);
            }

            model.TodayNutrition = n;
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
