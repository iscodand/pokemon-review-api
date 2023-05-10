using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserAuth.Api.Data.DTOs.Request;
using UserAuth.Api.Data.DTOs.Response;
using UserAuth.Api.Interfaces;
using UserAuth.Api.Models;

namespace UserAuth.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _configuration;

        public UserRepository(UserManager<User> userManager,
            IConfiguration configuration,
            ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }

        public UserResponse CreateUser(RegisterUserDTO userDTO)
        {
            if (userDTO == null)
                return new UserResponse()
                {
                    Message = "User cannot be null.",
                    IsSuccess = false
                };

            User identityUser = new()
            {
                UserName = userDTO.Username,
                Email = userDTO.Email
            };

            Task<IdentityResult> result = _userManager.CreateAsync(identityUser, userDTO.Password);

            if (!result.Result.Succeeded)
            {
                return new UserResponse()
                {
                    Message = "Error while creating user.",
                    IsSuccess = false,
                    ErrorMessages = result.Result.Errors.Select(e => e.Description)
                };
            }

            return new UserResponse()
            {
                Message = "User Successfuly created.",
                IsSuccess = true
            };
        }

        public async Task<TokenResponse> LoginUser(LoginUserDTO userDTO)
        {
            User? identityUser = await _userManager.FindByEmailAsync(userDTO.Email);
            if (identityUser == null)
            {
                return new TokenResponse()
                {
                    Message = "There's no user with this Email address.",
                    IsSuccess = false
                };
            }

            bool verifyPassword = await _userManager.CheckPasswordAsync(identityUser, userDTO.Password);
            if (!verifyPassword)
            {
                return new TokenResponse()
                {
                    Message = "Incorrect password. Verify and try again.",
                    IsSuccess = false
                };
            }

            // Generating user claims
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, identityUser.Email),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
            };

            // Generating tokens
            JwtSecurityToken accessToken = _tokenRepository.GenerateAccessToken(claims);
            string refreshToken = _tokenRepository.GenerateRefreshToken();

            // Add user refresh token
            _ = int.TryParse(_configuration["JWTSettings:AccessTokenValidityInMinutes"],
                out int refreshTokenValidityInMinutes);
            identityUser.RefreshToken = refreshToken;
            identityUser.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(identityUser);

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            return new TokenResponse()
            {
                AccessToken = tokenAsString,
                RefreshToken = refreshToken,
                ExpirationTime = accessToken.ValidTo,
                IsSuccess = true
            };
        }

        public async Task<TokenResponse> RefreshToken(RefreshTokenDTO tokenDTO)
        {
            string? accessToken = tokenDTO.AccessToken;
            string? refreshToken = tokenDTO.RefreshToken;

            ClaimsPrincipal? principal = _tokenRepository.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
                return new TokenResponse()
                {
                    Message = "Invalid refresh/access token.",
                    IsSuccess = false
                };

            User? user = await _userManager.FindByEmailAsync(principal.Identity.Name);

            if (user == null || user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
                return new TokenResponse()
                {
                    Message = "Invalid refresh/access token.",
                    IsSuccess = false
                };

            // Generating new tokens
            JwtSecurityToken newAccessToken = _tokenRepository.GenerateAccessToken(principal.Claims.ToList());
            string newAccessTokenToString = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
            
            string newRefreshToken = _tokenRepository.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            
            await _userManager.UpdateAsync(user);

            return new TokenResponse()
            {
                AccessToken = newAccessTokenToString,
                RefreshToken = newRefreshToken,
                ExpirationTime = newAccessToken.ValidTo,
                IsSuccess = true
            };
        }
    }
}
