using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IUserStepService
    {
        Task<UserStepDto> GetAllUserStepsAsync(string userId, bool trackChanges);
        Task<UserStep> CreateOneUserStepAsync(string userId, int stepId);
    }
}
