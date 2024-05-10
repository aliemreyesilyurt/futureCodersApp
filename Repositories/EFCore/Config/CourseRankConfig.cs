using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class CourseRankConfig : IEntityTypeConfiguration<CourseRank>
    {
        public void Configure(EntityTypeBuilder<CourseRank> builder)
        {
            builder.HasData(
                new CourseRank { Id = 1, CourseId = 1, RankId = 1 },
                new CourseRank { Id = 2, CourseId = 2, RankId = 1 },
                new CourseRank { Id = 3, CourseId = 2, RankId = 2 },
                new CourseRank { Id = 4, CourseId = 3, RankId = 1 },
                new CourseRank { Id = 5, CourseId = 3, RankId = 2 }
            );
        }
    }
}
