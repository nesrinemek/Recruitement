using library_domain.Models;

namespace libraryApi.Controllers;

public class BorrowBookRequest
{
    public MemberDto member { get; set; }
    public string borrowedAt { get; set; }


}
