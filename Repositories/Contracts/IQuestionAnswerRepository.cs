using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IQuestionAnswerRepository : IRepositoryBase<QuestionAnswer>
    {
        Task<QuestionAnswerDto> GetAllQuestionAnswersAsync(string userId, bool trackChanges);
        Task<QuestionAnswer> GetOneUserQuestionAnswerAsync(int questionId, bool trackChanges);
        void CreateOneQuestionAnswerStep(QuestionAnswer questionAnswer);
    }
}
