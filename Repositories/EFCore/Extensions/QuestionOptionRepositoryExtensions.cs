using Entities.Models;

namespace Repositories.EFCore.Extensions
{
    public static class QuestionOptionRepositoryExtensions
    {
        public static IQueryable<QuestionOption> FilterOptionsWithQuestionId(this IQueryable<QuestionOption> questionOptions,
            int questionId) =>
            questionOptions.Where(o => o.QuestionId.Equals(questionId));
    }
}
