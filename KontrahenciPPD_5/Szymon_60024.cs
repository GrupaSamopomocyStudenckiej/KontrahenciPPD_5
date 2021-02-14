using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using static KontrahenciPPD_5.Firma_F;
using static KontrahenciPPD_5.Pracownik_F;




namespace KontrahenciPPD_5
{
    class Szymon_60024
    {
        // Zapis firm do pliku - każda firma zajmuje 1024 bajty
        public static void SerializeFirmy(String DatabasePathFirm, List<Firma> ListaFirm)
        {
            foreach (Firma firma in ListaFirm)
            {
                using (Stream fs = new FileStream(DatabasePathFirm, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    byte[] buff = new byte[1024];
                    Array.Clear(buff, 0, buff.Length);

                    using (MemoryStream ms = new MemoryStream(buff))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(ms, firma);
                        ms.WriteTo(fs);
                    }
                }
            }
        }

        // Zapis pojedynczej firmy do pliku
        public static void SerializeFirma(String DatabasePathFirm, Firma firma)
        {
            using (Stream fs = new FileStream(DatabasePathFirm, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                byte[] buff = new byte[1024];
                Array.Clear(buff, 0, buff.Length);

                using (MemoryStream ms = new MemoryStream(buff))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, firma);
                    ms.WriteTo(fs);
                }
            }
        }

        public static void DodajFirme(String DatabasePathFirm)
        {
            Console.Clear();

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

        // Odczyt wszystkich pracowników do pliku
        public static void SerializePracownikow(String DatabasePathPracownikow, List<Pracownik> ListaPracownikow)
        {
            foreach (Pracownik pracownik in ListaPracownikow)
            {
                using (Stream fs = new FileStream(DatabasePathPracownikow, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    byte[] buff = new byte[1024];
                    Array.Clear(buff, 0, buff.Length);

                    using (MemoryStream ms = new MemoryStream(buff))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(ms, pracownik);
                        ms.WriteTo(fs);
                    }
                }
            }
        }

        // Odczyt pojedynczego pracownika z pliku
        public static void SerializePracownik(String DatabasePathPracownikow, Pracownik pracownik)
        {
            using (Stream fs = new FileStream(DatabasePathPracownikow, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                byte[] buff = new byte[1024];
                Array.Clear(buff, 0, buff.Length);

                using (MemoryStream ms = new MemoryStream(buff))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, pracownik);
                    ms.WriteTo(fs);
                }
            }
        }

        public static void DodajPracownika(string DatabasePathPracownikow)
        {
            Console.Clear();

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


    }
}
