using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool? IsAvailable { get; set; }

        public int? GenderId { get; set; }
        public Gender? Gender { get; set; }

        public int? RankId { get; set; }
        public Rank? Rank { get; set; }
    }
}
