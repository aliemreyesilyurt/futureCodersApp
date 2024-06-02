using Entities.Models;
using Entities.RequestFeatures;
using Repositories.EFCore;

namespace Repositories.Contracts
{
    public interface IQuestionRepository : IRepositoryBase<Question>
    {
        Task<List<Question>> GetAllQuestionsAsync(QuestionParameters questionParameters, bool trackChanges);
        Task<Question> GetOneQuestionByIdAsync(int questionId, bool trackChanges);
        void CreateOneQuestion(Question questionOption);
        void UpdateOneQuestion(Question questionOption);
        void DeleteOneQuestion(Question questionOption);
    }
}
