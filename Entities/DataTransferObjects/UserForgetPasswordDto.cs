using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record UserForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; init; }
    }
}
