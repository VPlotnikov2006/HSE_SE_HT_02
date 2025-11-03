using App.Core.Context;
using App.Notification;

namespace App.Core;

public class BankAccount<TNotifier>(Guid id) where TNotifier : INotifier
{
    private decimal balance = 0;

    public Guid Id { get; private set; } = id;
    public required string Name { get; set; }

    public decimal Balance
    {
        get => balance;
        set => balance = value < 0 ? throw new ArgumentOutOfRangeException(nameof(Balance)) : value;
    }

    public BankAccount(Guid _id, BankAccountContext ctx) : this(_id)
    {
        Name = ctx.Name;
        Balance = ctx.Balance;
    }

    public void Notify(string Message)
    {
        TNotifier.GetInstance().SendMessage(Name, Message);
    }
}
