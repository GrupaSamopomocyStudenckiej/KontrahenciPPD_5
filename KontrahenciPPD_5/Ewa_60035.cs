using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static KontrahenciPPD_5.Firma_BIN;
using static KontrahenciPPD_5.Pracownik_BIN;
using static KontrahenciPPD_5.Szymon_60024;

namespace KontrahenciPPD_5
{
    class Ewa_60035
    {
        // Usunięcie firmy z pliku
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

            File.Move(DatabasePathFirm, DatabasePathFirm + "." + DateTime.Now.ToString("ddMMyyyyHHmmss"));
            FileStream create = new FileStream(DatabasePathFirm, FileMode.Create, FileAccess.Write, FileShare.None);
            create.Close();

            SerializeFirmy(DatabasePathFirm, ListaFirmyNowe);
        }

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
    }
}
