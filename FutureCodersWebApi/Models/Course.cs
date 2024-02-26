namespace FutureCodersWebApi.Models
{
    public class Course
    {
        public int Id { get; set; }
        public String? CourseName { get; set; }
        public String? CourseDescription { get; set; }
        public String? CourseThumbnail { get; set; }
        public bool IsRequire { get; set; }
        public int Rank { get; set; }
    }
}
