using System;
using System.Collections.Generic;
using System.Text;
using WindowsInput;
using static KontrahenciPPD_5.Firma_BIN;
using static KontrahenciPPD_5.Program;
using static KontrahenciPPD_5.Michal_60039;
using static KontrahenciPPD_5.Ewa_60035;
using static KontrahenciPPD_5.Szymon_60024;

namespace KontrahenciPPD_5
{
    class Firma_F
    {
        public static void ShowFirmy(String DatabasePathFirm)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Firmy:");
                List<Firma> firmy = new List<Firma>();
                firmy = DeserializeFirmy(DatabasePathFirm);

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

        

        public static bool PokazFirme(string DatabasePathFirm, string id_firmy)
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
    }
}
