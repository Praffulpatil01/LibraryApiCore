using LibraryApi.Dtos;
using LibraryApi.Modal;
using LibraryApi.Repository;
using LibraryApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly BookService _bookService;
        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("getbooks")]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var list = await _bookService.GetBooks();
                if(list == null)
                {
                    return NotFound(ApiResponse<object>.Success(404, "Books Not Found", list));
                }
                return Ok(ApiResponse<object>.Success(200, "Books retrieved successfully", list));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(500, $"Internal server error: {ex.Message}"));
            }
        }
        [HttpPost("addbook")]
        public async Task<IActionResult> AddBook(CreateBookDto book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail(400, "Invalid request data"));

            try
            {
                var (success, message) = await _bookService.AddBook(book);
                if (!success)
                    return Conflict(ApiResponse<string>.Fail(409, message ?? "Unable to add book"));

                return StatusCode(201, ApiResponse<object>.Success(201, "Book added successfully", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(500, $"Internal server error: {ex.Message}"));
            }
        }
    }
}
