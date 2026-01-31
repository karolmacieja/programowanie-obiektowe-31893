Osoba osoba1 = new Osoba { Imie = "Marek", Wiek = 22 };
Osoba osoba2 = new Osoba { Imie = "Kasia", Wiek = 28 };

osoba1.PrzedstawSie();
osoba2.PrzedstawSie();


class Osoba
{
    public string Imie;
    public int Wiek;

    public void PrzedstawSie()
    {
        Console.WriteLine($"Cześć, mam na imię {Imie} i mam {Wiek} lat.");
    }
}