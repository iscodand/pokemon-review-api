using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuth.Api.Data.DTOs.Request;
using UserAuth.Api.Data.DTOs.Response;
using UserAuth.Api.Interfaces;

namespace UserAuth.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserRepository(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserResponse> CreateUser(RegisterUserDTO userDTO)
        {
            IdentityUser identityUser = new()
            {
                UserName = userDTO.Username,
                Email = userDTO.Email
            };

            var result = _userManager.CreateAsync(identityUser, userDTO.Password);

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
            IdentityUser? identityUser = await _userManager.FindByEmailAsync(userDTO.Email);
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

            // Generating Access Token if user is valid
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));
            Claim[] claims = new[]
            {
                new Claim("Email", userDTO.Email),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
            };
            JwtSecurityToken? token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponse()
            {
                AccessToken = tokenAsString,
                ExpirationTime = token.ValidTo,
                IsSuccess = true
            };
        }

        public UserResponse UserExists(RegisterUserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
