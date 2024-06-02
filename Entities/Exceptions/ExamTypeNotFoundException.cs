namespace Entities.Exceptions
{
    public class ExamTypeNotFoundException : NotFoundException
    {
        public ExamTypeNotFoundException(int id)
            : base($"The exam type with id: {id} could not found!")
        {
        }
    }
}
