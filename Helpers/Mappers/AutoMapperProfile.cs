using AutoMapper;
using TVShowTraker.Helpers.Mappers;
using TVShowTraker.Models;
using TVShowTraker.Models.ViewModels;

namespace TVShowTraker.Helpers.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Episode <-> EpisodeVM
            CreateMap<Episode, EpisodeVM>().ReverseMap();


            //Genre <-> GenreVM
            CreateMap<Genre, GenreVM>().ReverseMap();


            //TVShow <-> TVShowVM
            CreateMap<TVShow, TVShowVM>().ReverseMap();


            CreateMap<GenreVM, TVShowGenre>().ConvertUsing(new CustomMapp_Genre_TVShowGenre());
            CreateMap<TVShowGenre,GenreVM>().ConvertUsing(new CustomMapp_TVShowGenre_Genre());

            CreateMap<TVShow, TVShowCSV>().ReverseMap();

        }
    }
}
