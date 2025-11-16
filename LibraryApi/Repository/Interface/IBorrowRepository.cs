using LibraryApi.Dtos;
using LibraryApi.Modal;

namespace LibraryApi.Repository.Interface
{
    public interface IBorrowRepository
    {
        Task<Borrow?> GetBorrowRecordById(int borrowId);
        Task AddAsync(Borrow borrow);
        Task SaveAsync();
        Task<List<Borrow>> GetActiveBorrowByMemberAndBook(int memberId, int bookId);
        Task<List<OverdueDto>> GetOverdueAsync(int overdueDays);

        Task<List<(int BookId, int BorrowCount)>> GetTopBorrowedAsync(int topN);
    }
}
