using App.Core.Context;
using App.DataSaving;
using App.Notification;

namespace App.Core;

/// <summary>
/// Bank account class
/// </summary>
/// <param name="notifier">Notification system</param>
public class BankAccount(INotifier notifier) : ISerializable
{
    /// <summary>
    /// Notification system
    /// </summary>
    private readonly INotifier _notifier = notifier;

    /// <summary>
    /// Account balance
    /// </summary>
    private decimal balance = 0;

    /// <summary>
    /// Account Id
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Account name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Property for account balance
    /// </summary>
    public decimal Balance
    {
        get => balance;
        set => balance = value < 0 ? throw new ArgumentOutOfRangeException(nameof(Balance)) : value;
    }

    /// <summary>
    /// Account constructor with Id
    /// </summary>
    /// <param name="notifier">Notification system</param>
    /// <param name="id">Account Id</param>
    public BankAccount(INotifier notifier, Guid id) : this(notifier)
    {
        Id = id;
    }

    /// <summary>
    /// Account constructor
    /// </summary>
    /// <param name="notifier">Notification system</param>
    /// <param name="id">Account Id</param>
    /// <param name="ctx">Account parameters</param>
    public BankAccount(INotifier notifier, Guid id, BankAccountContext ctx) : this(notifier, id)
    {
        Name = ctx.Name;
        Balance = ctx.Balance;
    }

    /// <summary>
    /// Suspicious transaction notification
    /// </summary>
    /// <param name="Message">Message</param>
    public void Notify(string Message)
    {
        _notifier.SendMessage(Name!, Message);
    }

    /// <inheritdoc/>
    public void Accept(ISerializationVisitor visitor)
    {
        visitor.Visit(this);
    }
}
