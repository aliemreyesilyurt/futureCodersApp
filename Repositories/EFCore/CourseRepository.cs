using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public sealed class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneCourse(Course course) => Create(course);

        public void DeleteOneCourse(Course course) => Delete(course);

        public void UpdateOneCourse(Course course) => Update(course);

        public async Task<PagedList<Course>> GetAllCoursesAsync(List<CourseRank> courseWithRank, CourseParameters courseParameters, bool trackChanges)
        {
            List<Course> courses = new List<Course>();

            var courseIds = courseWithRank.Select(c => c.CourseId).ToList();

            if (courseParameters.IsRequire != null)
            {
                courses = await FindAll(trackChanges)
                .Where(c => courseIds.Contains(c.Id))
                .FilterCoursesWithIsRequire(courseParameters.IsRequire)
                .Search(courseParameters.SearchTerm)
                .Sort(courseParameters.OrderBy)
                .ToListAsync();
            }
            else
            {
                courses = await FindAll(trackChanges)
                .Where(c => courseIds.Contains(c.Id))
                .Search(courseParameters.SearchTerm)
                .Sort(courseParameters.OrderBy)
                .ToListAsync();
            }

            return PagedList<Course>
                .ToPagedList(courses,
                courseParameters.PageNumber,
                courseParameters.PageSize);
        }

        public async Task<Course> GetOneCourseByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

    }
}
