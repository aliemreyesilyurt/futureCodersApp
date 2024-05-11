namespace Entities.RequestFeatures
{
    public class BlogParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }
        public BlogParameters()
        {
            OrderBy = "id";
        }
    }
}
