using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Review", Schema = "course")]
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
