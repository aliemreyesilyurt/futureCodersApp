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

        public void CreateOneCourseRank(CourseRank course) => Create(course);

        public void DeleteOneCourseRank(CourseRank course) => Delete(course);

        public void UpdateOneCourseRank(CourseRank course) => Update(course);

        public async Task<PagedList<CourseRank>> GetAllCourseRanksAsync(CourseParameters courseParameters, bool trackChanges)
        {
            List<CourseRank> courseRank = new List<CourseRank>();

            if (courseParameters.RankId != null)
            {
                courseRank = await FindAll(trackChanges)
                    .FilterCoursesWithRank(Convert.ToInt32(courseParameters.RankId))
                    .OrderBy(b => b.Course.Id)
                    .ToListAsync();
            }
            else
            {
                courseRank = await FindAll(trackChanges)
                .OrderBy(b => b.CourseId)
                .ToListAsync();
            }
            return PagedList<CourseRank>

                .ToPagedList(courseRank,
                courseParameters.PageNumber,
                courseParameters.PageSize);
        }

        public async Task<CourseRank> GetOneCourseRankByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}
