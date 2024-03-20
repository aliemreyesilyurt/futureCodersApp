using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("QuestionAnswer", Schema = "exam")]
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionOptionId { get; set; }
        public QuestionOption QuestionOption { get; set; }
    }
}
