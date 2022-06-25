using TVShowTraker.Models;
using TVShowTraker.Models.Auth;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.Mappers;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Services
{
    public class UserService : IBaseService<User, UserVM>
    {
        private ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserVM> GetAll()
        {
            List<UserVM> usersVM = new List<UserVM>();
            var userList = _context.Set<User>().ToList();
            userList.ForEach(user => { 
                usersVM.Add(UserMapper.ParseModelToVM(user));
            });

            return usersVM;
        }
        public UserVM Get(int id)
        {
            var user = GetUserById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            return UserMapper.ParseModelToVM(user);
        }

        public ResponseModel Create(User model)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                if (CheckIfUserAlreadyExist(model))
                {
                    throw new ApplicationException("This user already exists");
                }

                _context.Add<User>(model);
                _context.SaveChanges();
                responseModel.IsSuccess = true;
                responseModel.Message = "User created successfully";
            }
            catch (Exception)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "User not created";
                throw;
            }
            return responseModel;
        }

        public ResponseModel Update(User user)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _context.Update<User>(user);
                _context.SaveChanges();
                responseModel.IsSuccess = true;
                responseModel.Message = "User updated successfully";
            }
            catch (Exception)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "User not updated";
                throw;
            }
            return responseModel;
        }

        public ResponseModel Delete(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var user = GetUserById(id);
                if (user != null)
                {
                    _context.Remove<User>(user);
                    _context.SaveChanges();
                    responseModel.IsSuccess = true;
                    responseModel.Message = "User deleted successfully";
                }
            }
            catch (Exception)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "User not deleted";
                throw;
            }
            return responseModel;
        }

        private User? GetUserById(int id) => _context.Find<User>(id);

        private bool CheckIfUserAlreadyExist(User model)
        {
            return _context.Set<User>().Any( user => user.Email == model.Email);
        }
    }
}
