using Seminar6;

ILogger logger = new FileLogger();

LibraryService service =
    new LibraryService(logger);

service.AddBook(
    "Чистый код",
    "Роберт Мартин",
    2008);

service.AddMagazine(
    "IT Magazine",
    12);

LibraryReportPrinter printer =
    new LibraryReportPrinter();

printer.Print(
    service.GetItems());

Console.ReadKey();