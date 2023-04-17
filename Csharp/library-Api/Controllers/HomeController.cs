using library_Api.Controllers;
using library_domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;
using Library_service.IServices;
using AutoMapper;


namespace libraryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILibrary _library;
        private readonly IMapper _mapper;
        public HomeController(ILibrary library, IMapper mapper)
        {
            _library = library;
            _mapper = mapper;
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

            Member? member = GetMember(request.member);

            return _library.borrowBook(isbnCode, member, date);
        }



        // POST: Home/returnBook
        [HttpPost("returnBook")]
        public decimal returnBook([FromBody] ReturnBorrowedBookRequest returnBorrowedBook)
        {
            Member? member = GetMember(returnBorrowedBook.member);
            return _library.returnBook( returnBorrowedBook.Book, member  ); ;
        }

        private Member? GetMember(MemberDto requestMember)
        {
            Member? member = null;
            switch (requestMember.Profil.Name.ToString())
            {
                case "Resident":
                    member = _mapper.Map<Resident> (requestMember);
                    break;
                case "Student":
                    member = _mapper.Map<Student>(requestMember);
                    break;
                default:
                    break;
            }

            return member;
        }


    }
}
