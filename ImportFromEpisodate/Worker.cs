using AutoMapper;
using ImportFromEpisodate.ImportModels;
using ImportFromEpisodate.WorkerServices;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Services;

namespace ImportFromEpisodate
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private TVShowService _service;

        public Worker(
            ILogger<Worker> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
            _logger = logger;
            _service = new TVShowService(context, mapper);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Get First Page of Most Popular TVShows in Episodate website
            var shows = await new GetMostPopularShowFirstPageService().GetShows();
            //Get TVShows with details from website
            var showsWithDetails = await new GetShowDetailsService().GetShowDetailsAsync(shows);
            //Import TVShow To our DB
            await _service.CreateAsync(showsWithDetails);
        }
    }
}