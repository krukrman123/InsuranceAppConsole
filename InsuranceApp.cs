using System;
using System.Collections.Generic;

namespace InsuranceAppConsole
{
    class InsuranceApp
    {
        private List<Insured> seznamPojisteny = new List<Insured>();




        /// <summary>
        /// Sppusteci Funkce
        /// </summary>

        public void Run()
        {
            int volba;

            do
            {
                Console.Clear();

                ZobrazMenu();

                volba = ZvolteAkci();

                switch (volba)
                {
                    case 1:
                        VytvorPojisteneho();
                        break;
                    case 2:
                        ZobrazSeznam();
                        break;
                    case 3:
                        VyhledejPojisteneho();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        ZobrazChybu("Neplatná volba");
                        break;
                }

            } while (volba != 0);
        }



        /// <summary>
        /// //Zobrazovaci funkce
        /// </summary>
        /// 
        private void ZobrazChybu(string zprava)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(zprava);
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(2000);
        }

        private void ZobrazMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================ Evidence Pojistenych ==============");
            Console.WriteLine();
            Console.WriteLine("1 - Vytvoření pojištěného");
            Console.WriteLine("2 - Zobrazení seznamu všech pojištěných");
            Console.WriteLine("3 - Vyhledání pojištěného podle jména a příjmení");
            Console.WriteLine("0 - Konec");
            Console.WriteLine("====================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
        
        private void ZobrazSeznam()
        {
            Console.Clear();
            Console.WriteLine("========================= SEZNAM POJIŠTĚNÝCH =========================");
            Console.WriteLine("{0,-20} | {1,-20} | {2,-5} | {3}", "Jméno", "Příjmení", "Věk", "Telefonní číslo");
            Console.WriteLine("----------------------------------------------------------------------");

            foreach (var pojisteny in seznamPojisteny)
            {
                Console.WriteLine("{0,-20} | {1,-20} | {2,-5} | {3}", pojisteny.Jmeno, pojisteny.Prijmeni, pojisteny.Vek, pojisteny.TelefonniCislo);
            }

            CekatNaStiskKlavesy();
        }



        /// <summary>
        /// Volici Akce 
        /// </summary>
        /// <returns></returns>


        private int ZvolteAkci()
        {
            int volba;
            int radekChyby = -1;

            do
            {
                Console.Clear(); // Vymaže obsah konzole

                ZobrazMenu();

                if (radekChyby == -1)
                {
                    radekChyby = Console.CursorTop; // Získáme aktuální pozici kurzoru
                }

                Console.SetCursorPosition(0, radekChyby); // Nastavíme kurzor na řádek chyby
                Console.Write(new string(' ', Console.WindowWidth - 1)); // Smazání řádku chyby
                Console.SetCursorPosition(0, radekChyby); 

                Console.ForegroundColor = ConsoleColor.Green; // Změní barvu textu na zelenou pro nápovědu
                Console.Write("Zvolte akci: ");
                Console.ForegroundColor = ConsoleColor.White; // Vrátí barvu textu zpět na bílou

                while (!int.TryParse(Console.ReadLine(), out volba) || volba < 0 || volba > 3)
                {
                    Console.SetCursorPosition(0, radekChyby); 
                    Console.Write(new string(' ', Console.WindowWidth - 1)); 
                    Console.SetCursorPosition(0, radekChyby); 

                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("Neplatný vstup. Zadejte platné číslo volby.");
                    Console.ForegroundColor = ConsoleColor.White; 

                    System.Threading.Thread.Sleep(1000); // Počkej 2 sekundy

                    Console.SetCursorPosition(0, radekChyby); // Nastavíme kurzor na řádek chyby
                    Console.Write(new string(' ', Console.WindowWidth - 1)); // Smazání řádku chyby
                    Console.SetCursorPosition(0, radekChyby); 

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Zvolte akci: ");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (volba < 0 || volba > 3); 

            return volba;
        }

        private string ZadejSlovo()
        {
            string slovo;
            while (true)
            {
                slovo = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(slovo) && slovo.All(char.IsLetter))
                {
                    break;
                }
                else
                {
                    ZobrazChybu("Neplatný vstup. Zadejte platné slovo.");
                }
            }
            return slovo;
        }

        private void CekatNaStiskKlavesy()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Stiskněte libovolnou klávesu pro pokračování...");
            Console.WriteLine("-----------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey(true);
        }






        /// <summary>
        /// Pojistenci Funkce
        /// </summary>



        private void VytvorPojisteneho()
        {
            Console.Write("Zadejte jméno: ");
            string jmeno = ZadejSlovo();

            Console.Write("Zadejte příjmení: ");
            string prijmeni = ZadejSlovo();

            int vek;
            while (true)
            {
                Console.Write("Zadejte věk: ");
                if (int.TryParse(Console.ReadLine(), out vek) && vek > 0 && vek <= 115)
                {
                    break;
                }
                else
                {
                    ZobrazChybu("Neplatný věk. Zadejte platné číslo (1-115).");
                }
            }

            string telefonniCislo;
            while (true)
            {
                Console.Write("Zadejte telefonní číslo (9 číslic): ");
                telefonniCislo = Console.ReadLine();
                if (telefonniCislo.Length == 9 && telefonniCislo.All(char.IsDigit))
                {
                    break;
                }
                else
                {
                    ZobrazChybu("Neplatné telefonní číslo. Zadejte platné číslo o délce 9 číslic.");
                }
            }

            seznamPojisteny.Add(new Insured(jmeno, prijmeni, vek, telefonniCislo));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=================================");
            Console.WriteLine("Pojištěný byl úspěšně vytvořen.");
            Console.WriteLine("=================================");
            Console.ForegroundColor = ConsoleColor.White;

            System.Threading.Thread.Sleep(2000);

            Console.Clear();
        }


        private void VyhledejPojisteneho()
        {
            Console.Write("Zadejte jméno pojištěného: ");
            string hledaneJmeno = Console.ReadLine();

            Console.Write("Zadejte příjmení pojištěného: ");
            string hledanePrijmeni = Console.ReadLine();

            List<Insured> nalezeniPojisteni = seznamPojisteny.FindAll(p =>
                p.Jmeno.Equals(hledaneJmeno, StringComparison.OrdinalIgnoreCase) &&
                p.Prijmeni.Equals(hledanePrijmeni, StringComparison.OrdinalIgnoreCase));

            if (nalezeniPojisteni.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("============================ VYHLEDÁNÍ POJIŠTĚNÉHO ============================");
                Console.WriteLine("{0,-4} | {1,-20} | {2,-20} | {3,-5} | {4}", "ID", "Jméno", "Příjmení", "Věk", "Telefonní číslo");
                Console.WriteLine("-------------------------------------------------------------------------------");

                for (int i = 0; i < nalezeniPojisteni.Count; i++)
                {
                    Console.WriteLine("{0,-4} | {1,-20} | {2,-20} | {3,-5} | {4}", i + 1, nalezeniPojisteni[i].Jmeno, nalezeniPojisteni[i].Prijmeni, nalezeniPojisteni[i].Vek, nalezeniPojisteni[i].TelefonniCislo);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Pojištěný nebyl nalezen.");
            }

            CekatNaStiskKlavesy();
        }







    }
}
