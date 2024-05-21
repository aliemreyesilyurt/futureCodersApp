namespace Entities.RequestFeatures
{
    public class UserParameters
    {
        const int maxPageSize = 50;

        //Auto-implemented property
        public int PageNumber { get; set; }

        //Full-property
        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        public bool? IsAvailable { get; set; }
        public int? RankId { get; set; }
        public bool ValidRank => RankId.Equals(1) || RankId.Equals(2) || RankId.Equals(null);
        public string? SearchTerm { get; set; }
    }
}
