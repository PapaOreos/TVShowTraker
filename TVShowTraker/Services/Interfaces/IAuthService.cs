using TVShowTraker.Models;
using TVShowTraker.Models.Auth;

namespace TVShowTraker.Services.Interfaces
{
    public interface IAuthService
    {
        AuthenticationResponse Authenticate(Login model);
        User GetById(int id);
        ResponseModel Register(Register register);
    }
}
