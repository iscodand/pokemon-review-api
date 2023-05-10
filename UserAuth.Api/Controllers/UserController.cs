using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using UserAuth.Api.Data.DTOs.Request;
using UserAuth.Api.Data.DTOs.Response;
using UserAuth.Api.Interfaces;
using UserAuth.Api.Models;

namespace UserAuth.Api.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // /api/v1/User/Register/
        [HttpPost("Register")]
        [ProducesResponseType(201, Type = typeof(UserResponse))]
        [ProducesResponseType(400, Type = typeof(UserResponse))]
        public IActionResult RegisterUser([FromBody] RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                UserResponse? result = _userRepository.CreateUser(userDTO);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Error while creating user. Verify and try again.");
        }

        // /api/v1/User/Login/
        [HttpPost("Login")]
        [ProducesResponseType(200, Type = typeof(TokenResponse))]
        [ProducesResponseType(400, Type = typeof(TokenResponse))]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                TokenResponse? result = await _userRepository.LoginUser(userDTO);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Error while login user. Verify and try again.");
        }

        // /api/v1/User/Login/Refresh
        [HttpPost("Login/Refresh/")]
        [ProducesResponseType(200, Type = typeof(TokenResponse))]
        [ProducesResponseType(400, Type = typeof(TokenResponse))]
        public async Task<ActionResult> RefreshToken([FromBody] Token tokenModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.RefreshToken(tokenModel);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Error while refresh user. Verify and try again.");
        }
    }
}
