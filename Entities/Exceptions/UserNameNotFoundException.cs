namespace Entities.Exceptions
{
    public class UserNameNotFoundException : NotFoundException
    {
        public UserNameNotFoundException(string userName)
            : base($"The user with username: {userName} could not found!")
        {
        }
    }
}
