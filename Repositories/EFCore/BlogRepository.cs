using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

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

        public async Task<PagedList<Blog>> GetAllBlogsAsync(BlogParameters blogParameters, bool trackChanges)
        {
            var blogs = await FindAll(trackChanges)
                .Search(blogParameters.SearchTerm)
                .Sort(blogParameters.OrderBy)
                .ToListAsync();


            return PagedList<Blog>
            .ToPagedList(blogs,
                blogParameters.PageNumber,
                blogParameters.PageSize);
        }

        public async Task<Blog> GetOneBlogByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

    }
}
