using System.ComponentModel.DataAnnotations;
using System;

namespace CNetwork.Models
{
    public class LoginViewModel : BaseEntity
    {
        [Required(ErrorMessage = "Enter an email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [MinLength(8, ErrorMessage = "Password cannot be less than eight characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
    }
}
