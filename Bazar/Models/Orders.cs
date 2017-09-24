using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bazar.Models
{
    public class Orders
    {
        [Key]
        public int OrderID {get; set;}

        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated {get;set;}

        public ApplicationUser ApplicationUser {get; set;}

        [Display(Name = "Payment Type")]
        public int? PaymentTypesID {get; set;}

        public PaymentTypes PaymentTypes {get; set;}

        public int? AddressID { get; set; }

        public Address Address { get; set; }

        public ICollection <OrderProduct> OrderProduct {get; set;}
    }
}
