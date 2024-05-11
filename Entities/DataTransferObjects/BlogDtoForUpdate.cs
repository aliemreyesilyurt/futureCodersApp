using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record BlogDtoForUpdate : BlogDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
}
