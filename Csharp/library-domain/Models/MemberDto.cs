using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library_domain.Models
{
    public class MemberDto 
    {
        public MemberDto(int id, string firstName, string lastName, string address, string description, decimal wallet, Profil profil) 
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Description = description;
            Wallet = wallet;
            Profil = profil;
        }

        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Address { get; protected set; }
        public string Description { get; protected set; }
        /// <summary>
        /// An initial sum of money the member has
        /// </summary>
        public decimal Wallet { get; private protected set; }
        public Profil Profil { get; protected set; }

    }
}
