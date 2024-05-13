using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Step", Schema = "course")]
    public class Step
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string VideoPath { get; set; }
        public bool Status { get; set; } = false;

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
