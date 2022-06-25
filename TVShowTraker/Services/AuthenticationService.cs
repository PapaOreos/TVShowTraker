using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TVShowTraker.Helpers;
using TVShowTraker.Models;
using TVShowTraker.Models.Auth;
using TVShowTraker.Models.Contexts;
using TVShowTraker.Models.Mappers;
using TVShowTraker.Models.ViewModels;
using TVShowTraker.Services.Interfaces;

namespace TVShowTraker.Services
{
    public class AuthenticationService : IAuthService
    {
        private ApplicationDbContext _context;
        private IBaseService<User, UserVM> _userService;

        private readonly IOptions<AppSettings> _appSettings;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AuthenticationService(
            ApplicationDbContext context, 
            IBaseService<User, UserVM> userService,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _userService = userService;
            _appSettings = appSettings;
        }

        public AuthenticationResponse Authenticate(Login model)
        {
            var user = _context.Set<User>().SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticationResponse(user, token);
        }

        public User GetById(int id) => UserMapper.ParseVMToModel(_userService.Get(id));


        public ResponseModel Register(Register register)
        {
            var user = new User();
            user.Username = register.Username;
            user.Password = register.Password;
            user.Email = register.Email;
            
            return _userService.Create(user);
        }


        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
