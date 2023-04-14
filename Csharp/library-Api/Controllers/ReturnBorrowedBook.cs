using library_infra.Models;

namespace library_Api.Controllers;

public class ReturnBorrowedBook
{
    public Member member { get; set; }
    public int? classe { get; set; }// a verifier!!!!!!

}
