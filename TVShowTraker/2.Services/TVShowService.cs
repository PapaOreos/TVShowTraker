using AutoMapper;
using TVShowTraker.Exceptions;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;
namespace TVShowTraker.Services
{
    public class TVShowService : IBaseService<TVShow, TVShowVM>
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TVShowService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponseModel Create(TVShow model)
        {
            var result = _context.Set<TVShow>().Update(model);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelCreateError, typeof(TVShow).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelCreateSuccess, typeof(TVShow).Name),
                Status = ExceptionMessages.Success
            };
        }

        public ResponseModel Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(string.Format(ExceptionMessages.ModelIdInvalid, typeof(TVShow).Name));

            var model = GetById(id);
            var result = _context.Set<TVShow>().Remove(model);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelDeleteError, typeof(TVShow).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelDeleteSuccess, typeof(TVShow).Name),
                Status = ExceptionMessages.Success
            };
        }

        public TVShowVM Get(int id)
        {
            var model = GetById(id);

            return _mapper.Map<TVShowVM>(model);
        }

        public List<TVShowVM> GetAll()
        {
            List<TVShowVM> vmList = new List<TVShowVM>();
            var modelList = _context.Set<TVShow>().ToList();
            modelList.ForEach(model => vmList.Add(_mapper.Map<TVShowVM>(model)));

            return vmList;
        }

        public ResponseModel Update(TVShow model)
        {
            var savedModel = GetById(model.Id);
            if (savedModel == null)
                throw new AppException(string.Format(ExceptionMessages.ModelNotExist, typeof(TVShow).Name));

            savedModel.Description = model.Description;
            savedModel.Rate = model.Rate;
            savedModel.Name = model.Name;
            savedModel.Permalink = model.Permalink;
            savedModel.Url = model.Url;
            savedModel.Description = model.Description;
            savedModel.StartDate = model.StartDate;
            savedModel.EndDate = model.EndDate;
            savedModel.Country = model.Country;
            savedModel.Status = model.Status;
            savedModel.Runtime = model.Runtime;
            savedModel.Network = model.Network;
            savedModel.YoutubeLink = model.YoutubeLink;
            savedModel.ImagePath = model.ImagePath;
            savedModel.ImageThumbnailPath = model.ImageThumbnailPath;
            savedModel.Rate = model.Rate;
            savedModel.RateCount = model.RateCount;
            savedModel.Geners = model.Geners;
            savedModel.Episodes = model.Episodes;

            var result = _context.Set<TVShow>().Update(savedModel);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelUpdateError, typeof(TVShow).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelUpdateSuccess, typeof(TVShow).Name),
                Status = ExceptionMessages.Success
            };
        }

        public TVShow GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException(string.Format(ExceptionMessages.ModelIdInvalid, typeof(TVShow).Name));

            var TVShow = _context.Set<TVShow>().Find(id);

            if (TVShow == null)
                throw new AppException(string.Format(ExceptionMessages.ModelNotExist, typeof(TVShow).Name));

            return TVShow;
        }
    }
}
