using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title).IsRequired();

            builder.HasData(
                new Blog { Id = 1, Title = "Backend Blog", BlogImage = "", Content = "Backend ile ilgili yol haritası!" },
                new Blog { Id = 2, Title = "Frontend Blog", BlogImage = "", Content = "Frontend ile ilgili yol haritası" },
                new Blog { Id = 3, Title = "Mobile Blog", BlogImage = "", Content = "Mobil uygulama ile ilgili yol haritası!" }
            );
        }
    }
}
