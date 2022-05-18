using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Kim_Angela_Eonjeong_HW4.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public String LastName { get; set; }

    }
};
