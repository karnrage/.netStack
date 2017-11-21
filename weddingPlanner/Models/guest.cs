	using System;
	using System.ComponentModel.DataAnnotations;
    // added below to be able to use lists
    using System.Collections.Generic; 

    namespace weddingPlanner.Models

//variable name must be exact match to table column
	{
        //need to use three objects, one of what was entered, a reg and login depending on usage
        public class guest : BaseEntity
    	{	
		    [Key]
            //if you don't call it by the class name and ID, you willneed to designate
            // it that it is the key
        	public int guestId { get; set; }
      
// foreign key
            public int userID {get; set;}
// object
            public user user {get; set;}
// foreign key
            public int wedID {get; set;}
// object
            public wedding wed {get; set;}


        }
    }