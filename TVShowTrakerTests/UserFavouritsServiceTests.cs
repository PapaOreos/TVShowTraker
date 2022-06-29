using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services;

namespace TVShowTrakerTests
{
    internal class UserFavouritsServiceTests
    {
        private UserFavouritTVShowService _service;
        private ApplicationDbContext _context;

        [SetUp]
        public void Init()
        {
            var context = new ApplicationDbContext();
            _service = new UserFavouritTVShowService(context);
            _context = context;
        }

        [Test]
        public void ShouldBePossibleCreate()
        {
            var user = _context.Users.FirstOrDefault();
            Assert.IsTrue(user != null, "User should exist");
            var tvShows = _context.TVShows.ToList();
            Assert.IsTrue(tvShows.Any(), "TVShows should exist");

            var favourit = new FavouritRequest() { ApplicationUserId = new Guid(user.Id), TVShowId = tvShows.FirstOrDefault().Id };
            var response = _service.AddFavourit(favourit);

            Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");
        }

        [Test]
        public void ShouldBePossibleGetAllFavourits()
        {
            var user = _context.Users.FirstOrDefault();
            Assert.IsTrue(user != null, "User should exist");
            var favourits = _service.GetUserFavourits(new Guid(user.Id));

            if (!favourits.Any())
            {
                var tvShows = _context.TVShows.ToList();
                Assert.IsTrue(tvShows.Any(), "TVShows should exist");

                var favourit = new FavouritRequest() { ApplicationUserId = new Guid(user.Id), TVShowId = tvShows.FirstOrDefault().Id };
                var response = _service.AddFavourit(favourit);
                Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");

                favourits = _service.GetUserFavourits(new Guid(user.Id));
            }

            Assert.IsTrue(favourits is not null, "Model shouldn't be null");
            Assert.IsTrue(favourits.Any(), "Model is empty");
        }

        [Test]
        public void ShouldBePossibleGetRemoveAllFavourits()
        {
            var user = _context.Users.FirstOrDefault();
            Assert.IsTrue(user != null, "User should exist");
            var favourits = _service.GetUserFavourits(new Guid(user.Id));

            if (!favourits.Any())
            {
                var tvShows = _context.TVShows.ToList();
                Assert.IsTrue(tvShows.Any(), "TVShows should exist");

                var favourit = new FavouritRequest() { ApplicationUserId = new Guid(user.Id), TVShowId = tvShows.FirstOrDefault().Id };
                var response = _service.AddFavourit(favourit);
                Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");

                favourits = _service.GetUserFavourits(new Guid(user.Id));
            }

            var deleteResponse = _service.RemoveAllFavourits(new Guid(user.Id));
            Assert.IsTrue(deleteResponse.Status == ExceptionMessages.Success, "Status should be success");
        }
    }
}
