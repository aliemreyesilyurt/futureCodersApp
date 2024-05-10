using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(
                new Review { Id = 1, Content = "Çok başarılı bir kurs olmuş!", CourseId = 1, UserId = "a1" },
                new Review { Id = 2, Content = "Sade ve güzel bir anlatım olmuş, harika :D", CourseId = 2, UserId = "a1" }
            );
        }
    }
}
