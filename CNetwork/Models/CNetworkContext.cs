using Microsoft.EntityFrameworkCore;

namespace CNetwork.Models
{
    public class CNetworkContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public CNetworkContext(DbContextOptions<CNetworkContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Friends> Friends { get; set; }
        public DbSet<Invite> Invite { get; set; }
    }
}