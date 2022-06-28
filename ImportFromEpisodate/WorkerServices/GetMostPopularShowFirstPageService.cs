using ImportFromEpisodate.ImportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportFromEpisodate.WorkerServices
{
    internal class GetMostPopularShowFirstPageService
    {
        private static readonly string URL = "https://www.episodate.com/api/most-popular?page=1";

        internal Task<List<MostPopularShow>> GetShows()
        {
            HttpClient client = new HttpClient();
            var url = new Uri(URL);
            var result = client.GetAsync(url);
            var list = new List<MostPopularShow>();

            throw new NotImplementedException();
        }
    }
}
