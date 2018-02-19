using System;
using System.ComponentModel.DataAnnotations;
// above: for automatic validations of the form entries

namespace RepeatRestaurant.Models
{
    public class thisUserReview
    {
        [Key]
		public int id {get; set;}
		[Required]
		[MinLength(3)]
        public string reviewer_name { get; set; }
		[Required]
		[MinLength(3)]
        public string restaurant_name { get; set; }
		[Required]
        public DateTime visit_date { get; set; }
		[Required]
		[MinLength(5)]
        public string review { get; set; }
		[Required]
		[Range(1,5)]
        public int stars { get; set; }
        [Required]
        public DateTime created_at { get; set; }
        // [Required]
        //     public DateTime date_modified { get; set; }


    }
        
}