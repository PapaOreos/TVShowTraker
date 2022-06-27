using TVShowTraker.Models.Contexts;

namespace TVShowTrakerTests
{
    public abstract class BaseTests<T> where T : class
    {
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {

        }
    }
}