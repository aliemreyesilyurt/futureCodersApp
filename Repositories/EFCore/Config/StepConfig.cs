using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class StepConfig : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.HasData(
                new Step { Id = 1, Title = "React'e giris", VideoPath = "react1.mp4", CourseId = 1 },
                new Step { Id = 2, Title = "React'e JX formati", VideoPath = "react2.mp4", CourseId = 1 },
                new Step { Id = 3, Title = "React'e component", VideoPath = "react3.mp4", CourseId = 1 },
                new Step { Id = 4, Title = "Flutter'a giris", VideoPath = "flutter.mp4", CourseId = 2 }
            );
        }
    }
}
