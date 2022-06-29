
using AutoMapper;
using Helpers.Contexts;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.Filters;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Services
{
    public class EpisodeService : BaseService<Episode, EpisodeVM, EpisodeFilter>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EpisodeService(
            ApplicationDbContext context,
            IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
