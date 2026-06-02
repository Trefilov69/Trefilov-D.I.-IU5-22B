namespace Seminar6;

public class LibraryReportPrinter
{
    public void Print(List<LibraryItem> items)
    {
        Console.WriteLine(
            $"=== Отчёт: {items.Count} элементов ===");

        foreach (var item in items)
        {
            Console.WriteLine(
                item.GetDisplayInfo());
        }

        File.WriteAllText(
            "report.txt",
            $"Всего элементов: {items.Count}");
    }
}