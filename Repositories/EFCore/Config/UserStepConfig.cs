using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class UserStepConfig : IEntityTypeConfiguration<UserStep>
    {
        public void Configure(EntityTypeBuilder<UserStep> builder)
        {
            builder.HasData(
                new UserStep { Id = 1, UserId = 2, StepId = 1 },
                new UserStep { Id = 2, UserId = 1, StepId = 1 },
                new UserStep { Id = 3, UserId = 1, StepId = 2 }
            );
        }
    }
}
