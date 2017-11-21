using System;
using System.ComponentModel.DataAnnotations;
// added below to be able to use lists
using System.Collections.Generic;

namespace weddingPlanner.Models

//variable name must be exact match to table column
{
    //need to use three objects, one of what was entered, a reg and login depending on usage
    public class user : BaseEntity
    {
        [Key]
        //if you don't call it by the class name and ID, you willneed to designate
        // it that it is the key
        public int userID { get; set; }
        //below in wrapper you can declare the entry form requirements. here only just variables
        // [Required]
        //
        // [MinLength(3)]
        public string firstName { get; set; }

        // [Required]
        // [MinLength(3)]
        public string lastName { get; set; }

        // [Required]

        public string email { get; set; }

        // [Required]
        // [MinLength(8)]
        // [DataType(DataType.Password)]
        public string password { get; set; }


        // [Required]
        public DateTime createAt { get; set; }

        //first <here name has to be the same as other object> next (after <>) can be whatever I want

        public List<wedding> weddings { get; set; }
        //---------------constructor below to not throw error of db retreval is blank; created one-to-many relationship
        public List<guest> guests { get; set; }
        public user()
        {
            weddings = new List<wedding>();
            guests = new List<guest>();
        }


        //---------------constructor below to not throw error of db retreval is blank; created one-to-many relationship



    }

    //need to use three objects, one of what was entered, a reg and login depending on usage
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }
        
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }

    //need to use three objects, one of what was entered, a reg and login depending on usage
    public class RegisterViewModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "F.Name can only contain letters")]
        [Display(Name = "F.Name")]
        public string firstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "L.Name can only contain letters")]
        [Display(Name = "L.Name")]
        public string lastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        
        [Required]
        [Compare("password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        public string confirmRegPassword { get; set; }
    }

    //This is a WRAPPER!
    public class LoginRegViewModel
    {//first is type of variable in greenm, second is actual name to use in form
        public LoginViewModel loginVM { get; set; }
        public RegisterViewModel registerVM { get; set; }
    }
}