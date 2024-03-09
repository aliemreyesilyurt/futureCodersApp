using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contract
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetAllCourses(bool trackChanges);
        CourseDto GetOneCourseById(int id, bool trackChanges);
        CourseDto CreateOneCourse(CourseDtoForInsertion course);
        void UpdateOneCourse(int id, CourseDtoForUpdate courseDto, bool trackChanges);
        void DeleteOneCourse(int id, bool trackChanges);
        (CourseDtoForUpdate courseDtoForUpdate, Course course) GetOneCourseForPatch(int id, bool trackChanges);
        void SaveChangesForUpdate(CourseDtoForUpdate courseDto, Course course);
    }
}
