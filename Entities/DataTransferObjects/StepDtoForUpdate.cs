using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record StepDtoForUpdate : StepDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
}
