using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Modal
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public int? PublishedYear { get; set; }
        public int AvailableCopies { get; set; }
    }
}
