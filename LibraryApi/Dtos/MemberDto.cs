using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class CreateMemberDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(6)] 
        public string Password { get; set; } = null!;

        public string? Phone { get; set; }
    }
    public class LoginDto
    {
        [Required][EmailAddress] public string Email { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;
    }
}
