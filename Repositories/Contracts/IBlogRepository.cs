using Entities.Models;
using Entities.RequestFeatures;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IBlogRepository : IRepositoryBase<Blog>
    {
        Task<PagedList<Blog>> GetAllBlogsAsync(BlogParameters blogParameters, bool trackChanges);
        Task<Blog> GetOneBlogByIdAsync(int id, bool trackChanges);
        void CreateOneBlog(Blog blog);
        void UpdateOneBlog(Blog blog);
        void DeleteOneBlog(Blog blog);
    }
}
