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
        public Customer()
        {
            Orders = new List<Order>();
        }
        
        [Required]
		[MinLength(3)]
        public string Name {get; set;}
        public List<Order> Orders {get; set;}

        [Required]
		[MinLength(3)]
        public Product product { get; set; }       
        // above (lowercase) product is an object of (uppercase) class of Product


        public int ProductID { get; set; }
		
		[Required]
        public int Quantity { get; set; }

        // [Required]
        // public DateTime CreatedDate { get; set; }  

        [Required]
        public DateTime PurchaseDate { get; set; } 
    }
}
