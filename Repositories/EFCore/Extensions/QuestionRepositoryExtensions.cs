using Entities.Models;

namespace Repositories.EFCore.Extensions
{
    public static class QuestionRepositoryExtensions
    {
        public static IQueryable<Question> FilterQuestionsWithQuestionId(this IQueryable<Question> questionOptions,
            int examTypeId) =>
            questionOptions.Where(q => q.ExamTypeId.Equals(examTypeId));
    }
}
