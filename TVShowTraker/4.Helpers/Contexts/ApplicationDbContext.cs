using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TVShowTraker.Models.Auth;

namespace TVShowTraker.Models.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        DbSet<ApplicationUser> User { get; set; }
        DbSet<Episode> Episodes { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<TVShow> TVShows { get; set; }
    }
}
