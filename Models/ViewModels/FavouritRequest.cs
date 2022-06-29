using System.ComponentModel.DataAnnotations;

namespace TVShowTraker.Models.ViewModels
{
    public class FavouritRequest
    {
        [Required]
        public Guid ApplicationUserId { get; set; }
        [Required]
        public int TVShowId { get; set; } = 0;
    }
}
