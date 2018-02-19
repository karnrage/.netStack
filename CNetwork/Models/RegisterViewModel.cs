using System.ComponentModel.DataAnnotations;
using System;

namespace CNetwork.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required(ErrorMessage = "Enter a first name")]
        [MinLength(2, ErrorMessage = "First name must be at least two characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter a last name")]
        [MinLength(2, ErrorMessage = "Last name must be at least two characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter an email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [MinLength(8, ErrorMessage = "Password cannot be less than eight characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Enter a Description")]
        [MinLength(8, ErrorMessage = "Description needs to be more than 4 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}