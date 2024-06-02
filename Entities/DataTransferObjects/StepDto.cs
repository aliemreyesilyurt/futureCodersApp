namespace Entities.DataTransferObjects
{
    public class StepDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string VideoPath { get; init; }
        public bool Status { get; set; } = false;
        public int CourseId { get; init; }
    }
}
