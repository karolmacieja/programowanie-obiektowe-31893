// --- STAŁE I KONFIGURACJA ---
const int beerRequiredAge = 18;
const int simRequiredAge = 16;
const int shopRequiredAge = 14;

const string accessAllowedMessage = "Welcome to my shop.";
const string beerRestrictionMessage = "You must be 18 years old to buy a beer.";
const string simRestrictionMessage = "You must be 16 years old to register a SIM card.";
const string accessDeniedMessage = "You must be at least 14 years old to enter.";

// --- LOGIKA WARUNKOWA (Zadanie domowe) ---
int age = 15; // Możesz tu wpisać dowolny wiek, aby przetestować program
Console.WriteLine($"Sprawdzanie uprawnień dla wieku: {age}");

if (age >= beerRequiredAge)
{
    Console.WriteLine(accessAllowedMessage);
}
else if (age >= simRequiredAge)
{
    // Osoba ma 16-17 lat
    Console.WriteLine($"{accessAllowedMessage} {beerRestrictionMessage}");
}
else if (age >= shopRequiredAge)
{
    // Osoba ma 14-15 lat
    Console.WriteLine($"{accessAllowedMessage} {beerRestrictionMessage} {simRestrictionMessage}");
}
else
{
    // Osoba poniżej 14 lat
    Console.WriteLine(accessDeniedMessage);
}

Console.WriteLine("-----------------------------------");

// --- TABLICE I PĘTLE (Z ostatnich screenów) ---
string[] names = { "Artur", "Jan", "Agata", "Alicja", "Bartosz" };



// Pętla for wyświetlająca wszystkie elementy tablicy
for (int i = 0; i < names.Length; i++)
{
    Console.WriteLine($"{i + 1}. {names[i]}");
}