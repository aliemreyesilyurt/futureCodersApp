using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record ReviewDtoForUpdate : ReviewDtoForManipulation
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public string UserId { get; init; }
    }
}
