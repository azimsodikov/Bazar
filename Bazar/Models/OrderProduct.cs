using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bazar.Models
{
    public class OrderProduct
    {
        [Key]
        public int OrderProductID {get; set;}

        [Required]
        public int OrderID {get; set;}
        public virtual Orders Order {get; set;}

        [Required]
        public int ProductID {get; set;}
        public virtual Products Product {get; set;}
    }
}
