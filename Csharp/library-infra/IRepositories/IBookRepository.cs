﻿using library_infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_infra.IRepositories;

public interface IBookRepository
{
    public Dictionary<Isbn, Book> availableBooks { get; }
    public Dictionary<Book, Member> borrowings { get; }
    public  void saveAll(IList<Book> books);
    public Book findBook(long isbnCode);
    public void save(Book book);

}
