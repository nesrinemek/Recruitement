using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace library_domain.Exception;

/// <summary>
/// This exception is thrown when a member who owns late books tries to borrow another book
/// </summary>
[Serializable]
public class HasLateBooksException : WebException
{
    public int StatutHttp { get; }
    public HasLateBooksException() : base("L'utilisateur a des livres en retard")
    {
        StatutHttp = (int)HttpStatusCode.BadRequest;
    }
}