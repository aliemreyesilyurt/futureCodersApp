using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync(bool trackChanges);
        Task<Blog> GetOneBlogByIdAsync(int id, bool trackChanges);
        Task<BlogDto> CreateOneBlogAsync(BlogDtoForInsertion blogDto);
        Task UpdateOneBlogAsync(int id, BlogDtoForUpdate blogDto, bool trackChanges);
        Task DeleteOneBlogAsync(int id, bool trackChanges);
        Task<(BlogDtoForUpdate blodDtoForUpdate, Blog blog)> GetOneBlogForPatchAsync(int id, bool trackChanges);
        Task SaveChangesForUpdateAsync(BlogDtoForUpdate blogDtoForUpdate, Blog blog);
    }
}
