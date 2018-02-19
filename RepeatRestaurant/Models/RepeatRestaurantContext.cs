using Microsoft.EntityFrameworkCore;
 
namespace RepeatRestaurant.Models
{
    public class RepeatRestaurantContext : DbContext
    //each table needs its own DbSet property
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public RepeatRestaurantContext(DbContextOptions<RepeatRestaurantContext> options) : base(options) { }
        public DbSet<thisUserReview> review_table { get; set; }
    }
}
