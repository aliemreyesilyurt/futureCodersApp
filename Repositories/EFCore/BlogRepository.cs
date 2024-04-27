using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        public BlogRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneBlog(Blog blog) => Create(blog);

        public void UpdateOneBlog(Blog blog) => Update(blog);

        public void DeleteOneBlog(Blog blog) => Delete(blog);

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(b => b.Title)
                .ToListAsync();

        public async Task<Blog> GetOneBlogByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

    }
}
