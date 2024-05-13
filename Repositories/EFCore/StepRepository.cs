using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public class StepRepository : RepositoryBase<Step>, IStepRepository
    {
        public StepRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneStep(Step step) => Create(step);
        public void DeleteOneStep(Step step) => Delete(step);
        public void UpdateOneStep(Step step) => Update(step);


        public async Task<List<Step>> GetAllStepsAsync(StepParameters stepParameters, bool trackChanges)
        {
            if (stepParameters.CourseId != null)
            {
                var steps = await FindAll(trackChanges)
                .FilterStepsWithCourseId((int)stepParameters.CourseId)
                .Search(stepParameters.SearchTerm)
                .OrderBy(s => s.Id)
                .ToListAsync();

                return steps;
            }
            else
            {
                var steps = await FindAll(trackChanges)
                    .Search(stepParameters.SearchTerm)
                    .OrderBy(s => s.Id)
                    .ToListAsync();

                return steps;
            }
        }

        public async Task<Step> GetOneStepByIdAsync(int stepId, bool trackChanges) =>
            await FindByCondition(s => s.Id.Equals(stepId), trackChanges)
                .SingleOrDefaultAsync();

    }
}
