using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Modal
{
    public class Member
    {
        [Key]
       public int MemberId{get;set;}
       public string Name{get;set;}
       public string Email {get;set;}
       public string Phone {get;set;}
       public string PasswordHash { get;set;}
       public DateTime JoinDate { get; set; }
    }
}
