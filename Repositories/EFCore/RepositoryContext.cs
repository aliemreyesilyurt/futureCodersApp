using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User>
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RankConfig());
            //modelBuilder.ApplyConfiguration(new ReviewConfig());
            //modelBuilder.ApplyConfiguration(new CourseConfig());
            //modelBuilder.ApplyConfiguration(new CourseRankConfig());
            //modelBuilder.ApplyConfiguration(new StepConfig());
            modelBuilder.ApplyConfiguration(new GenderConfig());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            //modelBuilder.ApplyConfiguration(new UserConfig());
            //modelBuilder.ApplyConfiguration(new UserStepConfig());
            //modelBuilder.ApplyConfiguration(new BlogConfig());

            // bu kullanim sayesinde IEntityTypeConfiguration ifadesini kullanan ifadeleri dogrudan cagirir
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
