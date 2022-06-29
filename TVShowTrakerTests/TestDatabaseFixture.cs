using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;

namespace TVShowTrakerTests
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Server=localhost; User ID=sa; Password=emCal+2021; Database=TVShowTrakerTests; MultipleActiveResultSets=true;";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        var tvshow = new TVShow
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
                            RateCount = 1566
                        };

                        context.AddRange(

                            new Genre { Description = "Drama" },
                            new Genre { Description = "Science-Fiction" },
                            new Genre { Description = "Action" },

                            tvshow,

                            new Episode { Season = 1, EpisodeNumber = 1, Name = "Pilot", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 2, Name = "Fastest Man Alive", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 3, Name = "Things You Can't Outrun", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 4, Name = "Going Rogue", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 5, Name = "Plastique", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 6, Name = "The Flash Is Born", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 7, Name = "Power Outage", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 8, Name = "Flash vs. Arrow", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 9, Name = "The Man in the Yellow Suit", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 10, Name = "Revenge of the Rogues", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 11, Name = "The Sound and the Fury", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 12, Name = "Crazy for You", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 13, Name = "The Nuclear Man", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 14, Name = "Fallout", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 15, Name = "Out of Time", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 16, Name = "Rogue Time", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 17, Name = "Tricksters", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 18, Name = "All Star Team Up", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 19, Name = "Who Is Harrison Wells?", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 20, Name = "The Trap", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 21, Name = "Grodd Lives", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 22, Name = "Rogue Air", TVShow = tvshow },
                            new Episode { Season = 1, EpisodeNumber = 23, Name = "Fast Enough", TVShow = tvshow }
                            );
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public ApplicationDbContext CreateContext()
            => new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);
    }
}
