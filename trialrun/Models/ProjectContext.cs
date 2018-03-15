using Microsoft.EntityFrameworkCore;

namespace trialrun.Models
{
    public class TrialrunContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public TrialrunContext(DbContextOptions<TrialrunContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        // public DbSet<Auction> Auctions { get; set; }
    }
}
