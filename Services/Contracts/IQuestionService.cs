using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Services.Contracts
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync(QuestionParameters questionParameters, bool trackChanges);
        Task<QuestionDto> GetOneQuestionByIdAsync(int id, bool trackChanges);
        Task<QuestionDto> CreateOneQuestionAsync(QuestionDtoForInsertion questionDto);
        Task UpdateOneQuestionTextAsync(QuestionDtoForUpdate questionDto, bool trackChanges);
        Task DeleteOneQuestionAsync(int id, bool trackChanges);
    }
}
