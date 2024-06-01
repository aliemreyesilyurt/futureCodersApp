namespace Entities.DataTransferObjects
{
    public class UserCourseDto
    {
        public int Id { get; init; }
        public string UserId { get; init; }
        public List<int> CourseIds { get; init; }
    }
}
