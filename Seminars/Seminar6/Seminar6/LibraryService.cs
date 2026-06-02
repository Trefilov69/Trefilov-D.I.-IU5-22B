namespace Seminar6;

public class LibraryService
{
    private readonly List<LibraryItem> _items = new();

    private readonly ILogger _logger;

    public LibraryService(ILogger logger)
    {
        _logger = logger;
    }

    public void AddBook(
        string title,
        string author,
        int year)
    {
        LibraryItemValidator.ValidateTitle(title);

        _items.Add(
            new Book
            {
                Title = title,
                Author = author,
                Year = year
            });

        _logger.Log(
            $"Добавлена книга {title}");
    }

    public void AddMagazine(
        string title,
        int issueNumber)
    {
        LibraryItemValidator.ValidateTitle(title);

        _items.Add(
            new Magazine
            {
                Title = title,
                IssueNumber = issueNumber
            });

        _logger.Log(
            $"Добавлен журнал {title}");
    }

    public List<LibraryItem> GetItems()
    {
        return _items;
    }
}