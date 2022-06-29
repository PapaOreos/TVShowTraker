using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFromEpisodate.ImportModels
{
    internal class MostPopularShow
    {
        public int id { get; set; } = 0;
        public string name { get; set; } = string.Empty;
        public string permalink { get; set; } = string.Empty;
        public string start_date { get; set; } = string.Empty;
        public string? end_date { get; set; } = null;
        public string country { get; set; } = string.Empty;
        public string network { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string image_thumbnail_path { get; set; } = string.Empty;
    }

    internal class MostPopularShowResponse
    {
        public string total { get; set; } = string.Empty;
        public int page { get; set; } = 0;
        public int Pages { get; set; } = 0;
        public List<MostPopularShow> tv_shows { get; set; } = new List<MostPopularShow>();
    }
}
