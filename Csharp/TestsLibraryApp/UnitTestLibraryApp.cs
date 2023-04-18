using library_domain.Exception;
using library_domain.Models;
using library_domain.Services;
using library_infra.IRepositories;
using library_infra.Repositories;
using Library_service.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Xunit;

namespace TestProjectBook
{
    public class UnitTestLibraryApp
    { 
        public ILibrary _library;
        private readonly IBookRepository _bookRepository;
        public static List<Book> books = new(); 
        public UnitTestLibraryApp()
        {
            _bookRepository=new BookRepository();
            _library = new Library(_bookRepository);
        }
        [Fact]
        public void findBookISBN968787565445()
        {
            // Arrange
            var isbn = new Isbn(968787565445);
            var book = new  Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, null, null);
            _bookRepository.save(book);

            // Act
            var result = _library.findBook(isbn.IsbnCode);

            // Assert
            Assert.Equal(book, result);
        }
        [Fact]
        public void FindBook_WhenBookDoesNotExist_ThrowsException()
        {
            // Arrange
            var isbn = new Isbn(1234567890);
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, null, null);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _library.findBook(isbn.IsbnCode));
        }
        [Fact]
        public void member_can_borrow_a_book_if_book_is_available()
        {
            // Arrange
            var isbn = new Isbn(1234567890);
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, null, null);
            _bookRepository.save(book);
            var member = new Resident(1, "John", "Doe", "123 Main St", "Resident member", 100, Profil.Resident);

            // Act
            var result = _library.borrowBook(isbn.IsbnCode, member, new DateOnly(2023, 4, 16));

            // Assert
            Assert.Equal(member, result.Member);
            Assert.Equal(new DateOnly(2023, 4, 16), result.BorrowedAt);
        }
        [Fact]
        public void borrowed_book_is_no_longer_available()
        {
            // Arrange
            var isbn = new Isbn(1234567890);
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, null, null); 
            var member2 = new Resident(1, "John", "Doe", "123 Main St", "Resident member", 100, Profil.Resident);
            _bookRepository.save(book);
            _library.borrowBook(isbn.IsbnCode, member2, new DateOnly(2023, 4, 16));

            // Act & Assert
            Assert.Throws<BookNotAvailableException>(() => _library.borrowBook(isbn.IsbnCode, member2, new DateOnly(2023, 4, 17)));
        }


    
        [Fact]
        public void residents_are_taxed_10cents_for_each_day_they_keep_a_book()
        {
            // Arrange
            var borrowDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-10));
            var isbn = new Isbn(1234567890);
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, borrowDate, null);
            var member2 = new Resident(1, "John", "Doe", "123 Main St", "Resident member", 100, Profil.Resident);


            // Act
            var amount = _library.returnBook(book, member2);

            // Assert
            Assert.Equal(1.00m, amount);
        }
        [Fact]
        public void students_pay_10_cents_the_first_30days()
        {
            // Arrange
            DateOnly borrowDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-29)); // Borrowed 29 days ago
            var isbn = new Isbn(1234567890);
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, borrowDate, null);
            Member student = new Student(1, "John", "Doe", "123 Main St", "student member", 100m, Profil.Student);

            // Act
            var amount = _library.returnBook(book, student);

            // Assert
            Assert.Equal(2.9m, amount);
        }
        [Fact]
        public void students_in_1st_year_are_not_taxed_for_the_first_15days()
        {
            // Arrange
            DateOnly borrowDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-14)); // Borrowed 14 days ago
            var isbn = new Isbn(1234567890);
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, borrowDate, null);
            Member student = new Student(1, "John", "Doe", "123 Main St", "student member", 100, Profil.Student_1_Class);

            // Act
           var amount = _library.returnBook(book, student);

            // Assert
           Assert.Equal(0M, amount);
        }
        [Fact]
        public void residents_pay_20cents_for_each_day_they_keep_a_book_after_the_initial_60days()
        {
            // Arrange
            DateOnly borrowDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-61)); // Borrowed 61 days ago
            var isbn = new Isbn(1234567890);
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, borrowDate, null);
            Member resident = new Resident(1, "John", "Doe", "123 Main St", "Resident member", 1000, Profil.Resident);

            // Act
            decimal amount = _library.returnBook(book, resident);

            // Assert
            Assert.Equal(6.20M, amount);
        }
        [Fact]
        public void members_cannot_borrow_book_if_they_have_late_books()
        {
            // Arrange
            DateOnly borrowDate1 = DateOnly.FromDateTime(DateTime.Today.AddDays(-31)); // Borrowed 31 days ago
            DateOnly borrowDate2 = DateOnly.FromDateTime(DateTime.Today.AddDays(-10)); // Borrowed 10 days ago
            var isbn1 = new Isbn(1234567890);
            var isbn2 = new Isbn(1234567890);
            var book1 = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn1, null, null);
            var book2 = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn2, null, null);
            _bookRepository.save(book1);
            _bookRepository.save(book2);

            Member student = new Student(1, "John", "Doe", "123 Main St", "Resident member", 100, Profil.Student);

            // Act
            _library.borrowBook(isbn1.IsbnCode, student, borrowDate1);

            Assert.Throws<HasLateBooksException>(() => _library.borrowBook(isbn2.IsbnCode, student, borrowDate2));

        }

    }
}
