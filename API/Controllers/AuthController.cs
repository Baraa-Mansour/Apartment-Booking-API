using API.Common;
using BLL.Interfaces;
using Contracts.DTOs.auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Tags("Authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>Registers a new user account.</summary>
        /// <param name="register">The registration details.</param>
        /// <response code="204">The account was created successfully.</response>
        /// <response code="400">The request is invalid.</response>
        /// <response code="409">The account already exists.</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Register([FromBody] RegisterRequest register)
        {
            await _authService.RegisterAsync(register);

            return NoContent();
        }

        /// <summary>Authenticates a user and returns access tokens.</summary>
        /// <param name="login">The login credentials.</param>
        /// <response code="200">Authentication succeeded.</response>
        /// <response code="400">The request is invalid.</response>
        /// <response code="401">The credentials are invalid.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginRequest login)
        {
            LoginResponse response = await _authService.LoginAsync(login);

            return Ok(ApiResponse<LoginResponse>.Ok(response));
        }
    }
}
