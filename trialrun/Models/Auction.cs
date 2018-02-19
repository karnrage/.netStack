using System;
using System.ComponentModel.DataAnnotations;
// added below to be able to use lists
using System.Collections.Generic; 

namespace trialrun.Models

//variable name must be exact match to table column
{
    //need to use three objects, one of what was entered, a reg and login depending on usage
    public class Auction
    {	
        [Key]
        //if you don't call it by the class name and ID, you willneed to designate
        // it that it is the key
        public int AuctionID { get; set; }
    
// foreign key
        public int UserID {get; set;}
// object
        public User user {get; set;}
// foreign key
        public int ProductID {get; set;}
// object
        public Product product {get; set;}


    }
}