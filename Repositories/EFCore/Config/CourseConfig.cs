using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course { Id = 1, CourseName = "React", CourseDescription = "This course is a react course", CourseThumbnail = "", IsRequire = true, Rank = 0 },
                new Course { Id = 2, CourseName = "Flutter", CourseDescription = "This course is a flutter course", CourseThumbnail = "", IsRequire = false, Rank = 1 },
                new Course { Id = 3, CourseName = "Bootstrap", CourseDescription = "This course is a bootstrap 5 course", CourseThumbnail = "", IsRequire = false, Rank = 0 }
            );
        }
    }
}
