namespace Entities.DataTransferObjects
{
    public class UserCourseDto
    {
        public int Id { get; init; }
        public string UserId { get; set; }
        public List<int> CourseIds { get; set; }
    }
}
