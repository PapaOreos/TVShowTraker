using ImportFromEpisodate.ImportModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ImportFromEpisodate
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Get First Page of Most Popular TVShows in Episodate website
                var shows = await GetMostPopularShowFirstPageServiceAsync();

                //Get TVShows with details from website
                if(shows != null)
                {
                    _ = await GetShowDetailsFromShowAsync(shows.FirstOrDefault().id);
                    _ = await GetShowDetailsFromShowAsync(shows.LastOrDefault().id);
                }
                //var showsWithDetails = await GetShowDetailsFromShowsAsync(shows);
                //Import TVShow To our DB
                //await _service.CreateAsync(showsWithDetails);
            }
        }

        private async Task<ImportTVShow> GetShowDetailsFromShowAsync(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://www.episodate.com/api/show-details?q=" + id);

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseModel = JsonConvert.DeserializeObject<ImportTVShow>(responseContent);
                    return responseModel;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        private async Task<List<ImportTVShow>> GetShowDetailsFromShowsAsync(List<MostPopularShow> shows)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //var list = new List<ImportTVShow>();
                    //shows.ForEach(async show =>
                    //{
                    //    HttpResponseMessage response = await client.GetAsync("https://www.episodate.com/api/show-details?q=" + show.id);

                    //    var responseContent = await response.Content.ReadAsStringAsync();
                    //    var responseModel = JsonConvert.DeserializeObject<ImportTVShow>(responseContent);
                    //    list.Add(responseModel);
                    //});

                    //return list;
                    return null;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        private async Task<ImportTVShow> GetDetailsFromShowsAsync(MostPopularShow show)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://www.episodate.com/api/show-details?q=" + show.id);

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<ImportTVShow>(responseContent);

                return responseModel;
            }
        }

        private async Task<List<MostPopularShow>> GetMostPopularShowFirstPageServiceAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://www.episodate.com/api/most-popular?page=1");

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<MostPopularShowResponse>(responseContent);

                    var list = new List<MostPopularShow>();
                    user.tv_shows.ForEach(tv => {
                        list.Add(tv);
                    });

                    return list;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
        }
    }
}