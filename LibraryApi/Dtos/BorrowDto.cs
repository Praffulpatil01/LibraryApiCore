using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class BorrowDto
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int BookId { get; set; }
    }
    public class ReturnDto
    {
        [Required]
        public int BorrowId { get; set; }
    }

    public class OverdueDto
    {
        public int BorrowId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public string MemberEmail { get; set; } = null!;
        public int BookId { get; set; }
        public string BookTitle { get; set; } = null!;
        public DateTime BorrowDate { get; set; }
        public int DaysOverdue { get; set; }
    }
}
