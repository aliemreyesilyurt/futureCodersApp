using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneReview(Review review) => Create(review);
        public void DeleteOneReview(Review review) => Delete(review);
        public void UpdateOneReview(Review review) => Update(review);

        public async Task<List<Review>> GetAllReviewsAsync(ReviewParameters reviewParameters, bool trackChanges)
        {
            if (reviewParameters.CourseId != null)
            {
                var reviews = await FindAll(trackChanges)
                .FilterReviewsWithCourseId((int)reviewParameters.CourseId)
                .Search(reviewParameters.SearchTerm)
                .OrderBy(r => r.Id)
                .ToListAsync();

                return reviews;
            }
            else
            {
                var reviews = await FindAll(trackChanges)
                    .Search(reviewParameters.SearchTerm)
                    .OrderBy(r => r.Id)
                    .ToListAsync();

                return reviews;
            }
        }

        public async Task<Review> GetOneReviewByIdAsync(int reviewId, bool trackChanges) =>
            await FindByCondition(r => r.Id.Equals(reviewId), trackChanges)
            .SingleOrDefaultAsync();
    }
}
