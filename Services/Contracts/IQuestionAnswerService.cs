using Entities.DataTransferObjects;

namespace Services.Contracts
{
    public interface IQuestionAnswerService
    {
        Task<QuestionAnswerDto> GetAllQuestionAnswersAsync(string userId, bool trackChanges);
        Task<QuestionAnswerDto> CreateOneQuestionAnswerAsync(string userId, int questionOptionId);
    }
}
