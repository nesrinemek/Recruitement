using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_infra.Models;

/// <summary>
/// A simple representation of a book
/// </summary>
public class Book
{
    public string title { get; set; }
    public string author { get; set; }
    public Isbn isbn { get; set; }
    public DateOnly? borrowedAt{ get; set; }
    public DateOnly? expectedReturnDate { get; set; }

    public Book()
    {
    }

    public Book(string Title, string Author, Isbn Isbn,DateOnly? borrowedAt, DateOnly? expectedReturnDate)
    {
        this.title = Title;
        this.author = Author;
        this.isbn = Isbn;
        this.borrowedAt = borrowedAt;
        this.expectedReturnDate = expectedReturnDate;
    }
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Book))
        {
            return false;
        }

        Book book = (Book)obj;

        return this.title == book.title && this.author == book.author && this.isbn == book.isbn && this.borrowedAt == book.borrowedAt && this.expectedReturnDate == book.expectedReturnDate;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        if (this.isbn != null)
        {
            hash = (hash * 23) + this.isbn.GetHashCode();
        }
        if (this.title != null)
        {
            hash = (hash * 23) + this.title.GetHashCode();
        }
        if (this.author != null)
        {
            hash = (hash * 23) + this.author.GetHashCode();
        }
        if (this.borrowedAt.HasValue)
        {
            hash = (hash * 23) + this.borrowedAt.Value.GetHashCode();
        }
        if (this.expectedReturnDate.HasValue)
        {
            hash = (hash * 23) + this.expectedReturnDate.Value.GetHashCode();
        }
        return hash;
    }

}
