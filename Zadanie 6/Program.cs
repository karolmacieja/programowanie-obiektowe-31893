Kot mojKot = new Kot();
mojKot.Jedz();   
mojKot.Miaucz();  


class Zwierze
{
    public void Jedz() => Console.WriteLine("Zwierzę je");
}

class Kot : Zwierze
{
    public void Miaucz() => Console.WriteLine("Miau miau!");
}