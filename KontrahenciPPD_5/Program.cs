﻿using System;
using System.IO;
using static KontrahenciPPD_5.Firma_BIN;
using static KontrahenciPPD_5.Firma_F;
using static KontrahenciPPD_5.Pracownik_BIN;
using static KontrahenciPPD_5.Pracownik_F;


namespace KontrahenciPPD_5
{
    class Program
    {
        static readonly string DatabasePathFirm = "c:\\kontrahenci\\BazaFirmBinarka.db";
        static readonly string DatabasePathPracownikow = "c:\\kontrahenci\\BazaPracownikowBinarka.db";

        public static bool ShowMenuGlowne()
        {
            Console.Clear();
            Console.WriteLine("Wybierz opcje:");
            Console.WriteLine("1) Wyswietl firmy");
            Console.WriteLine("2) Wyswietl pracownikow");
            Console.WriteLine("0) Wyjście");
            Console.Write("\r\nWybrano opcje: ");
            switch (Console.ReadLine())
            {
                case "1":
                    ShowFirmy(DatabasePathFirm);
                    ShowMenuFirmy(DatabasePathFirm);
                    return true;
                case "2":
                    ShowPracownicy(DatabasePathPracownikow);
                    ShowMenuPracownicy(DatabasePathPracownikow);
                    return true;
                case "0":
                    Environment.Exit(0);
                    return true;

                default:
                    Console.WriteLine("Nie wybrano nic. Sprobuj ponownie.");
                    ShowMenuGlowne();
                    return true;
            }
        }

        public static void Main()
        {
            try
            {
                if (!Directory.Exists("c:\\kontrahenci"))
                {
                    DirectoryInfo di = Directory.CreateDirectory("c:\\kontrahenci");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }


            FirstRunFirmy(DatabasePathFirm);
            FirstRunPracownicy(DatabasePathPracownikow);
            ShowMenuGlowne();
        }



    }
}
