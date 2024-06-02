namespace Entities.Exceptions
{
    public class QuestionOptionNotFoundException : NotFoundException
    {
        public QuestionOptionNotFoundException(int id)
            : base($"Question option with id {id} could not found!")
        {
        }
    }
}
