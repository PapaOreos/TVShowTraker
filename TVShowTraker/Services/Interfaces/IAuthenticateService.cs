using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TVShowTraker.Services.Interfaces
{
    public interface IAuthenticateService
    {
        public JwtSecurityToken CreateToken(List<Claim> authClaims);
        public string GenerateRefreshToken();
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
