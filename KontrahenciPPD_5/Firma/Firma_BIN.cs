using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace KontrahenciPPD_5.Firma
{
    class Firma_BIN
    {
        public static void FirstRunFirmy(string DatabasePathFirm)
        {
            try
            {
                if (!File.Exists(DatabasePathFirm))
                {
                    FileStream create = new FileStream(DatabasePathFirm, FileMode.Create, FileAccess.Write, FileShare.None);
                    create.Close();

                    Firma firma1 = new Firma("1", "", "nazwa", "912213312", "123123123", "Resovia", "Szportowa", "2", "10", "22-222", "Rzeszów", "123123123", "Polska", "test@test.com", "www.firma.pl", "212345678654321345678765");
                    Firma firma2 = new Firma("2", "1", "nazwa1", "912214312", "12312343123", "Rzeszów", "Chopina", "2", "10", "12-222", "Kielnarowa", "987987987", "Polska", "test2@test.com", "www.firma2.pl", "212345678632321345678765");
                    Firma firma3 = new Firma("3", "3", "nazw35", "914312", "12312343", "Rzeszów", "Chopina", "2", "10", "12-222", "Kielnarowa", "987987987", "Polska", "test2@test.com", "www.firma2.pl", "212345678632321345678765");

                    List<Firma> ListaFirm = new List<Firma>();
                    ListaFirm.Add(firma1);
                    ListaFirm.Add(firma2);
                    ListaFirm.Add(firma3);

                    SerializeFirmy(DatabasePathFirm, ListaFirm);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

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

        public static List<Firma> DeserializeFirmy(String DatabasePathFirm)
        {
            List<Firma> ListaFirmy = new List<Firma>();

            using (Stream fs = new FileStream(DatabasePathFirm, FileMode.Open, FileAccess.Read, FileShare.None))
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
                        ListaFirmy.Add((Firma)formatter.Deserialize(ms));
                    }
                }
                fs.Close();
            }
            return ListaFirmy;
        }

        public static Firma DeserializeFirma(String DatabasePathFirm, string id_firmy)
        {
            List<Firma> ListaFirmy = new List<Firma>();

            using (Stream fs = new FileStream(DatabasePathFirm, FileMode.Open, FileAccess.Read, FileShare.None))
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
                        ListaFirmy.Add((Firma)formatter.Deserialize(ms));
                    }
                }
                fs.Close();
            }

            Firma firma = new Firma();
            foreach (Firma firmaLoop in ListaFirmy)
            {
                if (firmaLoop.IdFirmy.Contains(id_firmy))
                {
                    firma = firmaLoop;
                }
            }

            return firma;
        }

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

        public static void UsuniecieFirmy(string DatabasePathFirm, string id_firmy)
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

            string DTStamp = DateTime.Now.ToString();


            File.Move(DatabasePathFirm, DatabasePathFirm + "." + DateTime.Now.ToString("ddMMyyyyHHmmss"));
            FileStream create = new FileStream(DatabasePathFirm, FileMode.Create, FileAccess.Write, FileShare.None);
            create.Close();

            SerializeFirmy(DatabasePathFirm, ListaFirmyNowe);
        }




    }
}
