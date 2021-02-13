using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsInput;
using static KontrahenciPPD_5.Pracownik.Pracownik_BIN;
using static KontrahenciPPD_5.Program;

namespace KontrahenciPPD_5.Pracownik
{
    class Pracownik_F 
    {
        public static void ShowPracownicy(string DatabasePathPracownikow)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Pracownicy:");
                List<Pracownik> pracownicy = new List<Pracownik>();
                pracownicy = DeserializePracownicy(DatabasePathPracownikow);

                List<Pracownik> pracownicyPosortowani = pracownicy.OrderBy(x => Int32.Parse(x.IdPracownika)).ToList();

                foreach (Pracownik pracownik in pracownicyPosortowani) // zmienna do wyszukania 
                {
                    Console.WriteLine(pracownik.IdPracownika + ". " + pracownik.Imie + " " + pracownik.Nazwisko);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public static bool ShowMenuPracownicy(string DatabasePathPracownikow)
        {
            Console.WriteLine("\r\nWybierz opcje:");
            Console.WriteLine("1) Dodaj nowego pracownika do bazy");
            Console.WriteLine("2) Wyświetl dane pracownika");
            Console.WriteLine("3) Zmień dane pracownika");
            Console.WriteLine("4) Usuń pracownika");
            Console.WriteLine("5) Wyświetl sortowane po...");
            Console.WriteLine("9) Wróć do menu głównego");
            Console.WriteLine("0) Wyjście");
            Console.Write("\r\nWybrano opcje: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DodajPracownika(DatabasePathPracownikow);
                    return true;
                case "2":
                    Console.WriteLine("Podaj ID pracownika do odczytania: ");
                    PokazPracownika(DatabasePathPracownikow, Console.ReadLine());
                    return true;
                case "3":
                    ZmienPracownika(DatabasePathPracownikow);
                    return true;
                case "4":
                    Console.WriteLine("Podaj ID pracownika do usunięcia: ");
                    UsunieciePracownika(DatabasePathPracownikow, Console.ReadLine());
                    ShowPracownicy(DatabasePathPracownikow);
                    ShowMenuPracownicy(DatabasePathPracownikow);
                    return true;
                case "5":
                    Console.WriteLine("1) ... nazwisku");
                    Console.WriteLine("2) ... imieniu");
                    Console.WriteLine("3) ... ID pracownika");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            SortowaniePracownikowPoNazwisku(DatabasePathPracownikow);
                            ShowMenuPracownicy(DatabasePathPracownikow);
                            return true;
                        case "2":
                            SortowaniePracownikowPoImieniu(DatabasePathPracownikow);
                            ShowMenuPracownicy(DatabasePathPracownikow);
                            return true;
                        case "3":
                            SortowaniePracownikowPoID(DatabasePathPracownikow);
                            ShowMenuPracownicy(DatabasePathPracownikow);
                            return true;
                    }
                    return true;
                case "9":
                    ShowMenuGlowne();
                    return true;
                case "0":
                    System.Environment.Exit(1);
                    return true;
                default:
                    Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                    ShowMenuPracownicy(DatabasePathPracownikow);
                    return true;
            }
        }

        static void DodajPracownika(string DatabasePathPracownikow)
        {
            Pracownik pracownikNowy = new Pracownik();

            Console.WriteLine("Podaj ID pracownika: ");
            pracownikNowy.IdPracownika = Console.ReadLine();

            Console.WriteLine("Podaj ID firmy: ");
            pracownikNowy.IdFirmy = Console.ReadLine();

            Console.WriteLine("Podaj imię pracownika: ");
            pracownikNowy.Imie = Console.ReadLine();

            Console.WriteLine("Podaj nazwisko pracownika: ");
            pracownikNowy.Nazwisko = Console.ReadLine();

            Console.WriteLine("Podaj numer telefonu pracownika: ");
            pracownikNowy.NrTelefonu = Console.ReadLine();

            Console.WriteLine("Podaj adres e-mail pracownika: ");
            pracownikNowy.Email = Console.ReadLine();

            SerializePracownik(DatabasePathPracownikow, pracownikNowy);

            ShowPracownicy(DatabasePathPracownikow);
            ShowMenuPracownicy(DatabasePathPracownikow);
        }

        static bool PokazPracownika(string DatabasePathPracownikow, string id_pracownika)
        {
            try
            {
                Pracownik pracownikRead = DeserializePracownik(DatabasePathPracownikow, id_pracownika);

                Console.Clear();
                Console.WriteLine("ID pracownika: " + pracownikRead.IdPracownika);
                Console.WriteLine("ID firmy: " + pracownikRead.IdFirmy);
                Console.WriteLine("Imię pracownika: " + pracownikRead.Imie);
                Console.WriteLine("Nazwisko pracownika: " + pracownikRead.Nazwisko);
                Console.WriteLine("Numer telefonu pracownika: " + pracownikRead.NrTelefonu);
                Console.WriteLine("Adres e-mail pracownika: " + pracownikRead.Email.Replace("\n", "").Replace("\r", ""));

                Console.WriteLine("\n1) Wróć do listy pracowników");
                Console.Write("Wybrano opcje: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        ShowPracownicy(DatabasePathPracownikow);
                        ShowMenuPracownicy(DatabasePathPracownikow);
                        return true;
                    default:
                        Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return false;
            }
        }

        static void ZmienPracownika(string DatabasePathPracownikow)
        {
            try
            {
                Pracownik pracownikNowy = new Pracownik();
                InputSimulator sim = new InputSimulator();

                Console.WriteLine("Podaj ID pracownika do zmiany: ");
                Pracownik pracownikRead = DeserializePracownik(DatabasePathPracownikow, Console.ReadLine());
                Console.Clear();

                Console.Write("ID pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.IdPracownika);
                pracownikNowy.IdPracownika = Console.ReadLine();

                Console.Write("ID firmy: ");
                if (pracownikRead.IdFirmy.Length != 0)
                {
                    sim.Keyboard.TextEntry(pracownikRead.IdFirmy);
                }
                else
                {
                    sim.Keyboard.TextEntry("0");
                }
                pracownikNowy.IdFirmy = Console.ReadLine();

                Console.Write("Imię pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.Imie);
                pracownikNowy.Imie = Console.ReadLine();

                Console.Write("Nazwisko pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.Nazwisko);
                pracownikNowy.Nazwisko = Console.ReadLine();

                Console.Write("Numer telefonu pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.NrTelefonu);
                pracownikNowy.NrTelefonu = Console.ReadLine();

                Console.Write("Adres e-mail pracownika: ");
                sim.Keyboard.TextEntry(pracownikRead.Email);
                pracownikNowy.Email = Console.ReadLine();

                if (pracownikNowy == pracownikRead)
                {
                    ShowPracownicy(DatabasePathPracownikow);
                    ShowMenuPracownicy(DatabasePathPracownikow);
                }
                else
                {
                    PrzepisaniePracownikow(DatabasePathPracownikow, pracownikRead.IdPracownika, pracownikNowy);
                    ShowPracownicy(DatabasePathPracownikow);
                    ShowMenuPracownicy(DatabasePathPracownikow);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
