﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyFitness.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ProfileImg { get; set; }

        [Required]
        public double CurrentWeight { get; set; }

        [Required]
        public double GoalWeight { get; set; }

        [Required]
        public int HeightFeet { get; set; }

        [Required]
        public int HeightInches { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }

        public ApplicationUser()
        {
            this.ProfileImg = "/images/egg.png";
        }
    }
}
