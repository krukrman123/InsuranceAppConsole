using System;

namespace InsuranceAppConsole
{
    class Insured
    {
        public string Jmeno { get; }
        public string Prijmeni { get; }
        public int Vek { get; }
        public string TelefonniCislo { get; }

        public Insured(string jmeno, string prijmeni, int vek, string telefonniCislo)
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Vek = vek;
            TelefonniCislo = telefonniCislo;
        }

        public override string ToString()
        {
            return $"{Jmeno,-20} | {Prijmeni,-20} | {Vek,-5} | {TelefonniCislo}";
        }
    }
}
