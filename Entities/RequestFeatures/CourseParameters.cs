namespace Entities.RequestFeatures
{
    public class CourseParameters : RequestParameters
    {
        public bool? IsRequire { get; set; }

        public int? RankId { get; set; }
        public bool ValidRank => RankId.Equals(1) || RankId.Equals(2) || RankId.Equals(null);
    }
}
