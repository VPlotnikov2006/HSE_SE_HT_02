namespace App.Notification;

/// <summary>
/// Notifier to the .log file
/// </summary>
public class LogNotifier : INotifier
{
    /// <summary>
    /// Path to file
    /// </summary>
    private const string Path = @"../../../../.log";

    /// <summary>
    /// Instance (for singleton pattern)
    /// </summary>
    private static LogNotifier? _instance = null;

    /// <summary>
    /// Locker (for singleton pattern)
    /// </summary>
    private static readonly Lock locker = new();

    /// <summary>
    /// Constructor, sends log to the file
    /// </summary>
    public LogNotifier()
    {
        File.AppendAllText(Path, $"{DateTimeOffset.Now:yyyy/MM/dd HH:mm:ss.fffzzz} | LogNotifier was created\n");
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public void SendMessage(string UserName, string Message)
    {
        File.AppendAllText(Path, $"{DateTimeOffset.Now:yyyy/MM/dd HH:mm:ss.fffzzz} | Send message \"{Message}\" to user \"{UserName}\"\n");
    }
}
