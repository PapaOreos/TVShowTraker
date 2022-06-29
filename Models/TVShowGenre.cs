namespace TVShowTraker.Models
{
    public class TVShowGenre
    {
        public int TVShowId { get; set; } = 0;
        public virtual TVShow TVShow { get; set; } = new TVShow();
        public int GenreId { get; set; } = 0;
        public virtual Genre Genre { get; set; } = new Genre();
    }
}
