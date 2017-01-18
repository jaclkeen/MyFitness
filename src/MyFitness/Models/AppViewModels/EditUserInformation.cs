using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyFitness.Models.AppViewModels
{
    public class EditUserInformation : BaseViewModel
    {
        public int inches { get; set; }
        public int feet { get; set; }
    }
}
