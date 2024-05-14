using Entities.Models;
using Entities.RequestFeatures;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<List<Review>> GetAllReviewsAsync(ReviewParameters reviewParameters, bool trackChanges);
        Task<Review> GetOneReviewByIdAsync(int reviewId, bool trackChanges);
        void CreateOneReview(Review review);
        void UpdateOneReview(Review review);
        void DeleteOneReview(Review review);
    }
}
