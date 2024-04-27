using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasData(
            //    new User { Name = "Ali Emre", Surname = "Yeşilyurt", BirthYear = 2000, Email = "aliemreyesilyurt@hotmail.com", Password = "12345", IsAvailable = false, IsAdmin = true, GenderId = 1, RankId = 1 },
            //    new User { Name = "Özgür", Surname = "Altuner", BirthYear = 2000, Email = "ozguraltuner@hotmail.com", Password = "12345", IsAvailable = false, IsAdmin = false, GenderId = 1, RankId = 2 },
            //    new User { Name = "Doğa", Surname = "Özfırat", BirthYear = 2001, Email = "dogaozfirat@hotmail.com", Password = "12345", IsAvailable = false, IsAdmin = false, GenderId = 2, RankId = 2 }
            //);
        }
    }
}
