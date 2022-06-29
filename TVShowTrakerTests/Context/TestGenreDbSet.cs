using Microsoft.EntityFrameworkCore;
using TVShowTraker.Models;

namespace TVShowTrakerTests.Context
{
    class TestGenreDbSet : TestDbSet<Genre>
    {
        public override Genre Find(params object[] keyValues)
        {
            return this.SingleOrDefault(product => product.Id == (int)keyValues.Single());
        }

        //public static implicit operator DbSet<Genre>(TestGenreDbSet v)
        //{
        //    var z = new TestDbSet<Genre>();
        //    return z;
        //    throw new NotImplementedException();
        //}
    }
}
