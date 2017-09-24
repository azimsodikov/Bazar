using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace Bazar.Models

{

    // Add profile data for application users by adding properties to the ApplicationUser class

    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int ApplicationUserID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "First Name can not be more than 25 charachters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25, ErrorMessage = "Last Name can not be more than 25 charachters")]
        public string LastName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }

        public virtual ICollection<PaymentTypes> PaymentTypes { get; set; }

        public virtual ICollection<Address> Address { get; set; }


    }

}

