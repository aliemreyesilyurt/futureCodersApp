using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class ExamTypeConfig : IEntityTypeConfiguration<ExamType>
    {
        public void Configure(EntityTypeBuilder<ExamType> builder)
        {
            builder.HasData(
                new ExamType { Id = 1, Name = "Algorithmic Level Determination Exam" },
                new ExamType { Id = 2, Name = "Final Coders Exam" }
            );
        }
    }
}
