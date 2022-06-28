using ImportFromEpisodate.ImportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowTraker.Models;

namespace ImportFromEpisodate.WorkerServices
{
    internal class GetShowDetailsService
    {
        private static readonly string URL = "https://www.episodate.com/api/show-details?q=";

        internal Task<List<TVShow>> GetShowDetailsAsync(List<MostPopularShow> shows)
        {
            HttpClient client = new HttpClient();

            shows.ForEach(show =>
            {
                var url = new Uri(URL + show.id);
                var result = client.GetAsync(url);
            });

            throw new NotImplementedException();
        }
    }
}
