
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
        //// Création des livres
        //Book book1 = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", new Isbn(5), null, null);
        //Book book2 = new Book("The Lord of the Rings", "J.R.R. Tolkien", new Isbn(6), null, null);
        //Book book3 = new Book("To Kill a Mockingbird", "Harper Lee", new Isbn(7), new DateOnly(2023, 04, 13), null);
        //Book book4 = new Book("Kill a king", "Harper Lee", new Isbn(8), null, null);

        //// Création de la bibliothéque
        //foreach (Book book in new Book[] { book1, book2, book3, book4 })
        //{
        //    availableBooks.Add(book.isbn, book);
        //}

        //// Création des membres
        //Member member1 = new Member { id = 1, firstName = "John", lastName = "Doe", address = "123 Main St", description = "Resident member", wallet = 100, type = "Resident" };
        //Member member2 = new Member { id = 2, firstName = "Jane", lastName = "Boo", address = "456 Elm St", description = "Student member", wallet = 50, type = "Student" };

        //// Emprunt des livres
        ////borrowings.Add(book, member1);
        //borrowings.Add(book2, member2);
        //borrowings.Add(book3, member1);

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
