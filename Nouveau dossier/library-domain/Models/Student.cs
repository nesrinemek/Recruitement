using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace library_domain.Models;

public class Student: Member
{
    public int classe { get; set; }
    public Student(int id, string firstName, string lastName, string address, string description, decimal wallet, Profil profil) :
        base(id, firstName, lastName, address, description, wallet, profil)
    {
    }
    public override void payBook(int numberOfDays)
    {
        decimal amount;      

        if (numberOfDays <= Profil.FreePeriod )//student.classe == 1 && 
        {
            // Les étudiants en première année ont une période gratuite de 15 jours pour chaque livre emprunté
            amount = 0;
        }
        else
        {
            // Le tarif étudiant est de 10 centimes par jour (0.10 eu)
            amount = numberOfDays * Profil.Rate;
        }
        if (amount < Wallet)
        {
            setWallet(Wallet - amount);
        }
        else
        {
            throw new System.Exception("dont have amount in wallet !!!");
        }
    }

    

}
