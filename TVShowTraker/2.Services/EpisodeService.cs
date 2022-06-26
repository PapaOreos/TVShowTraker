
using AutoMapper;
using TVShowTraker.Helpers.Exceptions;
using TVShowTraker.Exceptions;
using TVShowTraker.Models;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Services
{
    public class EpisodeService : IBaseService<Episode, EpisodeVM>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EpisodeService(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ResponseModel Create(Episode model)
        {
            var result = _context.Set<Episode>().Update(model);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelCreateError, typeof(Episode).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelCreateSuccess, typeof(Episode).Name),
                Status = ExceptionMessages.Success
            };
        }

        public ResponseModel Delete(int id)
        {
            if(id <= 0)
                throw new ArgumentException(string.Format(ExceptionMessages.ModelIdInvalid, typeof(Episode).Name));

            var model = GetById(id);
            var result = _context.Set<Episode>().Remove(model);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelDeleteError, typeof(Episode).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelDeleteSuccess, typeof(Episode).Name),
                Status = ExceptionMessages.Success
            };
        }

        public EpisodeVM Get(int id)
        {
            var model = GetById(id);

            return _mapper.Map<EpisodeVM>(model);
        }

        public List<EpisodeVM> GetAll()
        {
            List<EpisodeVM> vmList = new List<EpisodeVM>();
            var modelList = _context.Set<Episode>().ToList();
            modelList.ForEach(model => vmList.Add(_mapper.Map<EpisodeVM>(model)));

            return vmList;
        }

        public ResponseModel Update(Episode model)
        {
            var savedModel = GetById(model.Id);
            if (savedModel == null)
                throw new AppException(string.Format(ExceptionMessages.ModelNotExist, typeof(Episode).Name));

            savedModel.AirDate = model.AirDate;
            savedModel.EpisodeNumber = model.EpisodeNumber;
            savedModel.Name = model.Name;

            var result = _context.Set<Episode>().Update(savedModel);

            if (result == null)
                throw new AppException(string.Format(ExceptionMessages.ModelUpdateError, typeof(Episode).Name));

            _context.SaveChanges();
            return new ResponseModel()
            {
                Message = string.Format(ExceptionMessages.ModelUpdateSuccess, typeof(Episode).Name),
                Status = ExceptionMessages.Success
            };
        }

        public Episode GetById(int id)
        {
            if(id <= 0)
                throw new ArgumentException(string.Format(ExceptionMessages.ModelIdInvalid, typeof(Episode).Name));

            var episode = _context.Set<Episode>().Find(id);
            
            if (episode == null)
                throw new AppException(string.Format(ExceptionMessages.ModelNotExist, typeof(Episode).Name));

            return episode;
        }
    }
}
