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
    
    public override int GetHashCode()
    {
        return IsbnCode.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Isbn))
        {
            return false;
        }

        Isbn isbn = (Isbn)obj;

        return this.IsbnCode == isbn.IsbnCode; 
    }
}
