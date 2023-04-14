using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_infra.Models;

/// <summary>
/// A simple representation of a member
/// </summary>
public class Member
{
    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string address { get; set; }
    public string description { get; set; }
    /// <summary>
    /// An initial sum of money the member has
    /// </summary>
    public float wallet { get; set; }
    public string type { get; set; }


}
