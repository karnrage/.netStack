using System;
using System.ComponentModel.DataAnnotations;
// added below to be able to use lists
using System.Collections.Generic;

namespace weddingPlanner.Models

//variable name must be exact match to table column
{
    //need to use three objects, one of what was entered, a reg and login depending on usage
    public class wedding : BaseEntity
    {
        [Key]
        //if you don't call it by the class name and ID, you willneed to designate
        // it that it is the key
        public int wedID { get; set; }
        //below in wrapper you can declare the entry form requirements. here only just variables
        
        [Required]
        [MinLength(3)]
        [Display(Name = "Husband")]
        public string husband { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Wife")]
        public string wife { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Wedding Date")]
        public DateTime wedDate { get; set; }

        // public int guests { get; set; }



        // [Required]
        public DateTime createAt { get; set; }



//foreign key below
        public int userId { get; set; }
//entity framework below
        public user user { get; set; }
        
//need to create a list of guests, constructor so its not null
        public List<guest> guests { get; set; }
        public wedding()
        {
            guests = new List<guest>();
        }




    }
}