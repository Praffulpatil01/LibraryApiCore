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
    public class MemberController : ControllerBase
    {
        private readonly MemberService _memberService;
        public MemberController(MemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpPost("addmembers")]
        public async Task<IActionResult> AddMembers(CreateMemberDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.Fail(400, "Invalid request data"));

            try
            {
                var (success, msg) = await _memberService.AddMember(dto);
                if (!success)
                {
                    return BadRequest(ApiResponse<string>.Fail(400, msg ?? "Something went wrong. Please try again"));
                }

                return StatusCode(201, ApiResponse<object>.Success(201, "Member created successfully", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(500, $"Internal server error: {ex.Message}"));
            }
        }

    }
}
