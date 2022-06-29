using AutoMapper;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Helpers.Mappers;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Services;

namespace TVShowTrakerTests
{
    internal class EpisodeServiceTests
    {
        private EpisodeService _service;

        [SetUp]
        public void Init()
        {
            var context = new ApplicationDbContext();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper _mapper = mappingConfig.CreateMapper();

            _service = new EpisodeService(context, _mapper);
        }

        [Test]
        public void ShouldBePossibleCreate()
        {
            var tvShow = new TVShow()
            {
                Name = "The Flash",
                Permalink = "the-flash",
                Url = "https://www.episodate.com/tv-show/the-flash",
                Description = "Barry Allen is a Central City police forensic scientist",
                Country = "US",
                Status = "Running",
                Runtime = 60,
                Network = "The CW",
                Rating = 9.35m,
                RateCount = 1566
            };
            var episode = new Episode() { Season = 1, EpisodeNumber = 1, Name = "Pilot", TVShow = tvShow };
            var response = _service.Create(episode);

            Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");
            
            var createdModel = _service.GetAll();
            
            Assert.IsTrue(createdModel is not null, "Model shouldn't be null");
            Assert.IsTrue(createdModel.Any(x => x.Season == episode.Season && x.EpisodeNumber == episode.EpisodeNumber && x.Name == episode.Name), "Model is not valid");
        }
    }
}
