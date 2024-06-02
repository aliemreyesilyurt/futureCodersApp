using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;

namespace Repositories.EFCore
{
    public class QuestionOptionRepository : RepositoryBase<QuestionOption>, IQuestionOptionRepository
    {
        public QuestionOptionRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateOneQuestionOption(QuestionOption questionOption) => Create(questionOption);
        public void DeleteOneQuestionOption(QuestionOption questionOption) => Delete(questionOption);
        public void UpdateOneQuestionOption(QuestionOption questionOption) => Update(questionOption);

        public async Task<List<QuestionOption>> GetAllQuestionOptionsAsync(QuestionOptionParameters optionParameters, bool trackChanges)
        {
            IQueryable<QuestionOption> query = FindAll(trackChanges)
                .OrderBy(o => o.Id);

            if (optionParameters.QuestionId != null)
            {
                query = query.FilterOptionsWithQuestionId((int)optionParameters.QuestionId);
            }

            var questionOptions = await query.ToListAsync();

            return questionOptions;
        }

        public async Task<QuestionOption> GetOneQuestionOptionByIdAsync(int questionId, bool trackChanges) =>
            await FindByCondition(o => o.Id.Equals(questionId), trackChanges)
                .SingleOrDefaultAsync();
    }
}
