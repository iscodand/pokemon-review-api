using UserAuth.Api.Data.DTOs.Request;
using UserAuth.Api.Data.DTOs.Response;
using UserAuth.Api.Models;

namespace UserAuth.Api.Interfaces
{
    public interface IUserRepository
    {
        public UserResponse CreateUser(RegisterUserDTO userDTO);
        public Task<TokenResponse> LoginUser(LoginUserDTO userDTO);
        public Task<TokenResponse> RefreshToken(Token tokenModel);
    }
}
