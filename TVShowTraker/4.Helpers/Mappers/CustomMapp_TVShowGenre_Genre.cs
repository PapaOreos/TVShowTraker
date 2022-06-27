using AutoMapper;
using TVShowTraker.Models;
using TVShowTraker.Models.ViewModels;

namespace TVShowTraker.Helpers.Mappers
{
    public class CustomMapp_TVShowGenre_Genre : ITypeConverter<TVShowGenre, GenreVM>
    {
        public GenreVM Convert(TVShowGenre source, GenreVM destination, ResolutionContext context)
        {
            return new GenreVM()
            {
                Description = source.Genre.Description,
                Id = source.Genre.Id
            };
        }
    }
}
