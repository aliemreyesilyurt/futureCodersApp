using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Rank", Schema = "user")]
    public class Rank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

    }
}
