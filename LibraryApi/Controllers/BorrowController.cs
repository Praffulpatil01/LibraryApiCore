using LibraryApi.Dtos;
using LibraryApi.Modal;
using LibraryApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly BorrowService _borrowService;
        public BorrowController (BorrowService borrowService)
        {
            _borrowService = borrowService;
        }
        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook(BorrowDto borrowDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail(400, "Invalid request data"));

            try
            {
                var (success, msg) = await _borrowService.Borrow(borrowDto);
                if (!success)
                    return BadRequest(ApiResponse<string>.Fail(400, msg));

                return Ok(ApiResponse<string>.Success(200, msg, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(500, $"Internal server error: {ex.Message}"));
            }
        }
        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook(ReturnDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail(400, "Invalid request data"));

            try
            {
                var (success, msg) = await _borrowService.ReturnBook(dto);
                if (!success)
                    return BadRequest(ApiResponse<string>.Fail(400, msg));

                return Ok(ApiResponse<string>.Success(200, msg, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(500, $"Internal server error: {ex.Message}"));
            }
        }
    }
}
