using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Course", Schema = "course")]
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseThumbnail { get; set; }
        public bool IsOver { get; set; } = false;
        public bool IsRequire { get; set; }
    }
}
