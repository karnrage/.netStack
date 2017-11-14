using Microsoft.EntityFrameworkCore;
 
	namespace bankAccounts.Models
	{
    	public class bankAccountContext : DbContext
    		{
        	// base() calls the parent class' constructor passing the "options" parameter along
			// after public needs to be same as class name, do not use namesspace
        	public bankAccountContext(DbContextOptions<bankAccountContext> options) : base(options) { }
			//db<name of model object here> <name of table name in mySQL here>
            public DbSet<user> users { get; set; }

			public DbSet<transactions> transactions { get; set; }
    		}
	}