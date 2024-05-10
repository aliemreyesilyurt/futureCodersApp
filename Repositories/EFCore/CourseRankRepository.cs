using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public class CourseRankRepository : RepositoryBase<CourseRank>, ICourseRankRepository
    {
        public CourseRankRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneCourseRank(CourseRank courseRank) => Create(courseRank);

        public void DeleteOneCourseRank(CourseRank courseRank) => Delete(courseRank);

        public void UpdateOneCourseRank(CourseRank courseRank) => Update(courseRank);

        public async Task<List<CourseRank>> GetAllCourseRanksWithParamsAsync(CourseParameters courseParameters, bool trackChanges)
        {
            List<CourseRank> courseRank = new List<CourseRank>();

            if (courseParameters.RankId != null)
            {
                courseRank = await FindAll(trackChanges)
                    .FilterCoursesWithRank(Convert.ToInt32(courseParameters.RankId))
                    .OrderBy(cr => cr.Course.Id)
                    .ToListAsync();
            }
            else
            {
                courseRank = await FindAll(trackChanges)
                .OrderBy(cr => cr.CourseId)
                .ToListAsync();
            }

            return courseRank;
        }

        public async Task<List<CourseRank>> GetOneCourseRankByIdAsync(int courseId, bool trackChanges) =>
            await FindByCondition(cr => cr.CourseId.Equals(courseId), trackChanges)
            .ToListAsync();
    }
}
