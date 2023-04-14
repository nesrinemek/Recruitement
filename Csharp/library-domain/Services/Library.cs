using library_domain.Exception;
using library_domain.IServices;
using library_infra.Models;
using library_infra.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library_infra.Repositories;

namespace library_domain.Services;

public class Library : ILibrary
{
   
    private readonly IResidentService _residentService;
    private readonly IStudentService _studentService;
    private readonly IBookRepository _bookRepository;

    public Library(IResidentService residentService, IStudentService studentService, IBookRepository bookRepository)
    {
        _residentService = residentService;
        _studentService = studentService;
        _bookRepository = bookRepository;

    }
    public void initializeDatabase()
    {
        // Création des livres
        Book book1 = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", new Isbn(5), null, null);
        Book book2 = new Book("The Lord of the Rings", "J.R.R. Tolkien", new Isbn(6), null, null);
        Book book3 = new Book("To Kill a Mockingbird", "Harper Lee", new Isbn(7), new DateOnly(2023, 04, 13), null);
        Book book4 = new Book("Kill a king", "Harper Lee", new Isbn(8), null, null);

        // Création de la bibliothéque
        foreach (Book book in new Book[] { book1, book2, book3, book4 })
        {
            _bookRepository.availableBooks.Add(book.isbn, book);
        }

        // Création des membres
        Member member1 = new Member { id = 1, firstName = "John", lastName = "Doe", address = "123 Main St", description = "Resident member", wallet = 100, type = "Resident" };
        Member member2 = new Member { id = 2, firstName = "Jane", lastName = "Boo", address = "456 Elm St", description = "Student member", wallet = 50, type = "Student" };

        // Emprunt des livres
        //_bookRepository.borrowings.Add(book, member1);
        _bookRepository.borrowings.Add(book2, member2);
        _bookRepository.borrowings.Add(book3, member1);

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
            var isbn = book.isbn;
            if (!_bookRepository.availableBooks.ContainsKey(isbn))
            {
                _bookRepository.save(book);
            }
            else
            {
                throw new AlreadyExistBookException(book.isbn);

            }

        }
    }
    public Book borrowBook(long isbnCode, Member member, DateOnly borrowedAt)
    {
        Book bookToBorrow = new Book();
        bool isBookAvailable = false;

        // Chercher le livre avec l'isbnCode
        foreach (Isbn isbn in _bookRepository.availableBooks.Keys)
        {
            if (isbn.IsbnCode == isbnCode)
            {
                isBookAvailable = true;
                bookToBorrow = _bookRepository.availableBooks[isbn];
                break;
            }
        }
        if (isBookAvailable) // si le livre est disponible dans le dictionnaire _bookRepository.availableBooks
        {
            DateOnly? expectedReturnDate = null;


            if (!CanBorrowBooks(member))//"L'utilisateur a des livres en retard
            {
                throw new HasLateBooksException();
            }
            if (bookToBorrow.borrowedAt != null)// Livre déjà emprunté
            {
                throw new BookAlreadyBorrowedException(isbnCode);
            }

            // Vérifier si l'adhérent est autorisé à emprunter des livres
            else if (member.type== "Student")
            {
                var student = new Student
                {
                   address= member.address,
                  firstName= member.firstName,
                   lastName= member.lastName,
                   classe = 2 ,
                };

                // Vérifier si l'adhérent est un étudiant en première année 
                if (student.classe == 1)
                {
                    expectedReturnDate = borrowedAt.AddDays(15);

                    // Mise à jour de la date d'emprunt et la date de retour prévue
                    bookToBorrow.borrowedAt = borrowedAt;
                    bookToBorrow.expectedReturnDate = expectedReturnDate;
                    //Lui emprenter le livre
                    _bookRepository.borrowings.Add(bookToBorrow, student);
                    return bookToBorrow;

                }
                else // L'adhérent un étudiant mais pas en première année
                {
                    // Calculer la date de retour prévue pour le livre emprunté
                    expectedReturnDate = borrowedAt.AddDays(30);

                    // Mise à jour de la date d'emprunt et la date de retour prévue
                    bookToBorrow.borrowedAt = borrowedAt;
                    bookToBorrow.expectedReturnDate = expectedReturnDate;
                    //Lui emprenter le livre
                    _bookRepository.borrowings.Add(bookToBorrow, member);
                    return bookToBorrow;

                }
            }
            else if (member.type == "Resident" )
            {
                expectedReturnDate = borrowedAt.AddDays(60);
                // Mise à jour de la date d'emprunt et la date de retour prévue
                bookToBorrow.borrowedAt = borrowedAt;
                bookToBorrow.expectedReturnDate= expectedReturnDate;
                //Lui emprenter le livre
                _bookRepository.borrowings.Add(bookToBorrow, member);
                return bookToBorrow;

            }
        }
       
        throw new InvalidOperationException("Le livre n'exsite pas dans la ibliothèque");


    }
    public decimal returnBook(long isbnCode, Member member, int? classe)
    {
        Book BookBorrowed = new Book();
        bool isBookBorrowed = false;

        // Chercher le livre avec l'isbnCode
        foreach (Book book in _bookRepository.borrowings.Keys)
        {
            if (book.isbn.IsbnCode == isbnCode)
            {
                isBookBorrowed = true;
                BookBorrowed = book;
                break;
            }
        }

        if (!isBookBorrowed)
        {
            throw new InvalidOperationException("Le livre n'a pas été emprunté");
        }

        if (_bookRepository.borrowings[BookBorrowed].id != member.id)
        {
            throw new InvalidOperationException("Le livre a été emprunté par un autre membre");
        }
        if (_bookRepository.availableBooks.ContainsKey(BookBorrowed.isbn) && BookBorrowed.borrowedAt == null)
        {
            throw new AlreadyExistBookException(BookBorrowed.isbn);
        }
        if (IsMemberLate(member))
        {
            // Lever une exception si le membre est en retard
            throw new HasLateBooksException();
        }

        var Fees = CalculateFees(member, BookBorrowed, classe);
        if (Fees > member.wallet)
        {
            throw new InvalidOperationException("ton budget n'est pas suffisant"); //a verifier!!!!!!
        }

        // Mettre à jour les données du livre
        BookBorrowed.borrowedAt = null;
        BookBorrowed.expectedReturnDate = null;
        _bookRepository.borrowings.Remove(BookBorrowed);
        return Fees;
    }


    // Méthode pour vérifier si un membre est en retard sur le retour d'un livre emprunté (autres livres)
    private bool IsMemberLate(Member member) // autres livres le membre 
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        foreach (var borrowing in _bookRepository.borrowings)
        {
            if (member.type == "Student" && borrowing.Key.borrowedAt.HasValue && borrowing.Key.borrowedAt.Value.AddDays(30)< today   )
            {
                return true;
            }
            else if (member.type == "Resident" && borrowing.Key.borrowedAt.HasValue  && borrowing.Key.borrowedAt.Value.AddDays(60) < today)
            {
                return true;
            }

        }

        return false;

    }

    // Méthode pour calculer les frais
    private decimal CalculateFees(Member member, Book book, int? classe)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int numberOfDays = 0;
        if (book.borrowedAt != null)
        {
             numberOfDays = today.DayNumber - book.borrowedAt.Value.DayNumber ; 
        }
        decimal fees = 0;
        if (classe.HasValue && member.type== "Student")
        {
            var student = new Student
            {
                classe = (int)classe,
            };
            fees = _studentService.payBook(numberOfDays, student);
        }
        else if (member.type == "Resident")
        {
            fees = _residentService.payBook(numberOfDays);
        }

        // Ajoute les frais de retard à l'adhérent
        return fees;
    }

    // Méthode pour savoir si un membre peux emprunter des livres à la bibliothèque ou non
    private bool CanBorrowBooks(Member member)
    {
        if ((member.type == "Student" || member.type == "Resident") && IsMemberLate(member) == false)
        {
            return true;
        }
        return false;
    }

}
