using library_infra.Models;

namespace library_Api.Controllers;

public class ReturnBorrowedBookRequest
{
    public Member member { get; set; }
    public Book Book { get; set; }

}
