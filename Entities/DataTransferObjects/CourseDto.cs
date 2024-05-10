namespace Entities.DataTransferObjects
{
    public class CourseDto
    {
        public int Id { get; init; }
        public string CourseName { get; init; }
        public string CourseDescription { get; init; }
        public string CourseThumbnail { get; init; }
        public bool IsOver { get; init; }
        public bool IsRequire { get; init; }
        public ICollection<int> RankIds { get; set; }
    }
}
