using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record ReviewDtoForManipulation
    {
        [Required(ErrorMessage = "Review content is a required field.")]
        [MinLength(10, ErrorMessage = "Review content consist of at least 10 characters.")]
        [MaxLength(100, ErrorMessage = "Review content must consist of at maxium 100 characters")]
        public string Content { get; init; }

        [Required(ErrorMessage = "Course id is a required field.")]
        public int CourseId { get; init; }
    }
}
