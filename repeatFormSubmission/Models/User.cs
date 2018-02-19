using System.ComponentModel.DataAnnotations;

namespace repeatFormSubmission.Models
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
        
        [MinLength(4)]
        public string comments { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}