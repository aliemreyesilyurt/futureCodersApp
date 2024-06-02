using Entities.Models;
using Entities.RequestFeatures;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IQuestionOptionRepository : IRepositoryBase<QuestionOption>
    {
        Task<List<QuestionOption>> GetAllQuestionOptionsAsync(QuestionOptionParameters optionParameters, bool trackChanges);
        Task<QuestionOption> GetOneQuestionOptionByIdAsync(int questionId, bool trackChanges);
        void CreateOneQuestionOption(QuestionOption questionOption);
        void UpdateOneQuestionOption(QuestionOption questionOption);
        void DeleteOneQuestionOption(QuestionOption questionOption);
    }
}
