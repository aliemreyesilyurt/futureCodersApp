using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record CourseDtoForUpdate : CourseDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
}
