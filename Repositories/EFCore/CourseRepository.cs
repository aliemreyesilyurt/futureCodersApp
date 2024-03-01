using Entities.Models;
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

        public IQueryable<Course> GetAllCourses(bool trackChanges) =>
            FindAll(trackChanges);

        public Course GetOneCourseById(int id, bool trackChanges) =>
            FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefault();

    }
}
