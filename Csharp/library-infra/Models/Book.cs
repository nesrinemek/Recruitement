using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_infra.Models;

/// <summary>
/// A simple representation of a book
/// </summary>
public class Book
{
    public string Title { get; set; }// mettre les set privé 
    public string Author { get; set; }
    public Isbn Isbn { get; set; }
    public DateOnly? BorrowedAt{ get; private set; }
    public Member? Member { get; private set; }

     public void SetMember(Member member)
    {
        Member= member;
        member.AddBook(this);
    }

    public Book(string Title, string Author, Isbn Isbn,DateOnly? borrowedAt)
    {
        this.Title = Title;
        this.Author = Author;
        this.Isbn = Isbn;
        BorrowedAt = borrowedAt;
    }
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Book))
        {
            return false;
        }

        Book book = (Book)obj;

        return this.Title == book.Title && this.Author == book.Author && this.Isbn == book.Isbn ;
    }

    public override int GetHashCode() => HashCode.Combine(Title.GetHashCode(), Author.GetHashCode(), Isbn.GetHashCode());

    public void setBorrowedAt(DateOnly borrowedAt)
    {
        BorrowedAt = borrowedAt;
    }
}
