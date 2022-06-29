using AutoMapper;
using Helpers.Contexts;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.Filters;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Services
{
    public class GenreService : BaseService<Genre, GenreVM, GenreFilter>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenreService(
            ApplicationDbContext context,
            IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public GenreService(
        //    IApplicationDbContext context,
        //    IMapper mapper
        //    ):base(new ApplicationDbContext(), mapper)
        //{
        //    _context = context;
        //    _mapper = mapper;
        //}

        public override ResponseModel Create(Genre model)
        {
            if(GetByGenreDescription(model.Description) != null)
            {
                return new ResponseModel(
                    string.Format(ExceptionMessages.ModelNotCreatedDueToAlreadyExistInDB, typeof(Genre).Name), 
                    ExceptionMessages.Fail);
            }

            return base.Create(model);
        }

        public Genre? GetByGenreDescription(string description) =>
            _context.Genres.FirstOrDefault(x => x.Description == description);
    }
}
