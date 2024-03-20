using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("QuestionType", Schema = "exam")]
    public class QuestionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
