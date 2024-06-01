namespace Entities.Exceptions
{
    public class InvalidRankBadRequestException : BadRequestException
    {
        public InvalidRankBadRequestException()
            : base($"Rank should be 1 (Alfa) or 2 (Beta).")
        {
        }
    }
}
