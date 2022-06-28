namespace TVShowTraker.Models.ViewModels
{
    public class EpisodeVM
    {
        public int Id { get; set; } = 0;
        public int Season { get; set; } = 0;
        public int EpisodeNumber { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public DateTime AirDate { get; set; }
    }
}
