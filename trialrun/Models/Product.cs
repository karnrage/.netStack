using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace trialrun.Models
{
    public class Product : BaseEntity
    {
        [Key]
        //if you don't call it by the class name and ID, you willneed to designate
        // it that it is the key
        public int ProductID { get; set; }

        [Required]
        [Range (1,100000000, ErrorMessage = "Bid value for {0} must be between {1} and {2}.")]
        [Display(Name = "Bid")]
        public int bid { get; set; }

        [MinLength(3)]
        [Display(Name = "Product Name")]
        public string name { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Range(typeof(DateTime), "11/22/2017", "01/01/3000",
        ErrorMessage = "Date for {0} must be between {1} and {2}")]
        public DateTime endDate { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }


        public int UserID { get; set; }
        public User user { get; set; }

        // public List<User> users { get; set; }

        // public Product()
        // //above needs to exactly class name
        // {
        //     users = new List<User>();
        // }
        
    }
}
