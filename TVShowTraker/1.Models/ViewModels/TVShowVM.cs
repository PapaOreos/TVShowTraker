namespace TVShowTraker.Models.ViewModels
{
    public class TVShowVM
    {
        public string Name { get; set; } = string.Empty;
        public string Permalink { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
        public string Country { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Runtime { get; set; } = string.Empty;
        public string Network { get; set; } = string.Empty;
        public string? YoutubeLink { get; set; } = null;
        public string ImagePath { get; set; } = string.Empty;
        public string ImageThumbnailPath { get; set; } = string.Empty;
        public decimal Rate { get; set; } = 0m;
        public int RateCount { get; set; } = 0;
        //public string? CountDown { get; set; } = null;
        public List<Genre> Geners { get; set; } = new List<Genre>();
        public List<string> Pictures { get; set; } = new List<string>();
        public List<Episode> Episodes { get; set; } = new List<Episode>();
    }
}
