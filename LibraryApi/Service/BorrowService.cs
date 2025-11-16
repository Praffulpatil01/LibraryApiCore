using LibraryApi.Dtos;
using LibraryApi.Modal;
using LibraryApi.Repository.Interface;

namespace LibraryApi.Service
{
    public class BorrowService
    {
        private readonly IBorrowRepository _borrowRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IMemberRepository _memberRepo;
        public BorrowService( IBorrowRepository repo, IBookRepository bookRepo, IMemberRepository memberRepo)
        {
            _borrowRepo = repo;
            _bookRepo = bookRepo;
            _memberRepo = memberRepo;
        }
        public async Task<(bool Success, string Message)> Borrow(BorrowDto dto)
        {
            var member = await _memberRepo.GetMemberByIdAsync(dto.MemberId);
            if (member == null) return (false, "Member not found");

            var book = await _bookRepo.GetByIdAsync(dto.BookId);
            if (book == null) return (false, "Book not found");

            if (book.AvailableCopies <= 0)
                return (false, "No copies available");

            var active = await _borrowRepo.GetActiveBorrowByMemberAndBook(dto.MemberId, dto.BookId);
            if (active.Any()) return (false, "Already borrowed");

            var borrow = new Borrow
            {
                MemberId = dto.MemberId,
                BookId = dto.BookId,
                BorrowDate = DateTime.UtcNow.Date,
                IsReturned = false
            };

            await _borrowRepo.AddAsync(borrow);
            book.AvailableCopies--;

            await _borrowRepo.SaveAsync();
            await _bookRepo.SaveAsync();

            return (true, "Borrow successful");
        }

        public async Task<(bool Success, string Message)> ReturnBook(ReturnDto dto)
        {
            var record = await _borrowRepo.GetBorrowRecordById(dto.BorrowId);
            if (record == null) return (false, "Record not found");

            if (record.IsReturned) return (false, "Already returned");

            record.IsReturned = true;
            record.ReturnDate = Convert.ToString(DateTime.UtcNow.Date);

            var book = await _bookRepo.GetByIdAsync(record.BookId);
            book!.AvailableCopies++;

            await _borrowRepo.SaveAsync();
            await _bookRepo.SaveAsync();

            return (true, "Return successful");
        }

        public async Task<object> TopBorrowed()
        {
            var top = await _borrowRepo.GetTopBorrowedAsync(5);
            var books = await _bookRepo.GetAllBooks();

            return top.Join(
                books,
                t => t.BookId,
                b => b.BookId,
                (t, b) => new { b.Title, b.Author, t.BorrowCount }
            );
        }

        public async Task<object> Overdue(int overdueDays = 14)
        {
            var overdue = await _borrowRepo.GetOverdueAsync(overdueDays);

            return overdue.Select(o => new
            {
                o.BorrowId,
                Member = o.MemberName,
                Email = o.MemberEmail,
                Book = o.BookTitle,
                o.BorrowDate,
                OverdueDays = (DateTime.UtcNow.Date - o.BorrowDate.Date).Days - overdueDays
            });
        }
    }
}
