using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record QuestionDtoForInsertion
    {
        [Required(ErrorMessage = "Question text is a required field.")]
        [MinLength(20, ErrorMessage = "Question text consist of at least 20 characters.")]
        public string QuestionText { get; init; }

        [Required(ErrorMessage = "Exam type id is a required field.")]
        public int ExamTypeId { get; init; }
    }
}
