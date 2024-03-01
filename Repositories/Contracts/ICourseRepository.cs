using Entities.Models;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        IQueryable<Course> GetAllCourses(bool trackChanges);
        Course GetOneCourseById(int id, bool trackChanges);
        void CreateOneCourse(Course course);
        void UpdateOneCourse(Course course);
        void DeleteOneCourse(Course course);
    }
}
