using AutoMapper;
using CsvHelper;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using TVShowTraker.Exceptions;
using TVShowTraker.Helpers.Pagination;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.Filters;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;
namespace TVShowTraker.Services
{
    public class TVShowService : BaseService<TVShow, TVShowVM, TVShowFilter>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly GenreService _genreService;
        private readonly IMemoryCache _memoryCache;
        private readonly static string CacheTVShow = "TVShows";

        public TVShowService(
            ApplicationDbContext context,
            IMapper mapper,
            IMemoryCache memoryCache) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _genreService = new GenreService(_context, _mapper);
            _memoryCache = memoryCache;
        }

        public override PagedResult<TVShowVM> GetAllWithPagination(TVShowFilter filter)
        {
            var list = GetAll();
            if (!string.IsNullOrEmpty(filter.TVShowName))
                list = list.ToList().Where(t => t.Name.Contains(filter.TVShowName)).ToList();
            if (filter.GenreId > 0)
                list = list.Where(t => t.Genres.Any(g => g.GenreId == filter.GenreId)).ToList();

            var vmList = new List<TVShowVM>();
            list.ToList().ForEach(item =>
            {
                vmList.Add(_mapper.Map<TVShowVM>(item));
            });

            return GetPagedService.GetPaged(vmList.AsQueryable(), filter.CurrentPage, filter.PageSize);
        }

        public override List<TVShow> GetAll()
        {
            #region Try get Values from Chache
            var output = _memoryCache.Get<List<TVShow>>(CacheTVShow);
            if (output is not null)
            {
                return output;
            }

            #endregion
            var list = base.GetAll();
            list.ForEach(tvshow =>
            {
                tvshow.Episodes = GetEpisodesByTVShowId(tvshow.Id);
                tvshow.Genres = GetGenresByTVShowId(tvshow.Id);
            });

            #region MemoryCacheEntryOptions
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(9999));
            var set = _memoryCache.Set(CacheTVShow, list, cacheEntryOptions);
            //var cacheOptions = new MemoryCacheEntryOptions()
            //        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            //_memoryCache.Set(CacheTVShow, output, cacheOptions); 
            #endregion

            return list;
        }

        public override TVShowVM GetVM(int id)
        {
            var model = Get(id);

            model.Episodes = GetEpisodesByTVShowId(id);
            model.Genres = GetGenresByTVShowId(id);

            return _mapper.Map<TVShowVM>(model);
        }

        public override ResponseModel CreateVM(TVShowVM viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            CreateGenresIfNeeded(viewModel.Genres);

            var model = GetParsedModelFromVM(viewModel);

            return base.Create(model);
        }

        public override ResponseModel UpdateVM(TVShowVM viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            CreateGenresIfNeeded(viewModel.Genres);

            var model = GetParsedModelFromVM(viewModel);
            return base.Update(model);
        }

        private List<Episode> GetEpisodesByTVShowId(int id) =>
            _context.Set<Episode>().Where(e => e.TVShow.Id == id).ToList();

        private ICollection<TVShowGenre> GetGenresByTVShowId(int id)
        {
            var genres = _context.Set<TVShowGenre>().Where(x => x.TVShow.Id == id).ToList();
            genres.ForEach(g => g.Genre = _genreService.Get(g.GenreId));

            return genres;
        }

        private void CreateGenresIfNeeded(ICollection<GenreVM> genres)
        {
            genres.ToList().ForEach(genre =>
            {
                if (_genreService.GetByGenreDescription(genre.Description) == null)
                {
                    _genreService.CreateVM(genre);
                }
            });
        }

        private TVShow GetParsedModelFromVM(TVShowVM viewModel)
        {
            var model = new TVShow()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Permalink = viewModel.Permalink,
                Url = viewModel.Url,
                Description = viewModel.Description,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                Country = viewModel.Country,
                Status = viewModel.Status,
                Runtime = viewModel.Runtime,
                Network = viewModel.Network,
                YoutubeLink = viewModel.YoutubeLink,
                ImagePath = viewModel.ImagePath,
                ImageThumbnailPath = viewModel.ImageThumbnailPath,
                Rating = viewModel.Rating,
                RateCount = viewModel.RateCount,
            };
            model.Episodes = new List<Episode>();
            viewModel.Episodes.ForEach(e => model.Episodes.Add(_mapper.Map<Episode>(e)));
            model.Genres = new List<TVShowGenre>();
            viewModel.Genres.ForEach(g =>
                model.Genres.Add(
                    new TVShowGenre()
                    {
                        Genre = _genreService.GetByGenreDescription(g.Description),
                        TVShow = model
                    }));

            return model;
        }


        public void ExportTVShowToCSV(int tvShowId)
        {
            try
            {
                var exportModel = _mapper.Map<TVShowCSV>(base.Get(tvShowId));
                if (exportModel == null)
                    return;
                string path = Directory.GetCurrentDirectory();
                string newPath = Path.GetFullPath(Path.Combine(path, @"..\CSV\"));
                string file = Path.Combine(newPath, @"TVShow_" + DateTime.Now.ToString("ddMMyyyy_hhmm") + ".csv");
                using (var writer = new StreamWriter(file))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteHeader<TVShowCSV>();
                    csvWriter.WriteRecords(new List<TVShowCSV>() { exportModel });
                }
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }



        public Task CreateAsync(List<TVShow> showsWithDetails)
        {
            try
            {
                showsWithDetails.ForEach(show =>
                {
                    Create(show);
                });
                return Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
