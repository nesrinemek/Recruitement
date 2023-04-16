
using library_domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_infra.IRepositories;

public interface IBookRepository
{ 
    public  void saveAll(IList<Book> books);
    public Book findBook(long isbnCode);
    public void save(Book book);

    void saveBookBorrow(Book book, Member member);
    void returnBook(Book book);

    IEnumerable<Book> GetAvailableBooks();
}
