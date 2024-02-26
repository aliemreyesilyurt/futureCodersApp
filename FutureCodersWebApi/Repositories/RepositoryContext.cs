using FutureCodersWebApi.Models;
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
    }
}
