namespace Entities.Exceptions
{
    public class StepNotFoundException : NotFoundException
    {
        public StepNotFoundException(int id)
            : base($"The step with id: {id} could not found!")
        {
        }
    }
}
