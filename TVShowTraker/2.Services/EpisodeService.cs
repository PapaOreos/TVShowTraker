
using AutoMapper;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Services
{
    public class EpisodeService : BaseService<Episode, EpisodeVM>
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

        public override void Dispose()
        {
            _context.Dispose();
        }
    }
}
