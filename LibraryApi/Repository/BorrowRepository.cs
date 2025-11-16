using LibraryApi.Data;
using LibraryApi.Dtos;
using LibraryApi.Modal;
using LibraryApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repository
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly LibraryContext _context;
        public BorrowRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Borrow borrow)
        {
           await _context.BorrowRecords.AddAsync(borrow);
        }

        public async Task<List<Borrow>> GetActiveBorrowByMemberAndBook(int memberId, int bookId)
        {
            return await _context.BorrowRecords
            .Where(br => br.MemberId == memberId && br.BookId == bookId && br.IsReturned == false)
            .ToListAsync();
        }

        public async Task<Borrow?> GetBorrowRecordById(int borrowId)
        {
            return await _context.BorrowRecords.FirstOrDefaultAsync(b => b.BorrowId == borrowId);
        }

        public async Task<List<(int BookId, int BorrowCount)>> GetTopBorrowedAsync(int topN)
        {
            var result = await _context.BorrowRecords
             .GroupBy(x => x.BookId)
             .Select(g => new { g.Key, Count = g.Count() })
             .OrderByDescending(x => x.Count)
             .Take(topN)
             .ToListAsync();

            return result.Select(x => (x.Key, x.Count)).ToList();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<OverdueDto>> GetOverdueAsync(int overdueDays)
        {
            var today = DateTime.UtcNow.Date;

            var q = from br in _context.BorrowRecords.AsQueryable()
                    join m in _context.Members on br.MemberId equals m.MemberId
                    join b in _context.Books on br.BookId equals b.BookId
                    where !br.IsReturned
                          && EF.Functions.DateDiffDay(br.BorrowDate, today) > overdueDays
                    select new OverdueDto
                    {
                        BorrowId = Convert.ToInt32(br.BorrowId),
                        MemberId = Convert.ToInt32(m.MemberId),
                        MemberName = m.Name,
                        MemberEmail = m.Email,
                        BookId = Convert.ToInt32(b.BookId),
                        BookTitle = b.Title,
                        BorrowDate = br.BorrowDate,
                        DaysOverdue = EF.Functions.DateDiffDay(br.BorrowDate, today) - overdueDays
                    };

            return await q.OrderByDescending(x => x.BorrowDate).ToListAsync();
        }
    }
}
