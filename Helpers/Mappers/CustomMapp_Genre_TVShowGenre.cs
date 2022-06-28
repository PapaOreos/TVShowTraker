using AutoMapper;
using TVShowTraker.Models;
using TVShowTraker.Models.ViewModels;

namespace TVShowTraker.Helpers.Mappers
{
    public class CustomMapp_Genre_TVShowGenre : ITypeConverter<GenreVM, TVShowGenre>
    {
        public TVShowGenre Convert(GenreVM source, TVShowGenre destination, ResolutionContext context)
        {
            var modelConverted = new TVShowGenre();
            modelConverted.Genre = new Genre()
            {
                Description = source.Description,
                Id = source.Id
            };
            return modelConverted;
        }
    }
}
