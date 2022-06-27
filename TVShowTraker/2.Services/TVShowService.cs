using AutoMapper;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;
namespace TVShowTraker.Services
{
    public class TVShowService : BaseService<TVShow, TVShowVM>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly GenreService _genreService;

        public TVShowService(
            ApplicationDbContext context,
            IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _genreService = new GenreService(_context, _mapper);
        }

        public override List<TVShowVM> GetAllVM()
        {
            return base.GetAllVM();
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

    }
}
