using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyFitness.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Relationship 
    {
        [Key]
        public int RelationshipId { get; set; }

        [Required]
        public int SendingUserId { get; set; }
        public ApplicationUser SendingUser { get; set; }

        [Required]
        public int RecievingUserId { get; set; }
        public ApplicationUser RecievingUser { get; set; }

        [Required]
        public int RelationshipStatus { get; set; }
        //Status types: 0 - Pending, 1 - Accepted, 2 - Blocked
    }
}
