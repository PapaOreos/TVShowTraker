using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFromEpisodate.ImportModels
{
    internal class ImportTVShow
    {
        public string Name { get; set; } = string.Empty;
        public string Permalink { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
        public string Country { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Runtime { get; set; } = 0;
        public string Network { get; set; } = string.Empty;
        public string? YoutubeLink { get; set; } = null;
        public string ImagePath { get; set; } = string.Empty;
        public string ImageThumbnailPath { get; set; } = string.Empty;
        public decimal Rating { get; set; } = 0m;
        public int RateCount { get; set; } = 0;
        //public string? CountDown { get; set; } = null;
        public List<string> Genres { get; set; } = new List<string>();
        //public List<string> Pictures { get; set; } = new List<string>();
        //public List<EpisodeVM> Episodes { get; set; } = new List<EpisodeVM>();
    }
}
