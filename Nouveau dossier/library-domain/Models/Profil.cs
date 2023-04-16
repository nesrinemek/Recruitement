namespace library_domain.Models
{
    public class Profil : Enumeration
    {
        public static Profil Resident = new Profil(1, "Resident", 60, 0, 0.10m, 0.20m);
        public static Profil Student = new Profil(2, "Student", 30, 0,10m, 0);
        public static Profil Student_1_Class = new Profil(3, "Student_1_Class", 30, 15, 0, 0);
        public Profil(int id, string name, int maxPeriod = 0, int freePeriod = 0, decimal rate = 0, decimal lateRate = 0) : base(id, name)
        {
            MaxPeriod = maxPeriod;
            FreePeriod = freePeriod;
            Rate = rate;
            LateRate = lateRate;
        } 
        public int MaxPeriod { get; private set; }
        public int FreePeriod { get; private set; }
        public decimal Rate { get; set; }
        public decimal LateRate { get; set; }
    }
}