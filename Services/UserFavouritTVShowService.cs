using Microsoft.EntityFrameworkCore;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Auth;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.InterfacesAndAbstration;

namespace TVShowTraker.Services
{
    public class UserFavouritTVShowService : IUserFavouritsTVShowService
    {
        private readonly ApplicationDbContext _context;
        private DbSet<UserFavouritTVShow> favourits;

        public UserFavouritTVShowService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            favourits = _context.Set<UserFavouritTVShow>();
        }

        public List<string> GetUserFavourits(Guid userId)
        {
            var list = new List<string>();
            var userFavourits = favourits.Where(x => x.ApplicationUserId.Equals(userId)).ToList();
            userFavourits.ForEach(f => list.Add(_context.Set<TVShow>().Find(f.TVShowId)?.Name ?? ""));
            return list;
        }

        public ResponseModel AddFavourit(FavouritRequest request)
        {
            if (request.ApplicationUserId == Guid.Empty || request.TVShowId <= 0)
                throw new ArgumentException();

            if (GetFromUserAndTVShow(request.ApplicationUserId, request.TVShowId) != null)
            {
                return new ResponseModel(
                    ExceptionMessages.FavouritAlreadyExist,
                    ExceptionMessages.Fail
                    );
            }

            favourits.Add(
                new UserFavouritTVShow()
                {
                    TVShowId = request.TVShowId,
                    ApplicationUserId = request.ApplicationUserId
                });

            _context.SaveChanges();

            return new ResponseModel(
                ExceptionMessages.FavouritAddedSuccessfully,
                ExceptionMessages.Success
                );
        }


        public ResponseModel RemoveFavourit(FavouritRequest request)
        {
            if (request.ApplicationUserId == Guid.Empty || request.TVShowId <= 0)
                throw new ArgumentException();

            var model = GetFromUserAndTVShow(request.ApplicationUserId, request.TVShowId);
            if (model == null)
            {
                return new ResponseModel(
                    string.Format(ExceptionMessages.ModelNotExist, typeof(UserFavouritTVShow).Name),
                    ExceptionMessages.Fail
                    );
            }

            favourits.Remove(model);

            _context.SaveChanges();

            return new ResponseModel(
                ExceptionMessages.FavouritRemovedSuccessfully,
                ExceptionMessages.Success
                );
        }

        public ResponseModel RemoveAllFavourits(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException();

            var toRemove = favourits.Where(x => x.ApplicationUserId.Equals(userId)).ToList();
            toRemove.ForEach(x =>
            {
                var model = GetFromUserAndTVShow(userId, x.TVShowId);
                if (model != null)
                    favourits.Remove(model);
            });

            _context.SaveChanges();

            return new ResponseModel(
                ExceptionMessages.AllFavouritsRemovedSuccessfully,
                ExceptionMessages.Success
                );
        }

        private UserFavouritTVShow? GetFromUserAndTVShow(Guid userId, int tvShowId)
        {
            return favourits.FirstOrDefault(x => x.ApplicationUserId.Equals(userId) && x.TVShowId == tvShowId);
        }
    }
}
