using Helpers.Contexts;
using System.Data.Entity;
using TVShowTraker.Models;

namespace TVShowTrakerTests.Context
{
    internal class TestAppContext : IApplicationDbContext
    {
        public TestAppContext()
        {
            this.Genress = new TestGenreDbSet();
        }

        public DbSet<Genre> Genress { get; set; }

        public void Dispose()
        {
        }

        public void MarkAsModified(Genre item)
        {
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
