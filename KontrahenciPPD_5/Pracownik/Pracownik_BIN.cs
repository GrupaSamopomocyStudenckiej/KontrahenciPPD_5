using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace KontrahenciPPD_5.Pracownik
{
    class Pracownik_BIN
    {
        // Tworzenie "bazowego" pliku z przykładowymi danymi
        public static void FirstRunPracownicy(string DatabasePathPracownikow)
        {
            try
            {
                if (!File.Exists(DatabasePathPracownikow))
                {
                    FileStream create = new FileStream(DatabasePathPracownikow, FileMode.Create, FileAccess.Write, FileShare.None);
                    create.Close();

                    Pracownik pracownik1 = new Pracownik("1", "3", "weqqw", "jhgjhgh", "1234569", "123@sdf.jon");
                    Pracownik pracownik2 = new Pracownik("2", "2", "dsadas", "dsadasd", "1234569", "123@sdf.jon");
                    Pracownik pracownik3 = new Pracownik("3", "1", "weqqw", "cxzcxzzxc", "1234569", "123@sdf.jon");

                    List<Pracownik> ListaPracownikow = new List<Pracownik>();
                    ListaPracownikow.Add(pracownik1);
                    ListaPracownikow.Add(pracownik2);
                    ListaPracownikow.Add(pracownik3);

                    SerializePracownikow(DatabasePathPracownikow, ListaPracownikow);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
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

        // Zapis pracowników do pliku
        public static List<Pracownik> DeserializePracownicy(String DatabasePathPracownikow)
        {
            List<Pracownik> ListaPracownicy = new List<Pracownik>();

            using (Stream fs = new FileStream(DatabasePathPracownikow, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                byte[] bytes = new byte[fs.Length];
                Int32 byte_size = 1024;

                fs.Read(bytes, 0, bytes.Length);

                byte[] temp = new byte[byte_size];

                for (int i = 0; i < fs.Length; i += byte_size)
                {
                    Array.Copy(bytes, i, temp, 0, byte_size);
                    using (MemoryStream ms = new MemoryStream(temp))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        ListaPracownicy.Add((Pracownik)formatter.Deserialize(ms));
                    }
                }
                fs.Close();
            }
            return ListaPracownicy;
        }

        // Zapis pracownika do pliku
        public static Pracownik DeserializePracownik(String DatabasePathPracownikow, string id_pracownika)
        {
            List<Pracownik> ListaPracownikow = new List<Pracownik>();

            using (Stream fs = new FileStream(DatabasePathPracownikow, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                byte[] bytes = new byte[fs.Length];
                Int32 byte_size = 1024;

                fs.Read(bytes, 0, bytes.Length);

                byte[] temp = new byte[byte_size];

                for (int i = 0; i < fs.Length; i += byte_size)
                {
                    Array.Copy(bytes, i, temp, 0, byte_size);
                    using (MemoryStream ms = new MemoryStream(temp))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        ListaPracownikow.Add((Pracownik)formatter.Deserialize(ms));
                    }
                }
                fs.Close();

                Pracownik pracownik = new Pracownik();
                foreach (Pracownik pracownikLoop in ListaPracownikow)
                {
                    if (pracownikLoop.IdPracownika.Contains(id_pracownika))
                    {
                        pracownik = pracownikLoop;
                    }
                }

                return pracownik;
            }
        }

        // Utworzenie nowego pliku dla pracowników na podstawie edycji pliku pierwotnego (chyba?)
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


        //Usunięcie pracownika z pliku
        public static void UsunieciePracownika(string DatabasePathPracownikow, string id_pracownika)
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

            File.Move(DatabasePathPracownikow, DatabasePathPracownikow + "." + DateTime.Now.ToString("ddMMyyyyHHmmss"));
            FileStream create = new FileStream(DatabasePathPracownikow, FileMode.Create, FileAccess.Write, FileShare.None);
            create.Close();

            SerializePracownikow(DatabasePathPracownikow, ListaPracownicyNowe);
        }


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

