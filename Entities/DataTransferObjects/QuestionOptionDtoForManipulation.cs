using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record QuestionOptionDtoForManipulation
    {
        [Required(ErrorMessage = "Option answer is a required field.")]
        [MinLength(5, ErrorMessage = "Option answer consist of at least 5 characters.")]
        public string Answer { get; init; }

        [Required(ErrorMessage = "It is mandatory to enter information whether the answer is correct or not.")]
        public bool IsTrue { get; init; } = false;

        [Required(ErrorMessage = "Question Id is a required field.")]
        public int QuestionId { get; init; }
    }
}
