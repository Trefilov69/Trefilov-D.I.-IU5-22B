namespace Seminar6;

public class Magazine : LibraryItem
{
    public int IssueNumber { get; set; }

    public override string GetDisplayInfo()
    {
        return $"Журнал: {Title}";
    }
}