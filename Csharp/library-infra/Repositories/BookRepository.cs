
using library_infra.IRepositories;
using library_infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public Dictionary<Book, Member> borrowings { get; set; }
    public Dictionary<Isbn, Book> availableBooks { get; set; }
    public BookRepository()
    {
        availableBooks = new Dictionary<Isbn, Book>();
        borrowings = new Dictionary<Book, Member>();
    }


    public void saveAll(IList<Book> books)
    {
        foreach (var book in books)
        {
            save(book);
        }
    }

    public Book findBook(long isbnCode)
    {
        // Chercher le livre avec l'isbnCode envoyé
        foreach (Isbn isbn in availableBooks.Keys)
        {
            if (isbn.IsbnCode == isbnCode)
            {
                return availableBooks[isbn];
            }
        }
        return null;
    }

    public void save(Book book)
    {
        var isbn = book.isbn;
        if (!availableBooks.ContainsKey(isbn))
        {
            availableBooks.Add(isbn, book);
        }
       
    }

}
