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


        public async Task<List<Step>> GetAllStepsAsync(StepParameters stepParameters, List<int> stepIds, bool trackChanges)
        {
            IQueryable<Step> query = FindAll(trackChanges)
                .Search(stepParameters.SearchTerm)
                .OrderBy(s => s.Id);

            if (stepParameters.CourseId != null)
            {
                query = query.FilterStepsWithCourseId((int)stepParameters.CourseId);
            }

            var steps = await query.ToListAsync();

            if (stepIds != null && stepIds.Any())
            {
                foreach (var step in steps)
                {
                    step.Status = stepIds.Contains(step.Id);
                }
            }

            return steps;
        }

        public async Task<Step> GetOneStepByIdAsync(int stepId, bool trackChanges) =>
            await FindByCondition(s => s.Id.Equals(stepId), trackChanges)
                .SingleOrDefaultAsync();

    }
}
