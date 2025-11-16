using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Modal
{
    public class Borrow
    {
        [Key]
        public int BorrowId { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public string ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
