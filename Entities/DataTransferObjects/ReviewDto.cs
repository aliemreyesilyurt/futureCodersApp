namespace Entities.DataTransferObjects
{
    public record ReviewDto
    {
        public int Id { get; init; }
        public string Content { get; init; }
        public int CourseId { get; init; }
        public string UserId { get; init; }
    }
}
