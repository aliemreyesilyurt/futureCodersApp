using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EFCore
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) :
            base(options)
        {
        }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseRank> CourseRank { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswer { get; set; }
        public DbSet<QuestionOption> QuestionOption { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<Rank> Rank { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Step> Step { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserStep> UserStep { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CourseConfig());
            //modelBuilder.ApplyConfiguration(new UserConfig());
            //modelBuilder.ApplyConfiguration(new RankConfig());
            //modelBuilder.ApplyConfiguration(new GenderConfig());
        }
    }
}
