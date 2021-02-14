using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using static KontrahenciPPD_5.Szymon_60024;

namespace KontrahenciPPD_5
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
        


        //Usunięcie pracownika z pliku
        
    }
}

