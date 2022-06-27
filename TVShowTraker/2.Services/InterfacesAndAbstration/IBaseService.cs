using TVShowTraker.Models;

namespace TVShowTraker.Services.Interfaces
{
    public interface IBaseService<M> where M : BaseModel
    {
        public List<M> GetAll();
        public M Get(int id);
        public ResponseModel Create(M model);
        public ResponseModel Update(M model);
        public ResponseModel Delete(int id);
    }
}
