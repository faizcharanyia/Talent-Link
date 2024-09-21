using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

//TODO: Update this using statement to include your project name
using sp23Team09FinalProject.Models;

//TODO: Make this namespace match your project name
namespace sp23Team09FinalProject.DAL
{
    //NOTE: This class definition references the user class for this project.  
    //If your User class is called something other than AppUser, you will need
    //to change it in the line below
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //this code makes sure the database is re-created on the $5/month Azure tier
            builder.HasPerformanceLevel("Basic");
            builder.HasServiceTier("Basic");
            base.OnModelCreating(builder);
        }

        //TODO: Add Dbsets here.  Products is included as an example.  
        public DbSet<Application> Applications { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Industry> Industrys { get; set; }

        public DbSet<Global> CurrDate { get; set; }
    }
}
