namespace Entities.DataTransferObjects
{
    public record QuestionAnswerDto
    {
        public int Id { get; init; }
        public string UserId { get; init; }
        public List<int> QuestionOptionIds { get; init; }
    }
}
