using System;
using System.Collections.Generic;

namespace Projekt
{
    class Osoba
    {
        public string Imie;
        public string Nazwisko;
        public DateTime? DataUrodzenia;
        public DateTime? DataSmierci;

        public Osoba(string imie, string nazwisko = "")
        {
            Imie = imie;
            Nazwisko = nazwisko;
        }

        public string PelneImie() => (Imie + " " + Nazwisko).Trim();

        public int? WiekWLatach()
        {
            if (!DataUrodzenia.HasValue) return null;

            DateTime koniec = DataSmierci ?? DateTime.Now;
            double dni = (koniec - DataUrodzenia.Value).TotalDays;
            return (int)(dni / 365.25);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 1) Przygotuj dane
            var osoby = new List<Osoba>
            {
                new Osoba("Jan",  "Kowalski") { DataUrodzenia = new DateTime(1990, 4, 20) },
                new Osoba("Maria")            { DataUrodzenia = new DateTime(1950, 2, 10),
                                                 DataSmierci  = new DateTime(2000, 1, 1) },
                new Osoba("Adam", "Nowak")    // brak daty urodzenia
            };

            // Jeśli podano nazwisko w argumentach to wtedy szukamy tylko tego nazwiska.
            //  Jeśli nie podano, wypisujemy wszystkich.
            if (args.Length > 0)
            {
                string szukane = args[0];
                bool znaleziono = false;

                foreach (var o in osoby)
                {
                    if (o.Nazwisko.Equals(szukane, StringComparison.OrdinalIgnoreCase))
                    {
                        WypiszOsobe(o);
                        znaleziono = true;
                    }
                }

                if (!znaleziono)
                    Console.WriteLine("Brak osoby o nazwisku " + szukane);
            }
            else
            {
                foreach (var o in osoby)
                    WypiszOsobe(o);
            }
        }

        // Pomocnicza metoda do wypisywania osoby
        static void WypiszOsobe(Osoba o)
        {
            Console.WriteLine(o.PelneImie());

            var wiek = o.WiekWLatach();
            Console.WriteLine("Wiek: " + (wiek.HasValue ? wiek + " lat" : "brak danych"));
            Console.WriteLine();
        }
    }
}
