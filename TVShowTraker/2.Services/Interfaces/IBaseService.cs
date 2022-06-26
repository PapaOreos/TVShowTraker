using TVShowTraker.Models;

namespace TVShowTraker.Services.Interfaces
{
    public interface IBaseService<M, V>
    {
        List<V> GetAll();
        V Get(int id);
        M GetById(int id);
        ResponseModel Create(M model);
        ResponseModel Update(M model);
        ResponseModel Delete(int id);
    }
}
