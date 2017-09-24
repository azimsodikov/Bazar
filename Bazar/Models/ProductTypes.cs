using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bazar.Models
{
    // Add product type information inside below class
    public class ProductTypes
    {
        [Key]
        public int ProductTypeID {get; set;}

        [Required]
        [StringLength(25, ErrorMessage = "Product Name can not be more than 25 charachters")]
        [Display(Name = "Product Category")]
        public string CategoryName {get; set;}
    }
}
