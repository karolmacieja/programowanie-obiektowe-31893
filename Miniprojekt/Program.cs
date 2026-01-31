ElektrycznySamochod tesla = new ElektrycznySamochod();
tesla.Start();
tesla.Jedz();
tesla.Laduj();


class Pojazd
{
    public virtual void Start() => Console.WriteLine("Pojazd uruchomiony");
}

class Samochod : Pojazd
{
    public void Jedz() => Console.WriteLine("Samochód jedzie");
}

class ElektrycznySamochod : Samochod
{
    public void Laduj() => Console.WriteLine("Ładowanie baterii...");
}