using LibraryApi.Dtos;
using LibraryApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly BorrowService _Service;
        public ReportsController(BorrowService reportsService)
        {
            _Service = reportsService;
        }
        [HttpGet("top-borrowed")]
        public async Task<IActionResult> GetTopBorrowed()
        {
            var result = await _Service.TopBorrowed();

            if (result == null)
            {
                return NotFound(ApiResponse<string>.Fail(404, "No borrowed books found"));
            }
            return Ok(ApiResponse<object>.Success(200, "Top borrowed books retrieved successfully", result));
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdue(int overdueDays = 14)
        {
            var result = await _Service.Overdue(overdueDays);
            if (result == null)
            {
                return NotFound(ApiResponse<object>.Fail(404, "No overdue records found"));
            }
            return Ok(ApiResponse<object>.Success(200, "Overdue records retrieved successfully", result));
        }
    }
}
