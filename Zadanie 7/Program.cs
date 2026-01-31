Zwierze[] zoo = { new Pies(), new Kot() }; 


foreach (Zwierze z in zoo) 
{
    z.DajGlos(); 
}


class Zwierze
{
    public virtual void DajGlos() => Console.WriteLine("Zwierze wydaje dźwięk");
}

class Pies : Zwierze
{
    public override void DajGlos() => Console.WriteLine("Hau hau!");
}

class Kot : Zwierze
{
    public override void DajGlos() => Console.WriteLine("Miau!");
}