using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace trialrun.Models
{
    public class User : BaseEntity
    {
        [Key]
        //if you don't call it by the class name and ID, you willneed to designate
        // it that it is the key
        public int UserID { get; set; }
        //below in wrapper you can declare the entry form requirements. here only just variables

        public string firstName { get; set; }


        public string lastName { get; set; }



        // public string email { get; set; }
        public string username { get; set; }

        public int balance { get; set; }


        public string password { get; set; }


        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        //first <here name has to be the same as other object> next (after <>) can be whatever I want

        public List<Product> products { get; set; }
        // public List<Auction> auctions { get; set; }

        
        //---------------constructor below to not throw error of db retreval is blank; created one-to-many relationship
        
        public User()
        //above needs to exactly class name
        {
            products = new List<Product>();
            // auctions = new List<Auction>();
        }

    }
    
    //need to use three objects, one of what was entered, a reg and login depending on usage
    public class LoginViewModel
    {
        [Required]
        // [EmailAddress]
        [Display(Name = "username")]
        public string username { get; set; }
        
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
        [MinLength(3)]
        [MaxLength(20)]
        // [EmailAddress]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "U.Name can only contain letters")]
        [Display(Name = "Username")]
        public string username { get; set; }

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