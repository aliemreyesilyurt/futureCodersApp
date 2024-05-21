namespace Entities.Exceptions
{
    public class UserIdNotFoundException : NotFoundException
    {
        public UserIdNotFoundException(string id)
            : base($"The user with id: {id} could not found!")
        {
        }
    }
}
