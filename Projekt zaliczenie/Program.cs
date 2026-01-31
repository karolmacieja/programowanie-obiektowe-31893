using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using System.Text.Json;

namespace MenedzerFinansowy
{
    
    public interface IInformacja
    {
        void WyswietlSzczegoly();
    }

   
    public abstract class Transakcja : IInformacja
    {
        private decimal _kwota; 
        public decimal Kwota
        {
            get => _kwota;
            set => _kwota = value >= 0 ? value : throw new ArgumentException("Kwota nie może być ujemna!");
        }

        
        public string Opis { get; set; } = string.Empty;
        public string Kategoria { get; set; } = string.Empty;
        public DateTime Data { get; set; }

        public Transakcja() { } 

        protected Transakcja(decimal kwota, string opis, string kategoria)
        {
            Kwota = kwota;
            Opis = opis;
            Kategoria = kategoria;
            Data = DateTime.Now;
        }

        public virtual void WyswietlSzczegoly() 
        {
            Console.WriteLine($"{Data.ToShortDateString()} | {Opis} | {Kategoria} | {Kwota} PLN");
        }
    }

    
    public class Wydatek : Transakcja
    {
        public Wydatek() { }
        public Wydatek(decimal kwota, string opis, string kategoria) : base(kwota, opis, kategoria) { }

        public override void WyswietlSzczegoly()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[WYDATEK]  ");
            base.WyswietlSzczegoly();
            Console.ResetColor();
        }
    }

    public class Przychod : Transakcja
    {
        public Przychod() { }
        public Przychod(decimal kwota, string opis, string kategoria) : base(kwota, opis, kategoria) { }

        public override void WyswietlSzczegoly()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[PRZYCHÓD] ");
            base.WyswietlSzczegoly();
            Console.ResetColor();
        }
    }

    public class TransakcjaDTO
    {
        public string Typ { get; set; } = string.Empty;
        public decimal Kwota { get; set; }
        public string Opis { get; set; } = string.Empty;
        public string Kategoria { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }

    
    public class FinanseManager
    {
        private List<Transakcja> transakcje = new List<Transakcja>(); 
        private const string PlikBazy = "finanse.json";

        public void Dodaj(Transakcja t)
        {
            transakcje.Add(t);
            Zapisz();
        }

        public void Usun(int nr)
        {
            int indeks = nr - 1;
            if (indeks >= 0 && indeks < transakcje.Count)
            {
                transakcje.RemoveAt(indeks);
                Zapisz();
                Console.WriteLine("Usunięto pomyślnie.");
            }
            else Console.WriteLine("Błąd: Nieprawidłowy numer!");
        }

        public void PokazWszystkie()
        {
            Console.WriteLine("\n--- LISTA TRANSAKCJI ---");
            if (!transakcje.Any()) Console.WriteLine("Lista jest pusta.");
            for (int i = 0; i < transakcje.Count; i++) 
            {
                Console.Write($"{i + 1}. ");
                transakcje[i].WyswietlSzczegoly();
            }
        }

        public void RaportStatystyczny()
        {
            decimal w = transakcje.OfType<Wydatek>().Sum(x => x.Kwota); 
            decimal p = transakcje.OfType<Przychod>().Sum(x => x.Kwota);
            Console.WriteLine($"\nSaldo: {p - w} PLN | Przychody: {p} | Wydatki: {w}");
        }

        public void RaportKategorii()
        {
            Console.WriteLine("\n--- WYDATKI WEDŁUG KATEGORII (LINQ) ---");
            var grupy = transakcje.OfType<Wydatek>()
                .GroupBy(t => t.Kategoria)
                .Select(g => new { Kat = g.Key, Suma = g.Sum(x => x.Kwota) });

            foreach (var g in grupy) Console.WriteLine($"{g.Kat}: {g.Suma} PLN");
        }

        public void Szukaj(string fraza)
        {
            var wyniki = transakcje.Where(t => t.Opis.ToLower().Contains(fraza.ToLower())).ToList();
            Console.WriteLine($"\nWyniki wyszukiwania ({wyniki.Count}):");
            foreach (var w in wyniki) w.WyswietlSzczegoly();
        }

        public void Reset()
        {
            Console.Write("Czy na pewno usunąć wszystko? (t/n): ");
            string input = Console.ReadLine() ?? "";
            if (input.ToLower() == "t") { transakcje.Clear(); Zapisz(); }
        }

        public void Zapisz() 
        {
            var dane = transakcje.Select(t => new TransakcjaDTO {
                Typ = t.GetType().Name, Kwota = t.Kwota, Opis = t.Opis, Kategoria = t.Kategoria, Data = t.Data
            });
            File.WriteAllText(PlikBazy, JsonSerializer.Serialize(dane));
        }

        public void Wczytaj()
        {
            if (!File.Exists(PlikBazy)) return;
            string json = File.ReadAllText(PlikBazy);
            var dane = JsonSerializer.Deserialize<List<TransakcjaDTO>>(json);
            if (dane == null) return;

            transakcje.Clear();
            foreach (var d in dane)
            {
                Transakcja t = d.Typ == "Przychod" ? new Przychod(d.Kwota, d.Opis, d.Kategoria) : new Wydatek(d.Kwota, d.Opis, d.Kategoria);
                t.Data = d.Data;
                transakcje.Add(t);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            FinanseManager fm = new FinanseManager();
            fm.Wczytaj();
            bool programDziała = true;

            while (programDziała) 
            {
                Console.WriteLine("\n=== MENEDŻER FINANSÓW ===");
                Console.WriteLine("1. Przychód | 2. Wydatek | 3. Lista | 4. Saldo | 5. Usuń | 6. Kat. | 7. Szukaj | 8. Reset | 0. Koniec");
                Console.Write("Wybór: ");
                string opcja = Console.ReadLine() ?? ""; 

                switch (opcja) 
                {
                    case "1": case "2":
                        Console.Write("Kwota: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal k))
                        {
                            Console.Write("Opis: "); string o = Console.ReadLine() ?? "";
                            Console.Write("Kategoria: "); string kat = Console.ReadLine() ?? "";
                            if (opcja == "1") fm.Dodaj(new Przychod(k, o, kat));
                            else fm.Dodaj(new Wydatek(k, o, kat));
                        }
                        break;
                    case "3": fm.PokazWszystkie(); break;
                    case "4": fm.RaportStatystyczny(); break;
                    case "5":
                        fm.PokazWszystkie();
                        Console.Write("Podaj numer do usunięcia: ");
                        if (int.TryParse(Console.ReadLine() ?? "", out int n)) fm.Usun(n); 
                        break;
                    case "6": fm.RaportKategorii(); break;
                    case "7": 
                        Console.Write("Szukaj: "); fm.Szukaj(Console.ReadLine() ?? ""); 
                        break;
                    case "8": fm.Reset(); break;
                    case "0": programDziała = false; break;
                }
            }
        }
    }
}