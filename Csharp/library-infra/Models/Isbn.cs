using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_infra.Models;

/// <summary>
/// 
/// </summary>
public class Isbn
{
    public long IsbnCode { get; set; }

    public Isbn(long isbnCode)
    {
        IsbnCode = isbnCode;
    }

}
