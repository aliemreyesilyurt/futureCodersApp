namespace Entities.DataTransferObjects
{
    public record BlogDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
        public string BlogImage { get; init; }
    }
}
