using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contract
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync(bool trackChanges);
        Task<CourseDto> GetOneCourseByIdAsync(int id, bool trackChanges);
        Task<CourseDto> CreateOneCourseAsync(CourseDtoForInsertion course);
        Task UpdateOneCourseAsync(int id, CourseDtoForUpdate courseDto, bool trackChanges);
        Task DeleteOneCourseAsync(int id, bool trackChanges);
        Task<(CourseDtoForUpdate courseDtoForUpdate, Course course)> GetOneCourseForPatchAsync(int id, bool trackChanges);
        Task SaveChangesForUpdateAsync(CourseDtoForUpdate courseDto, Course course);
    }
}
