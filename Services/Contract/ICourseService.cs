using Entities.Models;

namespace Services.Contract
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses(bool trackChanges);
        Course GetOneCourseById(int id, bool trackChanges);
        Course CreateOneCourse(Course course);
        void UpdateOneCourse(int id, Course course, bool trackChanges);
        void DeleteOneCourse(int id, bool trackChanges);
    }
}
