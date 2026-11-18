using BLL.Interfaces;
using Contracts.DTOs;
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
    }
}
