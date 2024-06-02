namespace Entities.Exceptions
{
    public class QuestionNotFoundException : NotFoundException
    {
        public QuestionNotFoundException(int id)
            : base($"Question with id {id} could not found!")
        {
        }
    }
}
