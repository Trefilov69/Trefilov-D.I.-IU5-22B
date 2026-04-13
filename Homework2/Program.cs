
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

string dbPath = "zoo.db";
string zooCsv = "zoo.csv";
string animalCsv = "animal.csv";

var db = new DatabaseManager(dbPath);
db.InitializeDatabase(zooCsv, animalCsv);

while (true)
{
    Console.WriteLine("\n1 - Зоопарки");
    Console.WriteLine("2 - Животные");
    Console.WriteLine("3 - Добавить");
    Console.WriteLine("0 - Выход");
    Console.Write("Выбор: ");

    var c = Console.ReadLine();

    if (c == "1")
    {
        foreach (var z in db.GetAllZoos())
            Console.WriteLine(z);
    }
    else if (c == "2")
    {
        foreach (var a in db.GetAllAnimals())
            Console.WriteLine(a);
    }
    else if (c == "3")
    {
        Console.Write("Название: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Ошибка: пустое имя");
            continue;
        }

        Console.Write("ID зоопарка: ");
        if (!int.TryParse(Console.ReadLine(), out int zooId))
        {
            Console.WriteLine("Ошибка: введи число");
            continue;
        }

        Console.Write("Вес: ");
        if (!double.TryParse(Console.ReadLine(), out double w))
        {
            Console.WriteLine("Ошибка: введи число");
            continue;
        }

        db.AddAnimal(new Animal(0, zooId, name, w));
        Console.WriteLine("Добавлено!");
    }
    else if (c == "0") break;
}