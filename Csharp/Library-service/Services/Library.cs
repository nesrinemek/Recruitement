using library_domain.Exception;
using library_domain.Models;
using library_infra.IRepositories;
using Library_service.IServices;

namespace library_domain.Services;

public class Library : ILibrary
{
   
   
    private readonly IBookRepository _bookRepository;

    public Library(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
   
    public Book findBook(long isbnCode)
    {
        Book book = null;
        book = _bookRepository.findBook(isbnCode);

        if (book == null)
        {
            throw new BookNotAvailableException();
        }

        return book;
    }
    public void saveAllBook(IList<Book> books)
    {
        foreach (Book book in books)
        {
            long isbn = book.Isbn.IsbnCode;
            var availableBook = _bookRepository.findBook(isbn);
            if (availableBook is null)
            {
                _bookRepository.save(book);
            }
            else
            {
                throw new AlreadyExistBookException(book.Isbn);

            }

        }
    }
    public Book borrowBook(long isbnCode, Member member, DateOnly BorrowedAt)
    {
        var memberF = _bookRepository.findMember(member.Id);
        if (memberF is not null && memberF.IsLate)
        {
            throw new HasLateBooksException();
        }
        var book = _bookRepository.findBook(isbnCode);
        if(book is not null)
        {
            book.setBorrowedAt(BorrowedAt);
            _bookRepository.saveBookBorrow(book, member);
           
        }
        else
        {
            throw new BookNotAvailableException();
        }
        return book;
    }
    public decimal returnBook(Book book, Member member)
    {
        int numberOfDays = member.NumberOfDaysBorrowed(book);
        _bookRepository.returnBook(book);
        return member.payBook(numberOfDays);
         
    }

   
}
