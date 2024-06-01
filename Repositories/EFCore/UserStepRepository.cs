using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore
{
    public class UserStepRepository : RepositoryBase<UserStep>, IUserStepRepository
    {
        public UserStepRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneUserStep(UserStep userStep) => Create(userStep);

        public async Task<UserStepDto> GetAllUserStepsAsync(string userId, bool trackChanges)
        {
            var stepIds = await FindAll(trackChanges)
                .Where(us => us.UserId == userId)
                .Select(us => us.StepId)
                .ToListAsync();

            var userSteps = new UserStepDto
            {
                UserId = userId,
                StepIds = stepIds,
            };

            return userSteps;
        }

        public async Task<UserStep> GetOneUserStepAsync(int stepId, bool trackChanges) =>
            await FindByCondition(us => us.StepId == stepId, trackChanges)
                .SingleOrDefaultAsync();
    }
}
