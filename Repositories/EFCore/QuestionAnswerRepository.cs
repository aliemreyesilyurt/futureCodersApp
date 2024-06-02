using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class QuestionAnswerRepository : RepositoryBase<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneQuestionAnswerStep(QuestionAnswer questionAnswer) => Create(questionAnswer);

        public async Task<QuestionAnswerDto> GetAllQuestionAnswersAsync(string userId, bool trackChanges)
        {
            var questionOptionIds = await FindAll(trackChanges)
                .Where(qa => qa.UserId == userId)
                .Select(qa => qa.QuestionOptionId)
                .ToListAsync();

            var questionAnswers = new QuestionAnswerDto
            {
                UserId = userId,
                QuestionOptionIds = questionOptionIds,
            };

            return questionAnswers;
        }

        public async Task<QuestionAnswer> GetOneUserQuestionAnswerAsync(int questionId, bool trackChanges) =>
            await FindByCondition(qa => qa.QuestionOptionId == questionId, trackChanges)
                .SingleOrDefaultAsync();
    }
}
