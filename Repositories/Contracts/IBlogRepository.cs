using Entities.Models;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IBlogRepository : IRepositoryBase<Blog>
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync(bool trackChanges);
        Task<Blog> GetOneBlogByIdAsync(int id, bool trackChanges);
        void CreateOneBlog(Blog blog);
        void UpdateOneBlog(Blog blog);
        void DeleteOneBlog(Blog blog);
    }
}
