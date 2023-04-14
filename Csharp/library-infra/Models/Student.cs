using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_infra.Models;

public class Student: Member
{
    public int classe { get; set; }
    public Student()
    {
        type = "Student";
    }

}
