using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record CourseDtoForManipulation
    {
        [Required(ErrorMessage = "Course name is a required field.")]
        [MinLength(5, ErrorMessage = "Course name consist of at least 5 characters.")]
        [MaxLength(50, ErrorMessage = "Course name must consist of at maxium 50 characters")]
        public String CourseName { get; init; }

        [Required(ErrorMessage = "Course description is a required field.")]
        [MinLength(5, ErrorMessage = "Course description consist of at least 5 characters.")]
        public String CourseDescription { get; init; }

        [Required(ErrorMessage = "Course thumbnail is a required field.")]
        [MinLength(3, ErrorMessage = "Course description consist of at least 3 characters.")]
        public String CourseThumbnail { get; init; }

        [Required(ErrorMessage = "IsRequire is a required field.")]
        public bool IsRequire { get; init; }

        [Required(ErrorMessage = "Rank is a required field.")]
        public int Rank { get; init; }
    }
}
