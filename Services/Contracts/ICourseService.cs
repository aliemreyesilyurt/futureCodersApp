﻿using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface ICourseService
    {
        Task<(IEnumerable<CourseDto> courses, MetaData metaData)> GetAllCoursesAsync(CourseParameters courseParameters, bool trackChanges);
        Task<CourseDto> GetOneCourseByIdAsync(int id, bool trackChanges);
        Task<CourseDtoForInsertion> CreateOneCourseAsync(CourseDtoForInsertion course);
        Task UpdateOneCourseAsync(int id, CourseDtoForUpdate courseDto, bool trackChanges);
        Task DeleteOneCourseAsync(int id, bool trackChanges);
        Task<(CourseDtoForUpdate courseDtoForUpdate, Course course)> GetOneCourseForPatchAsync(int id, bool trackChanges);
        Task SaveChangesForUpdateAsync(CourseDtoForUpdate courseDto, Course course);
        Task UpdateOneCourseStatusAsync(int courseId, bool trackChanges);
        Task UpdateOneCourseImageAsync(int id, string fileName, bool trackChanges);
    }
}
