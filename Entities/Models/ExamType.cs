using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("ExamType", Schema = "exam")]
    public class ExamType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
