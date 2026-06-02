namespace Seminar6;

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        File.AppendAllText(
            "library.log",
            $"{DateTime.Now:u}: {message}\n");
    }
}