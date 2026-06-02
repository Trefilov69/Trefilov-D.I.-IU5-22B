namespace Seminar7;

class Program
{
    static void Main()
    {
        var rule = new FamilyFineRule();

        Console.WriteLine("Семейный абонемент");
        Console.WriteLine($"3 дня: {rule.Calculate(3)} руб.");
        Console.WriteLine($"8 дней: {rule.Calculate(8)} руб.");
        Console.WriteLine($"100 дней: {rule.Calculate(100)} руб.");

        Console.ReadKey();
    }
}