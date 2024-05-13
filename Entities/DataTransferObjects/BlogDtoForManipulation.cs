using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record BlogDtoForManipulation
    {
        [Required(ErrorMessage = "Blog title is a required field.")]
        [MinLength(5, ErrorMessage = "Blog title consist of at least 5 characters.")]
        [MaxLength(100, ErrorMessage = "Blog title must consist of at maxium 100 characters")]
        public string Title { get; init; }

        [Required(ErrorMessage = "Blog content is a required field.")]
        [MinLength(50, ErrorMessage = "Blog content consist of at least 50 characters.")]
        public string Content { get; init; }
        public string? BlogImage { get; set; }
    }
}
