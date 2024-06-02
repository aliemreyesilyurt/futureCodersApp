using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IQuestionOptionService
    {
        Task<IEnumerable<QuestionOptionDto>> GetAllQuestionOptionsAsync(QuestionOptionParameters optionParameters, bool trackChanges);
        Task<QuestionOptionDto> GetOneQuestionOptionByIdAsync(int id, bool trackChanges);
        Task<QuestionOptionDto> CreateOneQuestionOptionAsync(QuestionOptionDtoForInsertion optionDto);
        Task UpdateOneQuestionOptionAsync(QuestionOptionDtoForUpdate optionDto, bool trackChanges);
        Task DeleteOneQuestionOptionAsync(int id, bool trackChanges);
    }
}
