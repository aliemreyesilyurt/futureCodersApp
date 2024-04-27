using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("UserStep", Schema = "user")]
    public class UserStep
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public int StepId { get; set; }
        public Step Step { get; set; }
    }
}
