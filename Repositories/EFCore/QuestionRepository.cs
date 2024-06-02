using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneQuestion(Question question) => Create(question);
        public void DeleteOneQuestion(Question question) => Delete(question);
        public void UpdateOneQuestion(Question question) => Update(question);

        public async Task<List<Question>> GetAllQuestionsAsync(QuestionParameters questionParameters, bool trackChanges)
        {
            IQueryable<Question> query = FindAll(trackChanges)
                .OrderBy(o => o.Id);

            if (questionParameters.ExamTypeId != null)
            {
                query = query.FilterQuestionsWithQuestionId((int)questionParameters.ExamTypeId);
            }

            var questions = await query.ToListAsync();

            return questions;
        }

        public async Task<Question> GetOneQuestionByIdAsync(int questionId, bool trackChanges) =>
            await FindByCondition(q => q.Id.Equals(questionId), trackChanges)
                .SingleOrDefaultAsync();
    }
}
