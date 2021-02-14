using System;
using System.Collections.Generic;
using System.Text;
using static KontrahenciPPD_5.Firma_BIN;
using static KontrahenciPPD_5.Pracownik_BIN;
using static KontrahenciPPD_5.Firma_F;
using static KontrahenciPPD_5.Pracownik_F;
using static KontrahenciPPD_5.Szymon_60024;
using System.IO;
using WindowsInput;

namespace KontrahenciPPD_5
{
    class Michal_60039
    {
        // Utworzenie nowego pliku dla firm na podstawie edycji pliku pierwotnego (chyba?)
        public static void PrzepisanieFirm(string DatabasePathFirm, string id_firmy, Firma nowa_firma)
        {
            List<Firma> ListaFirmyStare = DeserializeFirmy(DatabasePathFirm);
            List<Firma> ListaFirmyNowe = new List<Firma>();

            foreach (Firma firmaLoop in ListaFirmyStare)
            {
                if (!firmaLoop.IdFirmy.Contains(id_firmy))
                {
                    ListaFirmyNowe.Add(firmaLoop);
                }
            }

            ListaFirmyNowe.Add(nowa_firma);

            File.Move(DatabasePathFirm, DatabasePathFirm + "." + DateTime.Now.ToString("ddMMyyyyHHmmss"));
            FileStream create = new FileStream(DatabasePathFirm, FileMode.Create, FileAccess.Write, FileShare.None);
            create.Close();

            SerializeFirmy(DatabasePathFirm, ListaFirmyNowe);
        }

        public static void ZmienFirme(string DatabasePathFirm)
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

        public static void PrzepisaniePracownikow(string DatabasePathPracownikow, string id_pracownika, Pracownik nowy_pracownik)
        {
            List<Pracownik> ListaPracownicyStare = DeserializePracownicy(DatabasePathPracownikow);
            List<Pracownik> ListaPracownicyNowe = new List<Pracownik>();

            foreach (Pracownik pracownikLoop in ListaPracownicyStare)
            {
                if (!pracownikLoop.IdPracownika.Contains(id_pracownika))
                {
                    ListaPracownicyNowe.Add(pracownikLoop);
                }
            }

            ListaPracownicyNowe.Add(nowy_pracownik);

            File.Move(DatabasePathPracownikow, DatabasePathPracownikow + "." + DateTime.Now.ToString("ddMMyyyyHHmmss"));
            FileStream create = new FileStream(DatabasePathPracownikow, FileMode.Create, FileAccess.Write, FileShare.None);
            create.Close();

            SerializePracownikow(DatabasePathPracownikow, ListaPracownicyNowe);
        }

        public static void ZmienPracownika(string DatabasePathPracownikow)
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
