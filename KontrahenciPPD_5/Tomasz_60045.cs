using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static KontrahenciPPD_5.Pracownik_BIN;


namespace KontrahenciPPD_5
{
    class Tomasz_60045
    {
        // Rosnące sortowanie pracowników według nazwiska
        public static void SortowaniePracownikowPoNazwisku(string DatabasePathPracownikow)
        {
            List<Pracownik> ListaPracownicy = DeserializePracownicy(DatabasePathPracownikow);

            ListaPracownicy.Sort(
            delegate (Pracownik p1, Pracownik p2)
            {
                int compareDate = p1.Nazwisko.CompareTo(p2.Nazwisko);
                if (compareDate == 0)
                {
                    return p2.Nazwisko.CompareTo(p1.Nazwisko);
                }
                return compareDate;
            }
            );

            Console.Clear();
            Console.WriteLine("Pracownicy:");

            foreach (Pracownik pracownik in ListaPracownicy) // zmienna do wyszukania 
            {
                Console.WriteLine(pracownik.IdPracownika + ". " + pracownik.Imie + " " + pracownik.Nazwisko);
            }

        }

        // Rosnące sortowanie pracowników według imienia
        public static void SortowaniePracownikowPoImieniu(string DatabasePathPracownikow)
        {
            List<Pracownik> ListaPracownicy = DeserializePracownicy(DatabasePathPracownikow);

            ListaPracownicy.Sort(
            delegate (Pracownik p1, Pracownik p2)
            {
                int compareDate = p1.Imie.CompareTo(p2.Imie);
                if (compareDate == 0)
                {
                    return p2.Imie.CompareTo(p1.Imie);
                }
                return compareDate;
            }
            );

            Console.Clear();
            Console.WriteLine("Pracownicy:");

            foreach (Pracownik pracownik in ListaPracownicy) // zmienna do wyszukania 
            {
                Console.WriteLine(pracownik.IdPracownika + ". " + pracownik.Imie + " " + pracownik.Nazwisko);
            }

        }

        // Rosnące sortowanie pracowników według numeru telefonu
        public static void SortowaniePracownikowPoID(string DatabasePathPracownikow)
        {
            List<Pracownik> ListaPracownicy = DeserializePracownicy(DatabasePathPracownikow);

            List<Pracownik> pracownicyPosortowani = ListaPracownicy.OrderBy(x => Int32.Parse(x.IdPracownika)).ToList();

            Console.Clear();
            Console.WriteLine("Pracownicy:");

            foreach (Pracownik pracownik in pracownicyPosortowani) // zmienna do wyszukania 
            {
                Console.WriteLine(pracownik.IdPracownika + ". " + pracownik.Imie + " " + pracownik.Nazwisko);
            }

        }
    }
}
