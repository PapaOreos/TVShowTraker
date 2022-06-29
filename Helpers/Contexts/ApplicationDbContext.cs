using Helpers.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TVShowTraker.Models.Auth;

namespace TVShowTraker.Models.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TVShowGenre>().HasKey(tg => new { tg.TVShowId, tg.GenreId });

            builder.Entity<TVShowGenre>()
                .HasOne(sc => sc.TVShow)
                .WithMany()
                .HasForeignKey(sc => sc.TVShowId);
            builder.Entity<TVShowGenre>()
                .HasOne(sc => sc.Genre)
                .WithMany()
                .HasForeignKey(sc => sc.GenreId);
        }

        public void MarkAsModified(Genre item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<TVShowGenre> TVShowsGenres { get; set; }
        public DbSet<UserFavouritTVShow> UserFavourits { get; set; }

        public System.Data.Entity.DbSet<Genre> Genress { get; set; }
    }
}
