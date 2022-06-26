using AutoMapper;
using TVShowTraker.Models;
using TVShowTraker.Models.ViewModels;

namespace TVShowTraker._4.Helpers.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Episode -> EpisodeVM
            CreateMap<Episode, EpisodeVM>().ReverseMap();
            //EpisodeVM -> Episode
            CreateMap<EpisodeVM, Episode>().ReverseMap();


        }
    }
}
