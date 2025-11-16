using LibraryApi.Dtos;
using LibraryApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;
        public AuthController(AuthService auth)
        {
            _auth = auth;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponse<string>.Fail(400, "Invalid data"));

            var (success, token, message) = await _auth.LoginAsync(dto);
            if (!success) return Unauthorized(ApiResponse<string>.Fail(401, message));

            return Ok(ApiResponse<object>.Success(200, message, new { token }));
        }
    }
}
