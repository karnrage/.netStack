using System.ComponentModel.DataAnnotations;

namespace form_submission.Models
{
        
    public abstract class BaseEntity {}
  
    
    public class User:BaseEntity
    {
        [Required]
        [MinLength(4)]
        public string firstName { get; set; }
        [Required]
        [MinLength(4)]
        public string lastName { get; set; }
        [Range(0,150)]
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}