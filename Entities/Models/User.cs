using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("User", Schema = "user")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAvailable { get; set; } = false;
        public bool? IsAdmin { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public int RankId { get; set; }
        public Rank Rank { get; set; }
    }
}
