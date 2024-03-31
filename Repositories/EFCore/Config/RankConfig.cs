using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class RankConfig : IEntityTypeConfiguration<Rank>
    {
        public void Configure(EntityTypeBuilder<Rank> builder)
        {
            builder.HasData(
                new Rank { Id = 1, Name = "Beta", Status = true },
                new Rank { Id = 2, Name = "Alfa", Status = true }
            );
        }
    }
}
