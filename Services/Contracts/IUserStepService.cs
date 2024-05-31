using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts
{
    public interface IUserStepService
    {
        Task<UserStepDto> GetAllUserStepsAsync(string userId, bool trackChanges);
        Task<UserStep> CreateOneUserStepAsync(UserStepDtoForInsertion userStepDto);
        Task DeleteOneUserStepAsync(int stepId, bool trackChanges);
    }
}
