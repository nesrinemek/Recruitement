
using library_infra.IRepositories;
using library_infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace library_infra.Repositories;


/// <summary>C
/// The book repository  
/// </summary>
/// //nesrine j'ai enlever virtual des signatures des methodes
/// 
public class BookRepository : IBookRepository
{
    private readonly Dictionary<Book, Member> _BorrowedBooks = new();
    private readonly Dictionary<Isbn, Book> _AvailableBooks = new();
    public BookRepository()
    { 
    }


    public void saveAll(IList<Book> books)
    {
        foreach (var book in books)
        {
            save(book);
        }
    }

    public Book findBook(long IsbnCode)
    { 
        return _AvailableBooks.FirstOrDefault(x => x.Key.IsbnCode == IsbnCode).Value;
    }

    public void save(Book book)
    {
        var Isbn = book.Isbn;
        if (!_AvailableBooks.ContainsKey(Isbn))
        {
            _AvailableBooks.Add(Isbn, book);
        }
       
    }
    public void saveBookBorrow(Book book, Member member)
    {
        _AvailableBooks.Remove(book.Isbn);
        if (!_BorrowedBooks.ContainsKey(book))
        {
            _BorrowedBooks.Add(book, member); 
            book.SetMember(member);
        }
         
    }
    public void returnBook(Book book)
    {
        _BorrowedBooks.Remove(book);
        if(!_AvailableBooks.ContainsKey(book.Isbn))
         book.setBorrowedAt(DateOnly.MinValue);
        _AvailableBooks.Add(book.Isbn, book);

    }

    public IEnumerable<Book> GetAvailableBooks()
    {
        return _AvailableBooks.Select(x => x.Value);
    }
}
