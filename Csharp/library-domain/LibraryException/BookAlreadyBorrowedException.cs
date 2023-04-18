using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_domain.Exception;

public class BookAlreadyBorrowedException : FormatException
{
    public BookAlreadyBorrowedException(object key) : base($"Book Already Borrowed: {key}")
    {
    }
}
