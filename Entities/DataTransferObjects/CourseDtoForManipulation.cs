using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record CourseDtoForManipulation
    {
        [Required(ErrorMessage = "Course name is a required field.")]
        [MinLength(5, ErrorMessage = "Course name consist of at least 5 characters.")]
        [MaxLength(100, ErrorMessage = "Course name must consist of at maxium 100 characters")]
        public string CourseName { get; init; }

        [Required(ErrorMessage = "Course description is a required field.")]
        [MinLength(5, ErrorMessage = "Course description consist of at least 5 characters.")]
        public string CourseDescription { get; init; }

        [Required(ErrorMessage = "Course thumbnail is a required field.")]
        [MinLength(3, ErrorMessage = "Course description consist of at least 3 characters.")]
        public string CourseThumbnail { get; init; }

        [Required(ErrorMessage = "IsRequire is a required field.")]
        public bool IsRequire { get; init; }
    }
}
