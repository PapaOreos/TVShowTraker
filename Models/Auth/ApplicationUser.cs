using Microsoft.AspNetCore.Identity;

namespace TVShowTraker.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<UserFavouritTVShow> Favourits { get; set; } = new List<UserFavouritTVShow>();
    }
}
