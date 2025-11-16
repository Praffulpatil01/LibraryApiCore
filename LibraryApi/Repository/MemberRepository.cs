using LibraryApi.Data;
using LibraryApi.Modal;
using LibraryApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryContext _context;
        public MemberRepository(LibraryContext libraryContext) 
        {
            _context = libraryContext;
        }
        public async Task AddAsync(Member member)
        {
            await _context.AddAsync(member);
        }

        public async Task<Member?> GetByEmailAsync(string email)
        {
           return await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<Member> GetMemberByIdAsync(int Id) => await _context.Members.FirstOrDefaultAsync(m => m.MemberId == Id);
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
