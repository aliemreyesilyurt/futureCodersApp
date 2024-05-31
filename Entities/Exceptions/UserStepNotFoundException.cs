namespace Entities.Exceptions
{
    public class UserStepNotFoundException : NotFoundException
    {
        public UserStepNotFoundException(int id)
            : base($"The user step with id: {id} could not found!")
        {
        }
    }
}
