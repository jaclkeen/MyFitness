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
        public async Task<int[,]> YearlyNutritionInfo()
        {
            int[,] NutritionInformation = new int[2, 12];
            ApplicationUser CurrentUser = await GetCurrentUserAsync();

            return NutritionInformation;
        }
    }
}