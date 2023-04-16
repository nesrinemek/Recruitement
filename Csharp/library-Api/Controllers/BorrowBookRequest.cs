using library_infra.Models;

namespace libraryApi.Controllers;

public class BorrowBookRequest
{
    public Member member { get; set; }
    public string borrowedAt { get; set; }


}
