namespace Entities.Exceptions
{
    public class ReviewNotFoundException : NotFoundException
    {
        public ReviewNotFoundException(int id)
            : base($"The step with id: {id} could not found!")
        {
        }
    }
}
