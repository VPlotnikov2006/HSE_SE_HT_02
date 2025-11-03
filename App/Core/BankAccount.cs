using App.Notification;

namespace App.Core;

public class BankAccount<TNotifier>(Guid _id) where TNotifier : INotifier
{
    private decimal balance = 0;

    public Guid Id { get; private set; } = _id;
    public required string Name { get; set; }

    public decimal Balance
    {
        get => balance;
        set => balance = value < 0 ? throw new ArgumentOutOfRangeException(nameof(Balance)) : value;
    }

    public void Notify(string Message)
    {
        TNotifier.GetInstance().SendMessage(Name, Message);
    }
}
