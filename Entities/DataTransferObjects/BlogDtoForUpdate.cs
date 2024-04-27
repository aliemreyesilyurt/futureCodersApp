using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record BlogDtoForUpdate
    {
        [Required]
        public int Id { get; init; }
    }
}
