namespace App.Notification;

/// <summary>
/// Notification service interface
/// </summary>
public interface INotifier
{
    /// <summary>
    /// Sends message <paramref name="Message"/> to user <paramref name="UserName"/>
    /// </summary>
    /// <param name="UserName">User name</param>
    /// <param name="Message">Message</param>
    public void SendMessage(string UserName, string Message);

    /// <summary>
    /// Singleton pattern
    /// </summary>
    /// <returns>Returns instance of notifier</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static virtual INotifier GetInstance()
    {
        throw new NotImplementedException();
    }
}
