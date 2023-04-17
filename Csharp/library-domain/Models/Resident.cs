using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace library_domain.Models;
public class Resident : Member
{
    
    public Resident(int id, string firstName, string lastName, string address, string description, decimal wallet, Profil profil) :
        base(id, firstName, lastName, address, description, wallet, profil)
    {
    }
    public override decimal payBook(int numberOfDays)
    {
        decimal amount;
        if (numberOfDays <= Profil.MaxPeriod)
        {
            // Le tarif riverain est de 10 centimes par jour (0.10 eu) pour les 60 premiers jours
            amount = numberOfDays * Profil.Rate;
        }
        else
        {
            // Le tarif riverain est de 20 centimes par jour (0.20 eu) au-delà de 60 jours
            amount = Profil.MaxPeriod * Profil.Rate + (numberOfDays - Profil.MaxPeriod) * Profil.LateRate;
        }
        if (amount < Wallet)
        {
            setWallet(Wallet - amount);
        }
        else
        {
            throw new  System.Exception("dont have amount in wallet !!!"); 
        }
        return amount;
    }

   

}
