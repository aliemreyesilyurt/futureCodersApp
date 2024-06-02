using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record ExamTypeDtoForInsertion
    {
        [Required(ErrorMessage = "Exam Type title is a required field.")]
        public string Name { get; init; }
    }
}
