using System;
using System.ComponentModel.DataAnnotations;

namespace RESTauranter.Models
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
        public DateTime date_created { get; set; }
        // [Required]
        //     public DateTime date_modified { get; set; }


    }
        
}