using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record QuestionAnswerDtoForInsertion
    {
        [Required(ErrorMessage = "User id is require.")]
        public string UserId { get; init; }

        [Required(ErrorMessage = "Question option id is require.")]
        public int QuestionOptionId { get; init; }
    }
}
