using Microsoft.EntityFrameworkCore;
 
namespace RESTauranter.Models
{
    public class RESTauranterContext : DbContext
    //each table needs its own DbSet property
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public RESTauranterContext(DbContextOptions<RESTauranterContext> options) : base(options) { }
        public DbSet<thisUserReview> review_table { get; set; }
    }
}
