using App.Core.Context;
using App.DataSaving;
using App.Notification;

namespace App.Core;

public class BankAccount(INotifier notifier): ISerializable
{
    private readonly INotifier _notifier = notifier;
    private decimal balance = 0;

    public Guid Id { get; private set; }
    public string? Name { get; set; }

    public decimal Balance
    {
        get => balance;
        set => balance = value < 0 ? throw new ArgumentOutOfRangeException(nameof(Balance)) : value;
    }

    public BankAccount(INotifier notifier, Guid id) : this(notifier)
    {
        Id = id;
    }

    public BankAccount(INotifier notifier, Guid id, BankAccountContext ctx) : this(notifier, id)
    {
        Name = ctx.Name;
        Balance = ctx.Balance;
    }

    public void Notify(string Message)
    {
        _notifier.SendMessage(Name!, Message);
    }

    public void Accept(ISerializationVisitor visitor)
    {
        visitor.Visit(this);
    }
}
