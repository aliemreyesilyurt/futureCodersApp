using Entities.Models;
using Entities.RequestFeatures;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IStepRepository : IRepositoryBase<Step>
    {
        Task<List<Step>> GetAllStepsAsync(StepParameters stepParameters, bool trackChanges);
        Task<Step> GetOneStepByIdAsync(int stepId, bool trackChanges);
        void CreateOneStep(Step step);
        void UpdateOneStep(Step step);
        void DeleteOneStep(Step step);
    }
}
