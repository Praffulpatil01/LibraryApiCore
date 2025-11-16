using LibraryApi.Dtos;
using LibraryApi.Modal;
using LibraryApi.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryApi.Service
{
    public class AuthService
    {

        private readonly IMemberRepository _memberRepo;
        private readonly JwtSettings _jwt;
        private readonly PasswordHasher<Member> _hasher;

        public AuthService(IMemberRepository memberRepo, IOptions<JwtSettings> jwtOptions)
        {
            _memberRepo = memberRepo;
            _jwt = jwtOptions.Value;
            _hasher = new PasswordHasher<Member>();
        }
        public async Task<(bool Success, string Token, string Message)> LoginAsync(LoginDto dto)
        {
            var member = await _memberRepo.GetByEmailAsync(dto.Email);
            if (member == null) return (false, string.Empty, "Invalid credentials");

            var verify = _hasher.VerifyHashedPassword(member, member.PasswordHash ?? "", dto.Password);
            if (verify == PasswordVerificationResult.Failed) return (false, string.Empty, "Invalid credentials");

            var token = GenerateToken(member);
            return (true, token, "Login successful");
        }

        private string GenerateToken(Member member)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, member.MemberId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, member.Email),
                new Claim("name", member.Name ?? string.Empty)
                // add role claim if you have roles
            };

            var expires = DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
