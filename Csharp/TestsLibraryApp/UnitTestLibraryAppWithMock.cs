using library_domain.Exception;
using library_domain.Models;
using library_domain.Services;
using library_infra.IRepositories;
using library_infra.Repositories;
using Library_service.IServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Xunit;

namespace TestProjectBook;

public class UnitTestLibraryAppWithMock
{
    private readonly ILibrary _library;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    public static List<Book> books = new();
    public UnitTestLibraryAppWithMock()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _library = new Library(_bookRepositoryMock.Object);
    }

    [Fact]
    public void findBookISBN968787565445()
    {
        // Arrange
        var isbn = new Isbn(968787565445);
        var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, null, null);
        _bookRepositoryMock.Setup(x => x.save(book));
        _bookRepositoryMock.Setup(x => x.findBook(isbn.IsbnCode)).Returns(book);

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
        _bookRepositoryMock.Setup(x => x.findBook(isbn.IsbnCode)).Returns((Book)null);

        // Act & Assert
        Assert.Throws<BookNotAvailableException>(() => _library.findBook(isbn.IsbnCode));
    }

    [Fact]
    public void member_can_borrow_a_book_if_book_is_available()
    {
        // Arrange
        var isbn = new Isbn(1234567890);
        var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling", isbn, null, null);
        _bookRepositoryMock.Setup(x => x.save(book));
        _bookRepositoryMock.Setup(x => x.findBook(isbn.IsbnCode)).Returns(book);
        var member = new Resident(1, "John", "Doe", "123 Main St", "Resident member", 100, Profil.Resident);

        // Act
        var result = _library.borrowBook(isbn.IsbnCode, member, new DateOnly(2023, 4, 16));

        // Assert
        Assert.Equal(book, result);
        Assert.Equal(new DateOnly(2023, 4, 16), result.BorrowedAt);
    }

}
