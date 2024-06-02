using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface IExamTypeService
    {
        Task<IEnumerable<ExamTypeDto>> GetAllStepsAsync(bool trackChanges);
        Task<ExamTypeDto> CreateOneStepAsync(ExamTypeDtoForInsertion examTypeDto);
        Task DeleteOneStepAsync(int id, bool trackChanges);
    }
}
