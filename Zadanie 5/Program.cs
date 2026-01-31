KontoBankowe mojeKonto = new KontoBankowe();
mojeKonto.Wplata(100);
mojeKonto.Wyplata(50);
Console.WriteLine($"Moje saldo to: {mojeKonto.PobierzSaldo()}");


class KontoBankowe
{
    private double saldo = 0;

    public void Wplata(double kwota) => saldo += kwota;
    public double PobierzSaldo() => saldo;

    public void Wyplata(double kwota)
    {
        if (kwota <= saldo)
        {
            saldo -= kwota;
            Console.WriteLine($"Wypłacono {kwota} zł.");
        }
        else
        {
            Console.WriteLine("Błąd: Niewystarczające środki na koncie.");
        }
    }
}