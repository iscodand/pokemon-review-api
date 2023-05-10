using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UserAuth.Api.Interfaces
{
    public interface ITokenRepository
    {
        public string GenerateRefreshToken();
        public JwtSecurityToken GenerateAccessToken(List<Claim> claims);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token);
    }
}
