
using TVShowTraker.Models;
using TVShowTraker.Models.ViewModels;

namespace TVShowTraker.Services.InterfacesAndAbstration
{
    public interface IUserFavouritsTVShowService
    {
        public List<string> GetUserFavourits(Guid userId);
        public ResponseModel AddFavourit(FavouritRequest request);
        public ResponseModel RemoveFavourit(FavouritRequest request);
        public ResponseModel RemoveAllFavourits(Guid userId);
    }
}
