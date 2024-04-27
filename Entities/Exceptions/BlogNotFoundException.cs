namespace Entities.Exceptions
{
    public sealed class BlogNotFoundException : NotFoundException
    {
        public BlogNotFoundException(int id)
            : base($"The blog with id: {id} could not found!")
        {
        }
    }
}
