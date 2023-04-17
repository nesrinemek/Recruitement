using library_domain.Models;

namespace library_Api.Controllers;

public class ReturnBorrowedBookRequest
{
    public MemberDto member { get; set; }
    public Book Book { get; set; }

}
