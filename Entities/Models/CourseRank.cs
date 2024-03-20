using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("CourseRank", Schema = "course")]
    public class CourseRank
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int RankId { get; set; }
        public Rank Rank { get; set; }
    }
}
