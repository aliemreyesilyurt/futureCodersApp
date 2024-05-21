using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record UserChangeEmailDto
    {
        [Required(ErrorMessage = "New email is required.")]
        public string NewEmail { get; init; }

        [Compare("NewEmail", ErrorMessage = "Please confirm your new email!")]
        public string ConfirmEmail { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; init; }
    }
}
