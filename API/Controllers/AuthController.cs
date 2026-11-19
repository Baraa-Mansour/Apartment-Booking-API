using BLL.Interfaces;
using Contracts.DTOs.auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Register([FromBody]RegisterRequest register)
        {
            await _authService.RegisterAsync(register);

            return NoContent();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]LoginRequest login)
        {
            LoginResponse response = await _authService.LoginAsync(login);

            return Ok(response);
        }
    }
}
