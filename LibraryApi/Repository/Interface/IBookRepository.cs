using LibraryApi.Dtos;
using LibraryApi.Modal;

namespace LibraryApi.Repository.Interface
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByIdISBNAsync(string ISBN);
        Task AddAsync(Book book);
        Task SaveAsync();
    }
}
