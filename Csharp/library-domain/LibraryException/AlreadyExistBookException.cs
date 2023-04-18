using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_domain.Exception;

public class AlreadyExistBookException : FormatException
{
    public AlreadyExistBookException(object key) : base($"Book Already Exist: {key}")
    {
    }
}
