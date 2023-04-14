using library_infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_domain.IServices;

public interface IStudentService
{
    public decimal payBook(int numberOfDays, Student student);
}
