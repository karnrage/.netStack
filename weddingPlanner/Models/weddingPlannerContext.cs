using Microsoft.EntityFrameworkCore;
 
	namespace weddingPlanner.Models
	{
    	public class weddingPlannerContext : DbContext
    		{
        	// base() calls the parent class' constructor passing the "options" parameter along
			// after public needs to be same as class name, do not use namesspace
        	public weddingPlannerContext(DbContextOptions<weddingPlannerContext> options) : base(options) { }
			//db<name of model object here> <name of table name in mySQL here>
            // public DbSet<user> users { get; set; }

            // NEED to create table name here

			//<model/object> class/table
			public DbSet<user> users { get; set; }

			//add tables. 

			public DbSet<wedding> weddings { get; set; }

			public DbSet<guest> guests {get; set;}


    		}
	}