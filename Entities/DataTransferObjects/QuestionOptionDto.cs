namespace Entities.DataTransferObjects
{
    public record QuestionOptionDto
    {
        public int Id { get; init; }
        public string Answer { get; init; }
        public bool IsTrue { get; init; } = false;
        public int QuestionId { get; init; }
    }
}
