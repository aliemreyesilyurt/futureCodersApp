using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            //builder.Property(u => u.Surname).IsRequired().HasMaxLength(50);
            //builder.Property(u => u.BirthYear).IsRequired();
            //builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            //builder.Property(u => u.Password).IsRequired().HasMaxLength(255);
            //builder.Property(u => u.IsAvailable).IsRequired();
            //builder.Property(u => u.IsAdmin).IsRequired();

            //builder.HasOne(u => u.Gender)
            //       .WithMany(g => g.Users)
            //       .HasForeignKey(u => u.GenderId)
            //       .IsRequired();

            //builder.HasOne(u => u.Rank)
            //       .WithMany(r => r.Users)
            //       .HasForeignKey(u => u.RankId)
            //       .IsRequired();

            //builder.HasData(
            //    new User { Id = 1, Name = "John", Surname = "Doe", BirthYear = 1990, Email = "john.doe@example.com", Password = "hashedPassword", IsAvailable = true, IsAdmin = false, GenderId = 1, RankId = 1 },
            //    new User { Id = 2, Name = "Jane", Surname = "Smith", BirthYear = 1985, Email = "jane.smith@example.com", Password = "hashedPassword", IsAvailable = true, IsAdmin = true, GenderId = 2, RankId = 2 }
            //);
        }
    }
}
