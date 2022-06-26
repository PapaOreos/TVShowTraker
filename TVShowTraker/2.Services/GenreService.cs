using AutoMapper;
using TVShowTraker.Exceptions;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Services
{
    public class GenreService : IBaseService<Genre, GenreVM>
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenreService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponseModel Create(Genre model)
        {
            var result = _context.Set<Genre>().Update(model);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelCreateError, typeof(Genre).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelCreateSuccess, typeof(Genre).Name),
                Status = ExceptionMessages.Success
            };
        }

        public ResponseModel Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(string.Format(ExceptionMessages.ModelIdInvalid, typeof(Genre).Name));

            var model = GetById(id);
            var result = _context.Set<Genre>().Remove(model);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelDeleteError, typeof(Genre).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelDeleteSuccess, typeof(Genre).Name),
                Status = ExceptionMessages.Success
            };
        }

        public GenreVM Get(int id)
        {
            var model = GetById(id);

            return _mapper.Map<GenreVM>(model);
        }

        public List<GenreVM> GetAll()
        {
            List<GenreVM> vmList = new List<GenreVM>();
            var modelList = _context.Set<Genre>().ToList();
            modelList.ForEach(model => vmList.Add(_mapper.Map<GenreVM>(model)));

            return vmList;
        }

        public ResponseModel Update(Genre model)
        {
            var savedModel = GetById(model.Id);
            if (savedModel == null)
                throw new AppException(string.Format(ExceptionMessages.ModelNotExist, typeof(Genre).Name));

            savedModel.Description = model.Description;

            var result = _context.Set<Genre>().Update(savedModel);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelUpdateError, typeof(Genre).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelUpdateSuccess, typeof(Genre).Name),
                Status = ExceptionMessages.Success
            };
        }

        public Genre GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException(string.Format(ExceptionMessages.ModelIdInvalid, typeof(Genre).Name));

            var Genre = _context.Set<Genre>().Find(id);

            if (Genre == null)
                throw new AppException(string.Format(ExceptionMessages.ModelNotExist, typeof(Genre).Name));

            return Genre;
        }
    }
}
