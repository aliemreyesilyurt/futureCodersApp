using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAllReviewsAsync(ReviewParameters reviewParameters, bool trackChanges);
        Task<ReviewDto> GetOneReviewByIdAsync(int id, bool trackChanges);
        Task<ReviewDto> CreateOneReviewAsync(ReviewDtoForInsertion reviewDto, string userId);
        Task UpdateOneReviewAsync(int id, ReviewDtoForUpdate reviewDto, bool trackChanges);
        Task UpdateOneReviewContentAsync(int id, string content, bool trackChanges);
        Task DeleteOneReviewAsync(int id, bool trackChanges);
    }
}
