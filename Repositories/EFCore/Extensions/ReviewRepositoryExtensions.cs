using Entities.Models;

namespace Repositories.EFCore.Extensions
{
    public static class ReviewRepositoryExtensions
    {
        public static IQueryable<Review> FilterReviewsWithCourseId(this IQueryable<Review> reviews, int courseId) =>
            reviews.Where(r => r.CourseId.Equals(courseId));

        public static IQueryable<Review> Search(this IQueryable<Review> reviews,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return reviews;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return reviews
                .Where(r => r.Content
                .ToLower()
                .Contains(searchTerm));
        }
    }
}
