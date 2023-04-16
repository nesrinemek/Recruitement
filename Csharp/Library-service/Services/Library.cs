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
        try
        {
            book = _bookRepository.findBook(isbnCode);
        }
        catch
        {
            // Gérer l'exception
            throw new InvalidOperationException("Une exception s'est produite lors de la recherche du livre : ");

        }

        if (book == null)
        {
            // Gérer le cas où la méthode _bookRepository.findBook(isbnCode) a retourné une valeur nulle
            throw new InvalidOperationException("Le livre avec l'ISBN " + isbnCode + " n'a pas été trouvé.");
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

        //if (member.IsLate)
        //{
        //    throw new HasLateBooksException();
        //}
        var book = _bookRepository.findBook(isbnCode);
        if(book is not null)
        {
            book.setBorrowedAt(BorrowedAt);
            _bookRepository.saveBookBorrow(book, member);
           
        }
        return book;
    }
    public void returnBook(Book book, Member member)
    {
        int numberOfDays = member.NumberOfDaysBorrowed(book);
        _bookRepository.returnBook(book);
        member.payBook(numberOfDays);
         
    }

   //private void initializeDatabase()
    //{
    //    // Création des livres
    //    Book book1 = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", new Isbn(5), null, null);
    //    Book book2 = new Book("The Lord of the Rings", "J.R.R. Tolkien", new Isbn(6), null, null);
    //    Book book3 = new Book("To Kill a Mockingbird", "Harper Lee", new Isbn(7), new DateOnly(2023, 04, 13), null);
    //    Book book4 = new Book("Kill a king", "Harper Lee", new Isbn(8), null, null);
    //    var list = new List<Book>
    //    {
    //        book1,
    //        book2,
    //        book3,
    //        book4
    //    };
    //    // Création de la bibliothéque
    //    _bookRepository.saveAll(list);

    //    // Création des membres
    //    //(int id, string firstName, string lastName, string address, string description, decimal wallet, string type)
    //    Member member1 = new Resident(1, "John", "Doe", "123 Main St", "Resident member", 100,Profil.Resident);
    //    Member member2 = new Resident(2, "Jane", "Boo", "456 Main St", "Student member", 50, Profil.Student);
         
    //    // Emprunt des livres
    //    //_bookRepository.borrowings.Add(book, member1);
    //    _bookRepository.saveBookBorrow(book2, member2);
    //    _bookRepository.saveBookBorrow(book3, member1);

    //}
}
