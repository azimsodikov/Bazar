using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bazar.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Address
    {
        [Key]
        public int AddressID {get; set;}

        [Required]
        [StringLength(25, ErrorMessage = "Street can not be more than 25 charachters")]
        public string Street {get; set;}

        [Required]
        [StringLength(25, ErrorMessage = "City can not be more than 25 charachters")]
        public string City {get; set;}

        [Required]
        [StringLength(25, ErrorMessage = "State can not be more than 25 charachters")]
        public string State {get; set;}

        [Required]
        [StringLength(25, ErrorMessage = "Zip can not be more than 25 charachters")]
        public string Zip {get; set;}

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int? Phone { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

    }
}
