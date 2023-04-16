using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace library_infra.Models;

/// <summary>
/// A simple representation of a member
/// </summary>
public  class Member
{
    private readonly HashSet<Book> books = new HashSet<Book>();
    public Member(int id, string firstName, string lastName, string address, string description, decimal wallet, Profil profil)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Description = description;
        Wallet = wallet;
        Profil = profil;
    }

    public int Id { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public string Address { get; protected set; }
    public string Description { get; protected set; }
    /// <summary>
    /// An initial sum of money the member has
    /// </summary>
    public decimal Wallet { get; private protected set; }
    public Profil Profil { get; protected set; }



    public virtual bool IsLate {
        get
        {
            if(books.Count == 0) return false;
            int maxperiod = Profil.MaxPeriod;

            return true;
        }
    }
    //DateOnly today = DateOnly.FromDateTime(DateTime.Today);
    //    foreach (var borrowing in _bookRepository._BorrowedBooks)
    //    {
    //        if (member.Profil == "Student" && borrowing.Key.BorrowedAt.HasValue && borrowing.Key.BorrowedAt.Value.AddDays(30)< today   )
    //        {
    //            return true;
    //        }
    //        else if (member.Profil == "Resident" && borrowing.Key.BorrowedAt.HasValue  && borrowing.Key.BorrowedAt.Value.AddDays(60) < today)
    //        {
    //            return true;
    //        }

    //    }

    //    return false;

    public void setWallet(decimal wallet) => Wallet = wallet;

    public virtual void payBook(int numerOfdays)
    { }

    public int NumberOfDaysBorrowed(Book book)
    {

        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int numberOfDays = 0;
        if (book.BorrowedAt != null)
        {
            numberOfDays = today.DayNumber - book.BorrowedAt.Value.DayNumber;
        }
        return numberOfDays;
    }

    public void AddBook(Book book)
    {
        if (books.Any(b => b.Isbn == book.Isbn))
        {
            throw new Exception("Duplicate book");
        }
        books.Add(book);
    }
     
}
