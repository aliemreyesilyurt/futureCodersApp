using Entities.Models;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class CourseRepositoryExtensions
    {
        public static IQueryable<CourseRank> FilterCoursesWithRank(this IQueryable<CourseRank> ranks,
            int rank) =>
            ranks.Where(r => r.Rank.Id.Equals(rank));

        public static IQueryable<Course> FilterCoursesWithIsRequire(this IQueryable<Course> courses,
            bool? isRequire) =>
            courses.Where(c => c.IsRequire.Equals(isRequire));

        public static IQueryable<Course> Search(this IQueryable<Course> courses,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return courses;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return courses
                .Where(c => c.CourseName
                .ToLower()
                .Contains(searchTerm));
        }

        public static IQueryable<Course> Sort(this IQueryable<Course> courses,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return courses.OrderBy(c => c.Id);

            // Global metod Type verilerek cagrilir
            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Course>(orderByQueryString);

            if (orderQuery is null)
                return courses.OrderBy(c => c.Id);

            return courses.OrderBy(orderQuery);

        }
    }
}
