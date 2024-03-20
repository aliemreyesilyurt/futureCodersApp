namespace Entities.DataTransferObjects
{
    public record CourseDto
    {
        public int Id { get; init; }
        public string CourseName { get; init; }
        public string CourseDescription { get; init; }
        public string CourseThumbnail { get; init; }
        public bool IsRequire { get; init; }
    }
}
