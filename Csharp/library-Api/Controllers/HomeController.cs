using library_Api.Controllers;
using library_domain.IServices;
using library_infra.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;


namespace libraryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILibrary _library;
        public HomeController(ILibrary library)
        {
            _library = library;
        }


        // GET: Home/findBook/5
        [HttpGet("findBook/{isbnCode}")]
        public Book findBook(long isbnCode)
        {
            return _library.findBook(isbnCode);

        }

        // POST: Home/saveAllBook
        [HttpPost("saveAllBook")]
        public void saveAllBook([FromBody] IList<Book> books)
        {
            _library.saveAllBook(books);
        }


        // POST: Home/borrowBook/5
        [HttpPost("borrowBook/{isbnCode}")]
        public Book borrowBook(long isbnCode, [FromBody] BorrowBookRequest request)
        {
            DateOnly date = DateOnly.ParseExact(request.borrowedAt, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return _library.borrowBook(isbnCode, request.member, date);
        }

       
        // POST: Home/returnBook/5
        [HttpPost("returnBook")]
        public void returnBook([FromBody] ReturnBorrowedBookRequest returnBorrowedBook)
        {
             _library.returnBook( returnBorrowedBook.Book, returnBorrowedBook.member); ;
        }



    }
}
