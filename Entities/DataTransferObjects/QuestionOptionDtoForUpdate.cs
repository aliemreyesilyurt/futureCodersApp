using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record QuestionOptionDtoForUpdate : QuestionOptionDtoForManipulation
    {
        [Required]
        public int Id { get; init; }
    }
}
