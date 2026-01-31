int liczba;
do
{
    Console.Write("Podaj liczbę większą od zera: ");
    liczba = int.Parse(Console.ReadLine());
} while (liczba <= 0);

Console.WriteLine($"Podałeś poprawną liczbę: {liczba}");