using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { Id = "a1", FirstName = "Ali Emre", LastName = "Yeşilyurt", IsAvailable = false, GenderId = 1, RankId = 1 }
            );
        }
    }
}
