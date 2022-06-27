using AutoMapper;
using TVShowTraker.Helpers.Mappers;
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
            //CreateMap<EpisodeVM, Episode>().ReverseMap();


            //Genre -> GenreVM
            CreateMap<Genre, GenreVM>().ReverseMap();
            //GenreVM -> Genre
            //CreateMap<GenreVM, Genre>().ReverseMap();


            //TVShow -> TVShowVM
            CreateMap<TVShow, TVShowVM>().ReverseMap();
            //TVShowVM -> TVShow
            //CreateMap<TVShowVM, TVShow>().ReverseMap();


            //TVShowVM -> TVShowVM
            CreateMap<GenreVM, TVShowGenre>().ConvertUsing(new CustomMapp_Genre_TVShowGenre());
            CreateMap<TVShowGenre,GenreVM>().ConvertUsing(new CustomMapp_TVShowGenre_Genre());
            //TVShowVM -> TVShow
            //CreateMap<TVShowVM, TVShow>().ReverseMap();

        }
    }
}
