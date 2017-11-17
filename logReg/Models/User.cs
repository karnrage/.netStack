using System;
using System.ComponentModel.DataAnnotations;

namespace logReg.Models
{
    public class User
    {
        [Key]
		public int id {get; set;}

		[Required]
		[MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "First Name")]        
        public string first_name { get; set; }

		[Required]
		[MinLength(3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "Last Name")] 
        public string last_name { get; set; }

        [Required]
		[EmailAddress]
        public string email { get; set; }
		
		// [Required]
		// [MinLength(5)]
        // public EmailAddressAttribute email { get; set; }

        [Required]
		[MinLength(8)]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
		[MinLength(8)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string password_confirm { get; set; }
		
        // [Required]
        // public DateTime date_created { get; set; }

        // [Required]
        // public DateTime date_modified { get; set; }
        // [Required]
		// [Range(1,5)]
        // public int stars { get; set; }
        // [Required]
        // public DateTime visit_date { get; set; }

    }
        
}