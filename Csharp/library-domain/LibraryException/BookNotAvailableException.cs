using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_domain.Exception;

public class BookNotAvailableException : FormatException
{
    public BookNotAvailableException() : base($"Book Not Available: ")
    {
    }
}
