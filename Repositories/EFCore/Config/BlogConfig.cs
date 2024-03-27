using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasData(
                new Blog { Id = 1, Content = "Backend ile ilgili yol haritası!" },
                new Blog { Id = 2, Content = "Frontend ile ilgili yol haritası!" },
                new Blog { Id = 3, Content = "Mobil uygulama ile ilgili yol haritası!" }
            );
        }
    }
}
