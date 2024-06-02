namespace Entities.Exceptions
{
    public class InvalidUserCourseBadRequestException : BadRequestException
    {
        public InvalidUserCourseBadRequestException(string userId, int courseId)
            : base($"The user with id: {userId} has not followed all the steps of the course with id: {courseId}! ")
        {
        }
    }
}
