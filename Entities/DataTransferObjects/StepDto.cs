namespace Entities.DataTransferObjects
{
    public record StepDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string VideoPath { get; init; }
        public bool Status { get; init; } = false;
        public int CourseId { get; init; }
    }
}
