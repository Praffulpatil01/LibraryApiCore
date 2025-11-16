using LibraryApi.Dtos;
using LibraryApi.Modal;
using LibraryApi.Repository.Interface;
using Microsoft.AspNetCore.Identity;

namespace LibraryApi.Service
{
    public class MemberService
    {
        private readonly IMemberRepository _memberRepo;
        private readonly PasswordHasher<Member> _hasher;
        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepo = memberRepository;
            _hasher = new PasswordHasher<Member>();
        }

        public async Task<(bool Success, string? Message)> AddMember(CreateMemberDto dto)
        {
            var exists = await _memberRepo.GetByEmailAsync(dto.Email);
            if (exists != null)
                return (false, "Email already exists");

            var member = new Member
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                JoinDate = DateTime.UtcNow,
            };
            member.PasswordHash = _hasher.HashPassword(member, dto.Password);
            await _memberRepo.AddAsync(member);
            await _memberRepo.SaveAsync();

            return (true, null);
        }
    }
}
