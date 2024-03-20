using Entities.Models;
using Entities.RequestFeatures;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        Task<PagedList<Course>> GetAllCoursesAsync(PagedList<CourseRank> courseWithRank, CourseParameters courseParameters, bool trackChanges);
        Task<Course> GetOneCourseByIdAsync(int id, bool trackChanges);
        void CreateOneCourse(Course course);
        void UpdateOneCourse(Course course);
        void DeleteOneCourse(Course course);
    }
}
