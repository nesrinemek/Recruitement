using library_domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_domain.Services;

public class ResidentService :  IResidentService
{
    public  decimal payBook(int numberOfDays)
    {
        decimal Fee = 0;
        if (numberOfDays <= 60)
        {
            // Le tarif riverain est de 10 centimes par jour (0.10 eu) pour les 60 premiers jours
            Fee = numberOfDays * 0.10m;
        }
        else
        {
            // Le tarif riverain est de 20 centimes par jour (0.20 eu) au-delà de 60 jours
            Fee = 60 * 0.10m + (numberOfDays - 60) * 0.20m;
        }
        return Fee;
    }
}
