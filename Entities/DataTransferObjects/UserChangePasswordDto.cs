using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record UserChangePasswordDto
    {
        [Required(ErrorMessage = "Current password is required.")]
        public string CurrentPassword { get; init; }

        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; init; }
    }
}
