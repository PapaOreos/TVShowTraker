using TVShowTraker.Models;
using System.Data.Entity;

namespace Helpers.Contexts
{
    public  interface IApplicationDbContext : IDisposable
    {
        DbSet<Genre> Genress { get; }
        int SaveChanges();
        void MarkAsModified(Genre item);
    }
}
