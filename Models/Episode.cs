
namespace TVShowTraker.Models
{
    public class Episode : BaseModel
    {
        public int Season { get; set; } = 0;
        public int EpisodeNumber { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public DateTime AirDate { get; set; }
        public virtual TVShow TVShow { get; set; } = new TVShow();
    }
}
