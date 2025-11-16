using LibraryApi.Modal;

namespace LibraryApi.Repository.Interface
{
    public interface IMemberRepository
    {
        Task<Member?> GetMemberByIdAsync(int id);
        Task<Member?> GetByEmailAsync(string email);
        Task AddAsync(Member member);
        Task SaveAsync();
    }
}
