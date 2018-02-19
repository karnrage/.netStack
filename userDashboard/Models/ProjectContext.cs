using Microsoft.EntityFrameworkCore;

namespace userDashboard.Models
{
    public class UserDashboardContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public UserDashboardContext(DbContextOptions<UserDashboardContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Network> Networks { get; set; }
    }
}
