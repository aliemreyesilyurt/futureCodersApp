using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Question", Schema = "exam")]
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType QuestionType { get; set; }
        public ICollection<QuestionOption> QuestionOptions { get; set; }
    }
}
