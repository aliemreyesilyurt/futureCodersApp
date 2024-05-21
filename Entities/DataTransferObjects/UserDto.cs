namespace Entities.DataTransferObjects
{
    public record UserDto
    {
        public string Id { get; init; }
        public string UserName { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public bool IsAvailable { get; init; }
        public string Email { get; init; }
        public int GenderId { get; init; }
        public int RankId { get; init; }
    }
}
