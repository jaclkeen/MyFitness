using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFitness.Models.AppViewModels
{
    public class HomeIndexViewModel : BaseViewModel
    {
        public ApplicationUser CurrentUser { get; set; }

        public string Today { get; set; }

        public DailyNutrition TodayNutrition { get; set; }

        public int FatTotal { get; set; }

        public int CalorieTotal { get; set; }

        public int CarbTotal { get; set; }

        public int ProteinTotal { get; set; }

        public HomeIndexViewModel()
        {
            this.FatTotal = 0;
            this.CalorieTotal = 0;
            this.CarbTotal = 0;
            this.ProteinTotal = 0;
        }

        public string FormatTodayDate(DateTime today)
        {
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


            string DateString = $"{MonthName} {today.Day}, {today.Year}";

            return DateString;
        }
    }
}
