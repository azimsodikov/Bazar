using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bazar.Models
{
    // Product Ratings class
    public class ProductRatings
    {
        [Key]
        public int ProductRatingsID {get; set;}

        public ICollection <Products> Products {get; set;}

        public ICollection <ApplicationUser> ApplicationUser {get; set;}

        public int Rating { get; set; }
    }
}
