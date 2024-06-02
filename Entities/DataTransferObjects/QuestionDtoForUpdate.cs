using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record QuestionDtoForUpdate
    {
        [Required]
        public int Id { get; init; }

        [Required(ErrorMessage = "Question text is a required field.")]
        [MinLength(20, ErrorMessage = "Question text consist of at least 20 characters.")]
        public string QuestionText { get; init; }
    }
}
