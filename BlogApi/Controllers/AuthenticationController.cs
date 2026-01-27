using Blog.Service.Abstraction;
using Blog.Shared.DTOs.IdentityDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var result = await _authenticationService.LoginAsync(loginDto);
            if (!result.Success)
                return Unauthorized(new { errors = result.Errors ?? (result.Error is null ? [] : new[] { result.Error }) });

            return Ok(result.Value);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var result = await _authenticationService.RegisterAsync(registerDto);
            if (!result.Success)
                return BadRequest(new { errors = result.Errors ?? (result.Error is null ? [] : new[] { result.Error }) });

            return Ok(result.Value);
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _authenticationService.EmailExistsAsync(email);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _authenticationService.GetUserByEmailAsync(email!);
            if (!result.Success)
                return NotFound(new { errors = result.Errors ?? (result.Error is null ? [] : new[] { result.Error }) });
            return Ok(result.Value);
        }


    }
}
