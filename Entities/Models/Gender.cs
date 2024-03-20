using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Gender", Schema = "user")]
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
