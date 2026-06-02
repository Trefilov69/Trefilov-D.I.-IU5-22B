namespace Seminar6;

public static class LibraryItemValidator
{
    public static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Название не может быть пустым");
    }
}