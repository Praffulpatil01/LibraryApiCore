using LibraryApi.Dtos;
using LibraryApi.Modal;
using LibraryApi.Repository;
using LibraryApi.Repository.Interface;

namespace LibraryApi.Service
{
    public class BookService
    {
        private readonly IBookRepository _bookRepo;
        public BookService(IBookRepository ibookRepo)
        {
            _bookRepo = ibookRepo;
        }
        public async Task<(bool Success, string? Message)> AddBook(CreateBookDto dto)
        {
            if (dto.AvailableCopies < 0)
                return (false, "Negative copies not allowed");

            var exists = await _bookRepo.GetByIdISBNAsync(dto.ISBN);
            if (exists != null)
                return (false, "ISBN already exists");

            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                PublishedYear = dto.PublishedYear,
                AvailableCopies = dto.AvailableCopies
            };
            await _bookRepo.AddAsync(book);
            await _bookRepo.SaveAsync();

            return (true, null);
        }

        public async Task<List<BookDto>> GetBooks()
        {
            var books = await _bookRepo.GetAllBooks();

            return books.Select(b => new BookDto
            {
                BookId = Convert.ToInt32(b.BookId),
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                PublishedYear = b.PublishedYear,
                AvailableCopies = b.AvailableCopies,
                Availability = b.AvailableCopies > 0 ? "Available" : "Not Available"
            }).ToList();
        }
    }
}
