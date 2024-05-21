namespace Entities.DataTransferObjects
{
    public record UserDtoForUpdate
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }
}
