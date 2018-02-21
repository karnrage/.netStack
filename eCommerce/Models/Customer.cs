using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace eCommerce.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        //if you don't call it by the class name and ID, you willneed to designate
        // it that it is the key
        public int CustomerID { get; set; }
         
  
        // public Customer customer { get; set; }
        // // above (lowercase) customer is an object of (uppercase) class of Customer
        public List<Order> Orders {get; set;}
        
        [Required]
		[MinLength(3)]
        public string Name {get; set;}     		


        // [Required]
        // public DateTime CreatedDate { get; set; }  

        public Customer()
        {
            Orders = new List<Order>();
        }
  
    }
}
// do not need the below because there is no relationship between the customer and product. The order creates/is the relationship between those two classes
        // public Product product { get; set; }       
        // // above (lowercase) product is an object of (uppercase) class of Product


        // public int ProductID { get; set; }