using Entities.Models;

namespace Entities.DataTransferObjects
{
    public record QuestionDto
    {
        public int Id { get; init; }
        public string QuestionText { get; init; }
        public int ExamTypeId { get; init; }
        public List<QuestionOption> QuestionOptions { get; set; }
    }
}
