using System.ComponentModel.DataAnnotations.Schema;
using TVShowTraker.Models.Auth;

namespace TVShowTraker.Models
{
    public class UserFavouritTVShow: BaseModel
    {
        [ForeignKey("ApplicationUserId")]
        public Guid ApplicationUserId { get; set; }
        [ForeignKey("TVShowId")]
        public int TVShowId { get; set; } = 0;
    }
}
