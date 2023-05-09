using UserAuth.Api.Data.DTOs.Request;
using UserAuth.Api.Data.DTOs.Response;

namespace UserAuth.Api.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserResponse> CreateUser(RegisterUserDTO userDTO);
        public Task<TokenResponse> LoginUser(LoginUserDTO userDTO); 
        public UserResponse UserExists(RegisterUserDTO userDTO);
    }
}
