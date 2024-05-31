namespace Entities.DataTransferObjects
{
    public record UserStepDto
    {
        public int Id { get; init; }
        public string UserId { get; init; }
        public List<int> StepIds { get; init; }
    }
}
