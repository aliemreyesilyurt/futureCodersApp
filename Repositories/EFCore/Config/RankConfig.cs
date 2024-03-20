using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class RankConfig : IEntityTypeConfiguration<Rank>
    {
        public void Configure(EntityTypeBuilder<Rank> builder)
        {
            builder.Property(r => r.Name).IsRequired().HasMaxLength(50);

            builder.HasData(
                new Rank { Id = 1, Name = "Beginner" },
                new Rank { Id = 2, Name = "Intermediate" },
                new Rank { Id = 3, Name = "Advanced" }
            );
        }
    }
}
