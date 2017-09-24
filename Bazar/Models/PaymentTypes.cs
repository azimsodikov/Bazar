using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bazar.Models
{
    // Add product type information inside below class
    public class PaymentTypes
    {
        [Key]
        public int PaymentTypesID {get; set;}

        [Required]
        [StringLength(25, ErrorMessage = "Payment type Name can not be more than 25 charachters")]
        public string Type {get; set;}

        [Required]
        [StringLength(20)]
        [Display(Name = "Account Number")]
        public string AccountNumber {get; set;}

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        public DateTime ExpDate {get; set;}


        public int SecurityCode {get; set;}

        public bool? IsActive {get; set;}

        [Required]
        public ApplicationUser User { get; set; }


        public ICollection<Orders> Orders { get; set; }

    }
}
