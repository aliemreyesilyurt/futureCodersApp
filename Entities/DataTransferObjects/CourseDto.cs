namespace Entities.DataTransferObjects
{
    public record CourseDto
    {
        public int Id { get; init; }
        public String? CourseName { get; init; }
        public String? CourseDescription { get; init; }
        public String? CourseThumbnail { get; init; }
        public bool IsRequire { get; init; }
        public int Rank { get; init; }
    }
}
