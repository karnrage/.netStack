using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace eCommerce.Models
{
    public class Product : BaseEntity
    {
        [Key]
        //if you don't call it by the class name and ID, you willneed to designate
        // it that it is the key
        public int ProductID { get; set; }
         
        [Required]
		[MinLength(3)]
        public string ProductName { get; set; }

		
		[MinLength(3)]
        public string ImageLink { get; set; }

		[Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }  

        [Required]
        public DateTime PurchaseDate { get; set; } 
    }
}
