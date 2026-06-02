namespace Seminar6;

public abstract class LibraryItem
{
    public string Title { get; set; } = "";

    public abstract string GetDisplayInfo();
}