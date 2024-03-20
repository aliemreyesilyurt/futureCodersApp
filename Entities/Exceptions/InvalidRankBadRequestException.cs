namespace Entities.Exceptions
{
    public class InvalidRankBadRequestException : BadRequestException
    {
        public InvalidRankBadRequestException()
            : base($"Rank should be 0 or 1.")
        {
        }
    }
}
