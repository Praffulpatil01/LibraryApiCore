using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [Required]
        public string ISBN { get; set; } = null!;

        public int? PublishedYear { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableCopies { get; set; }
    }
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public int? PublishedYear { get; set; }
        public int AvailableCopies { get; set; }
        public string Availability { get; set; } = null!;
    }
}
