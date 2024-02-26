using FutureCodersWebApi.Models;
using FutureCodersWebApi.Repositories.Config;
using Microsoft.EntityFrameworkCore;

namespace FutureCodersWebApi.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) :
            base(options)
        {

        }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfig());
        }
    }
}
