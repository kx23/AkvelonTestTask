using Microsoft.EntityFrameworkCore;

namespace AkvelonTestTask.Models
{
    public class AppDbContext: DbContext
    {
        //DbContext defines the data context used to interact with the database
        public AppDbContext() 
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Tasks and Projects represent a set of entities stored in a database
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
