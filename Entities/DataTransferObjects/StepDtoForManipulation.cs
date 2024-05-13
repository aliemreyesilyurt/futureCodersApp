using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record StepDtoForManipulation
    {
        [Required(ErrorMessage = "Step title is a required field.")]
        [MinLength(5, ErrorMessage = "Step title consist of at least 5 characters.")]
        [MaxLength(100, ErrorMessage = "Step title must consist of at maxium 100 characters")]
        public string Title { get; init; }
        public bool Status { get; set; } = false;

        [Required(ErrorMessage = "Course id is a required field.")]
        public int CourseId { get; init; }
        public string? VideoPath { get; set; }
    }

}
