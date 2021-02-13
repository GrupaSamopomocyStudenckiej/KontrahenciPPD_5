using System;
using System.Collections.Generic;
using System.Text;
using WindowsInput;
using static KontrahenciPPD_5.Firma.Firma_BIN;
using static KontrahenciPPD_5.Program;

namespace KontrahenciPPD_5.Firma
{
    class Firma_F
    {
        public static void ShowFirmy(String DatabasePath)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Firmy:");
                List<Firma> firmy = new List<Firma>();
                firmy = DeserializeFirmy(DatabasePath);

                foreach (Firma firma in firmy) // zmienna do wyszukania 
                {
                    Console.WriteLine(firma.IdFirmy + ". " + firma.NazwaFirmy + " " + firma.Nip + " " + firma.Regon);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public static bool ShowMenuFirmy(String DatabasePathFirm)
        {
            Console.WriteLine("\r\nWybierz opcje:");
            Console.WriteLine("1) Dodaj nową firmę do bazy");
            Console.WriteLine("2) Wyświetl dane firmy");
            Console.WriteLine("3) Zmień dane firmy");
            Console.WriteLine("4) Usuń firmę");
            Console.WriteLine("9) Wróć do menu głównego");
            Console.WriteLine("0) Wyjście");
            Console.Write("\r\nWybrano opcje: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DodajFirme(DatabasePathFirm);
                    return true;
                case "2":
                    Console.WriteLine("Podaj ID firmy do odczytania: ");
                    PokazFirme(DatabasePathFirm,Console.ReadLine());
                    return true;
                case "3":
                    ZmienFirme(DatabasePathFirm);
                    return true;
                case "4":
                    Console.WriteLine("Podaj ID firmy do usunięcia: ");
                    UsuniecieFirmy(DatabasePathFirm, Console.ReadLine());
                    ShowFirmy(DatabasePathFirm);
                    ShowMenuFirmy(DatabasePathFirm);
                    return true;
                case "9":
                    ShowMenuGlowne();
                    return true;
                case "0":
                    System.Environment.Exit(1);
                    return true;
                default:
                    Console.WriteLine("Nie wybrano nic. Sprubuj ponownie.");
                    ShowMenuFirmy(DatabasePathFirm);
                    return true;
            }
        }

        static void DodajFirme(String DatabasePathFirm)
        {
            Firma firmyNowy = new Firma();

            Console.WriteLine("Podaj ID firmy: ");
            firmyNowy.IdFirmy = Console.ReadLine();

            Console.WriteLine("Podaj ID siedziby firmy: ");
            firmyNowy.IdSiedzibyFirmy = Console.ReadLine();

            Console.WriteLine("Podaj nazwę firmy: ");
            firmyNowy.NazwaFirmy = Console.ReadLine();

            Console.WriteLine("Podaj NIP firmy: ");
            firmyNowy.Nip = Console.ReadLine();

            Console.WriteLine("Podaj REGON firmy: ");
            firmyNowy.Regon = Console.ReadLine();

            Console.WriteLine("Podaj miasto firmy: ");
            firmyNowy.Miasto = Console.ReadLine();

            Console.WriteLine("Podaj ulicę firmy: ");
            firmyNowy.Ulica = Console.ReadLine();

            Console.WriteLine("Podaj numer budynku firmy: ");
            firmyNowy.NrBudynku = Console.ReadLine();

            Console.WriteLine("Podaj numer lokalu firmy: ");
            firmyNowy.NrLokalu = Console.ReadLine();

            Console.WriteLine("Podaj kod pocztowy firmy: ");
            firmyNowy.KodPocztowy = Console.ReadLine();

            Console.WriteLine("Podaj pocztę firmy: ");
            firmyNowy.Poczta = Console.ReadLine();

            Console.WriteLine("Podaj numer telefonu do firmy: ");
            firmyNowy.NrTelefonu = Console.ReadLine();

            Console.WriteLine("Podaj kraj firmy: ");
            firmyNowy.Kraj = Console.ReadLine();

            Console.WriteLine("Podaj adres e-mail firmy: ");
            firmyNowy.Email = Console.ReadLine();

            Console.WriteLine("Podaj stronę WWW firmy: ");
            firmyNowy.StronaWWW = Console.ReadLine();

            Console.WriteLine("Podaj numer konta firmy: ");
            firmyNowy.NrKonta = Console.ReadLine();

            SerializeFirma(DatabasePathFirm, firmyNowy);

            ShowFirmy(DatabasePathFirm);
            ShowMenuFirmy(DatabasePathFirm);
        }

        static bool PokazFirme(string DatabasePathFirm, string id_firmy)
        {
            try
            {
                Firma firmaRead = DeserializeFirma(DatabasePathFirm, id_firmy);

                Console.Clear();
                Console.WriteLine("ID firmy: " + firmaRead.IdFirmy);
                Console.WriteLine("ID siedziby firmy: " + firmaRead.IdSiedzibyFirmy);
                Console.WriteLine("Nazwa firmy: " + firmaRead.NazwaFirmy);
                Console.WriteLine("NIP: " + firmaRead.Nip);
                Console.WriteLine("REGON: " + firmaRead.Regon);
                Console.WriteLine(firmaRead.Miasto + " " + "Ul. " + firmaRead.Ulica + " " + firmaRead.NrBudynku + "/" + firmaRead.NrLokalu + " " + firmaRead.KodPocztowy + " " + firmaRead.Poczta);
                Console.WriteLine("Numer telefonu: " + firmaRead.NrTelefonu);
                Console.WriteLine("Kraj: " + firmaRead.Kraj);
                Console.WriteLine("Email: " + firmaRead.Email);
                Console.WriteLine("Strona WWW: " + firmaRead.StronaWWW);
                Console.WriteLine("Numer konta: " + firmaRead.NrKonta.Replace("\n", "").Replace("\r", ""));

                Console.WriteLine("\r\n1) Wróc do listy firm");
                Console.Write("Wybrano opcje: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        ShowFirmy(DatabasePathFirm);
                        ShowMenuFirmy(DatabasePathFirm);
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

        static void ZmienFirme(string DatabasePathFirm)
        {
            try
            {
                Firma firmaNowy = new Firma();
                InputSimulator sim = new InputSimulator();

                Console.WriteLine("Podaj ID firmy do zmiany: ");

                Firma firmaRead = DeserializeFirma(DatabasePathFirm, Console.ReadLine());

                Console.Clear();

                Console.Write("ID firmy: ");
                sim.Keyboard.TextEntry(firmaRead.IdFirmy);
                firmaNowy.IdFirmy = Console.ReadLine();

                Console.Write("ID siedziby firmy: ");
                sim.Keyboard.TextEntry(firmaRead.IdSiedzibyFirmy);
                firmaNowy.IdSiedzibyFirmy = Console.ReadLine();

                Console.Write("Nazwa firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NazwaFirmy);
                firmaNowy.NazwaFirmy = Console.ReadLine();

                Console.Write("NIP firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Nip);
                firmaNowy.Nip = Console.ReadLine();

                Console.Write("REGON firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Regon);
                firmaNowy.Regon = Console.ReadLine();

                Console.Write("Miasto firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Miasto);
                firmaNowy.Miasto = Console.ReadLine();

                Console.Write("Ulica firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Ulica);
                firmaNowy.Ulica = Console.ReadLine();

                Console.Write("Numer budynku firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NrBudynku);
                firmaNowy.NrBudynku = Console.ReadLine();

                Console.Write("Numer lokalu firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NrLokalu);
                firmaNowy.NrLokalu = Console.ReadLine();

                Console.Write("Kod pocztowy firmy: ");
                sim.Keyboard.TextEntry(firmaRead.KodPocztowy);
                firmaNowy.KodPocztowy = Console.ReadLine();

                Console.Write("Poczta firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Poczta);
                firmaNowy.Poczta = Console.ReadLine();

                Console.Write("Numer telefonu firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NrTelefonu);
                firmaNowy.NrTelefonu = Console.ReadLine();

                Console.Write("Kraj firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Kraj);
                firmaNowy.Kraj = Console.ReadLine();

                Console.Write("E-mail firmy: ");
                sim.Keyboard.TextEntry(firmaRead.Email);
                firmaNowy.Email = Console.ReadLine();

                Console.Write("Strona WWW firmy: ");
                sim.Keyboard.TextEntry(firmaRead.StronaWWW);
                firmaNowy.StronaWWW = Console.ReadLine();

                Console.Write("Numer konta firmy: ");
                sim.Keyboard.TextEntry(firmaRead.NrKonta);
                firmaNowy.NrKonta = Console.ReadLine();

                if (firmaNowy == firmaRead)
                {
                    ShowFirmy(DatabasePathFirm);
                    ShowMenuFirmy(DatabasePathFirm);
                }
                else
                {

                    PrzepisanieFirm(DatabasePathFirm, firmaRead.IdFirmy, firmaNowy);
                    ShowFirmy(DatabasePathFirm);
                    ShowMenuFirmy(DatabasePathFirm);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        static void WyszukajSiedzibeFirmy(string DatabasePathFirm, string id_siedziby)
        {
            bool czyZnalazl = false;
            if (id_siedziby == "0")
            {
                Console.WriteLine("To jest siedziba firmy");
                Console.ReadKey(true);
                ShowMenuFirmy(DatabasePathFirm);
            }
            else
            {
                _ = new List<Firma>();
                List<Firma> firmy = DeserializeFirmy(DatabasePathFirm);
                foreach (Firma firma in firmy)
                {
                    if (firma.IdSiedzibyFirmy == id_siedziby)
                    {
                        Console.WriteLine(firma.NazwaFirmy + ". " + firma.Nip + " " + firma.Regon);
                        czyZnalazl = true;
                    }
                }
            }
            if (czyZnalazl == false)
            {
                Console.WriteLine("Błąd wyszukania, brak odpowiadających rekordów w bazie");
            }
            Console.ReadKey(true);
            ShowMenuFirmy(DatabasePathFirm);
        }
    }
}
