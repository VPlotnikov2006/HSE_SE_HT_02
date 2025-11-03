namespace App.Notification;

public interface INotifier
{
    public void SendMessage(string UserName, string Message);

    public static abstract INotifier GetInstance();
}
