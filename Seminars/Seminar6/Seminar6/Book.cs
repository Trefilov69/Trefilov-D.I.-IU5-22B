namespace Seminar6;

public class Book : LibraryItem
{
    public string Author { get; set; } = "";

    public int Year { get; set; }

    public override string GetDisplayInfo()
    {
        return $"Книга: {Title}";
    }
}