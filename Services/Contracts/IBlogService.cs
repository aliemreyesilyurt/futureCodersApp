using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IBlogService
    {
        Task<(IEnumerable<BlogDto> blogs, MetaData metaData)> GetAllBlogsAsync(BlogParameters blogParameters, bool trackChanges);
        Task<BlogDto> GetOneBlogByIdAsync(int id, bool trackChanges);
        Task<BlogDto> CreateOneBlogAsync(BlogDtoForInsertion blogDto);
        Task UpdateOneBlogAsync(int id, BlogDtoForUpdate blogDto, bool trackChanges);
        Task UpdateOneBlogImageAsync(int id, string fileName, bool trackChanges);
        Task DeleteOneBlogAsync(int id, bool trackChanges);
        Task SaveChangesForUpdateAsync(BlogDtoForUpdate blogDtoForUpdate, Blog blog);
    }
}
