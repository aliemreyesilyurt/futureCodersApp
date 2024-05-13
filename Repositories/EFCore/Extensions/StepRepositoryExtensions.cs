using Entities.Models;

namespace Repositories.EFCore.Extensions
{
    public static class StepRepositoryExtensions
    {
        public static IQueryable<Step> FilterStepsWithCourseId(this IQueryable<Step> steps,
            int courseId) =>
            steps.Where(s => s.CourseId.Equals(courseId));

        public static IQueryable<Step> Search(this IQueryable<Step> steps,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return steps;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return steps
                .Where(s => s.Title
                .ToLower()
                .Contains(searchTerm));
        }
    }
}
