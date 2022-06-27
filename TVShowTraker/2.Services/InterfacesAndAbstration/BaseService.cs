using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TVShowTraker.Exceptions;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;

namespace TVShowTraker.Services.Interfaces
{
    public abstract class BaseService<M, V> : IBaseService<M>, IDisposable where M : BaseModel where V : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private DbSet<M> entities;

        public BaseService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            entities = _context.Set<M>();
        }

        #region GetAll
        public virtual List<V> GetAllVM()
        {
            List<V> vmList = new List<V>();
            var modelList = GetAll();
            modelList.ForEach(model => vmList.Add(_mapper.Map<V>(model)));

            return vmList;
        }

        public virtual List<M> GetAll() => entities.ToList();
        #endregion

        #region Get
        public virtual V GetVM(int id)
        {
            var model = Get(id);

            return _mapper.Map<V>(model);
        }

        public virtual M Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException(string.Format(ExceptionMessages.ModelIdInvalid, typeof(M).Name));

            var entity = entities.Find(id);

            if (entity == null)
                throw new AppException(string.Format(ExceptionMessages.ModelNotExist, typeof(M).Name));

            return entity;
        }
        #endregion

        #region Create
        public virtual ResponseModel Create(M model)
        {
            if (model == null || model.Id > 0)
                throw new ArgumentException();

            var result = entities.Add(model);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelCreateError, typeof(M).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelCreateSuccess, typeof(M).Name),
                Status = ExceptionMessages.Success
            };
        }

        public virtual ResponseModel CreateVM(V viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            var model = _mapper.Map<M>(viewModel);

            return Create(model);
        }
        #endregion

        #region Update
        public virtual ResponseModel Update(M model)
        {
            var savedModel = Get(model.Id);
            if (savedModel == null)
                throw new AppException(string.Format(ExceptionMessages.ModelNotExist, typeof(M).Name));

            _context.Entry(savedModel).CurrentValues.SetValues(model);

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelUpdateSuccess, typeof(M).Name),
                Status = ExceptionMessages.Success
            };
        }

        public virtual ResponseModel UpdateVM(V viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException();

            var model = _mapper.Map<M>(viewModel);

            return Update(model);
        }
        #endregion

        #region Delete
        public virtual ResponseModel Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(string.Format(ExceptionMessages.ModelIdInvalid, typeof(M).Name));

            var model = Get(id);
            var result = entities.Remove(model);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelDeleteError, typeof(M).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelDeleteSuccess, typeof(M).Name),
                Status = ExceptionMessages.Success
            };
        } 
        #endregion

        public virtual void Dispose()
        {
        }
    }
}
