
namespace App.Notification;

public class LogNotifier : INotifier
{
    private const string Path = @"../../../../.log";
    private static LogNotifier? _instance = null;
    private static readonly Lock locker = new();

    public LogNotifier()
    {
        File.AppendAllText(Path, $"LogNotifier was created\n");
    }

    public static INotifier GetInstance()
    {
        if (_instance is null)
        {
            lock (locker)
            {
                _instance ??= new();
            }
        }
        return _instance;
    }

    public void SendMessage(string UserName, string Message)
    {
        File.AppendAllText(Path, $"Send message \"{Message}\" to user \"{UserName}\"\n");
    }
}
