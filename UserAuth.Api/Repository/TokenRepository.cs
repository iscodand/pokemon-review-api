using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserAuth.Api.Interfaces;

namespace UserAuth.Api.Repository
{
    public class TokenRepository : ITokenRepository
    {
        public IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SymmetricSecurityKey authSigningKey()
        {
            SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));
            return authSigningKey;
        }

        public JwtSecurityToken GenerateAccessToken(List<Claim> claims)
        {
            // Try to convert AccessTokenValidityInMinutes -> int
            _ = int.TryParse(_configuration["JWTSettings:AccessTokenValidityInMinutes"], out
                int tokenValidityInMinutes);

            // Generate JWT Access Token
            JwtSecurityToken? token = new(
                claims: claims,
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                signingCredentials: new SigningCredentials(authSigningKey(), SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = authSigningKey(),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokenHandler = new();

            ClaimsPrincipal? principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token.");

            return principal;
        }
    }
}
