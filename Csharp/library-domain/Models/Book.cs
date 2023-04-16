using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_domain.Models;

/// <summary>
/// A simple representation of a book
/// </summary>
public class Book
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public Isbn Isbn { get; private set; }
    public DateOnly? BorrowedAt{ get;  private set; }
    public Member? Member { get; private set; }

     public void SetMember(Member member)
    {
        Member= member;
        member.AddBook(this);
    }

    public Book(string title, string author, Isbn isbn,DateOnly? borrowedAt, Member member)
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        BorrowedAt = borrowedAt;
        Member = member;
    }
    public override int GetHashCode() => HashCode.Combine(Title.GetHashCode(), Author.GetHashCode(), Isbn.GetHashCode());

    public void setBorrowedAt(DateOnly borrowedAt)
    {
        BorrowedAt = borrowedAt;
    }
}
