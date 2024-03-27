using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class GenderConfig : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {

            builder.HasData(
                new Gender { Id = 1, Name = "Male" },
                new Gender { Id = 2, Name = "Female" },
                new Gender { Id = 3, Name = "Other" }
            );
        }
    }
}
