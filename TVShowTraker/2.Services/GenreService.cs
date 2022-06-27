using AutoMapper;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Services
{
    public class GenreService : BaseService<Genre, GenreVM>
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

        public override ResponseModel Create(Genre model)
        {
            if(GetByGenreDescription(model.Description) != null)
            {
                return new ResponseModel(
                    string.Format(ExceptionMessages.ModelNotCreatedDueToAlreadyExistInDB, typeof(Genre).Name), 
                    ExceptionMessages.Success);
            }

            return base.Create(model);
        }

        public Genre? GetByGenreDescription(string description) =>
            _context.Set<Genre>().FirstOrDefault(x => x.Description == description);

        public override void Dispose()
        {
            _context.Dispose();
        }
    }
}
