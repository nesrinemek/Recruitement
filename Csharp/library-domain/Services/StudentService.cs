using library_domain.IServices;
using library_infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_domain.Services;

public class StudentService :  IStudentService
{    
    public  decimal payBook(int numberOfDays, Student student)
    {
        decimal Fee ;

        if (student.classe == 1 && numberOfDays <= 15) 
        { 
            // Les étudiants en première année ont une période gratuite de 15 jours pour chaque livre emprunté
            Fee = 0;
        }
        else
        {
            // Le tarif étudiant est de 10 centimes par jour (0.10 eu)
            Fee = numberOfDays * 0.10m;
        }
        return Fee;
    }
}
