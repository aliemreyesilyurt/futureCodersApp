using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IStepService
    {
        Task<IEnumerable<StepDto>> GetAllStepsAsync(StepParameters stepParameters, List<int> stepIds, bool trackChanges);
        Task<StepDto> GetOneStepByIdAsync(int id, bool trackChanges);
        Task<StepDto> CreateOneStepAsync(StepDtoForInsertion stepDto);
        Task UpdateOneStepAsync(int id, StepDtoForUpdate stepDto, bool trackChanges);
        Task UpdateOneStepVideoAsync(int id, string fileName, bool trackChanges);
        Task DeleteOneStepAsync(int id, bool trackChanges);
    }
}
