using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IUserStepRepository : IRepositoryBase<UserStep>
    {
        Task<UserStepDto> GetAllUserStepsAsync(string userId, bool trackChanges);
        Task<UserStep> GetOneUserStepAsync(int stepId, bool trackChanges);
        void CreateOneUserStep(UserStep userStep);
        void DeleteOneUserStep(UserStep userStep);
    }
}
