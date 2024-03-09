using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneCourse(Course course) => Create(course);

        public void DeleteOneCourse(Course course) => Delete(course);

        public void UpdateOneCourse(Course course) => Update(course);

        public async Task<PagedList<Course>> GetAllCoursesAsync(CourseParameters courseParameters, bool trackChanges)
        {
            var course = await FindAll(trackChanges)
                .OrderBy(b => b.Id)
                .ToListAsync();

            return PagedList<Course>
                .ToPagedList(course, courseParameters.PageNumber, courseParameters.PageSize);
        }

        public async Task<Course> GetOneCourseByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

    }
}
