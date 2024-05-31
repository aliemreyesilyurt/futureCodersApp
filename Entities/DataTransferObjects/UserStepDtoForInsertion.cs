using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record UserStepDtoForInsertion
    {
        [Required(ErrorMessage = "User id is require.")]
        public string UserId { get; init; }

        [Required(ErrorMessage = "Step id is require.")]
        public int StepId { get; init; }
    }
}
