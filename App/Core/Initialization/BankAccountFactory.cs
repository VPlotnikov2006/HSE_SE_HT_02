using App.Core.Context;
using App.Data.BankAccountData;
using App.Notification;

namespace App.Core.Initialization;

/// <summary>
/// Bank account factory
/// </summary>
/// <param name="repository">Repository to store new account</param>
/// <param name="notifier">Notification system</param>
public class BankAccountFactory(IBankAccountRepository repository, INotifier notifier)
{
    /// <summary>
    /// Repository to store new account
    /// </summary>
    private readonly IBankAccountRepository _repository = repository;

    /// <summary>
    /// Notification system
    /// </summary>
    private readonly INotifier _notifier = notifier;

    /// <summary>
    /// Bank Account creation method
    /// </summary>
    /// <param name="ctx">Account parameters</param>
    /// <returns>New account, stored in <see cref="_repository"/> </returns>
    public BankAccount Create(BankAccountContext ctx)
    {
        BankAccount account = new(_notifier, Guid.NewGuid(), ctx);

        _repository.Add(account);

        return account;
    }
}
