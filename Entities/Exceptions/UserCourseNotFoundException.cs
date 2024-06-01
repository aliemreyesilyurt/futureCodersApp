namespace Entities.Exceptions
{
    public class UserCourseNotFoundException : NotFoundException
    {
        public UserCourseNotFoundException(int id)
            : base($"The user course with id: {id} could not found!")
        {
        }
    }
}
