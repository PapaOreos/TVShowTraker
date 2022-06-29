using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Helpers.Mappers;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services;

namespace TVShowTrakerTests
{
    internal class TVShowServiceTests
    {
        private TVShowService _service;

        [SetUp]
        public void Init()
        {
            var context = new ApplicationDbContext();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper _mapper = mappingConfig.CreateMapper();

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            _service = new TVShowService(context, _mapper, memoryCache);
        }

        [Test]
        public void ShouldBePossibleCreate()
        {
            var tvshow = GetDefaultTVShow();
            var response = _service.CreateVM(tvshow);

            Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");
        }

        [Test]
        public void ShouldBePossibleGetById()
        {
            var tvShows = _service.GetAll();
            if (!tvShows.Any())
            {
                var tvshow = GetDefaultTVShow();
                var response = _service.CreateVM(tvshow);

                Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");
                tvShows = _service.GetAll();
            }

            var tvShow = _service.Get(tvShows.FirstOrDefault().Id);

            Assert.IsTrue(tvShow is not null, "Model shouldn't be null");
            Assert.IsTrue(tvShow.IsValid(), "Model is not valid");
        }

        [Test]
        public void ShouldBePossibleDelete()
        {
            var tvShows = _service.GetAll();
            if (!tvShows.Any())
            {
                var tvshow = GetDefaultTVShow();
                var response = _service.CreateVM(tvshow);

                Assert.IsTrue(response.Status == ExceptionMessages.Success, "Status should be success");
                tvShows = _service.GetAll();
            }

            var tvShow = _service.Get(tvShows.FirstOrDefault().Id);

            Assert.IsTrue(tvShow is not null, "Model shouldn't be null");
            Assert.IsTrue(tvShow.IsValid(), "Model is not valid");

            var deleteResponse = _service.Delete(tvShow.Id);
            Assert.IsTrue(deleteResponse.Status == ExceptionMessages.Success, "Status should be success");
        }


        private static TVShowVM GetDefaultTVShow()
        {
            return new TVShowVM()
            {
                Name = "The Flash",
                Permalink = "the-flash",
                Url = "https://www.episodate.com/tv-show/the-flash",
                Description = "Barry Allen is a Central City police forensic scientist with a reasonably happy life, despite the childhood trauma of a mysterious red and yellow being killing his mother and framing his father. All that changes when a massive particle accelerator accident leads to Barry being struck by lightning in his lab. Coming out of coma nine months later, Barry and his new friends at STAR labs find that he now has the ability to move at superhuman speed. <br>Furthermore, Barry learns that he is but one of many affected by that event, most of whom are using their powers for evil. Determined to make a difference, Barry dedicates his life to fighting such threats, as The Flash. While he gains allies he never expected, there are also secret forces determined to aid and manipulate him for their own agenda.",
                Country = "US",
                Status = "Running",
                Runtime = 60,
                Network = "The CW",
                Rating = 9.35m,
                RateCount = 1566,
                Genres = new List<GenreVM>
                {
                    new GenreVM { Description = "Drama" },
                    new GenreVM { Description = "Science-Fiction" },
                    new GenreVM { Description = "Action" }
                },
                Episodes = new List<EpisodeVM> {
                    new EpisodeVM { Season = 1, EpisodeNumber = 1, Name = "Pilot" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 2, Name = "Fastest Man Alive" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 3, Name = "Things You Can't Outrun" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 4, Name = "Going Rogue" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 5, Name = "Plastique" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 6, Name = "The Flash Is Born" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 7, Name = "Power Outage" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 8, Name = "Flash vs. Arrow" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 9, Name = "The Man in the Yellow Suit" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 10, Name = "Revenge of the Rogues" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 11, Name = "The Sound and the Fury" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 12, Name = "Crazy for You" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 13, Name = "The Nuclear Man" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 14, Name = "Fallout" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 15, Name = "Out of Time" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 16, Name = "Rogue Time" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 17, Name = "Tricksters" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 18, Name = "All Star Team Up" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 19, Name = "Who Is Harrison Wells?" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 20, Name = "The Trap" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 21, Name = "Grodd Lives" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 22, Name = "Rogue Air" },
                    new EpisodeVM { Season = 1, EpisodeNumber = 23, Name = "Fast Enough" }
                }
            };
        }
    }
}
