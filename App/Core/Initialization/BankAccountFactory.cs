using App.Core.Context;
using App.Data.BankAccountData;
using App.Notification;

namespace App.Core.Initialization;

public class BankAccountFactory(IBankAccountRepository repository, INotifier notifier)
{
    private readonly IBankAccountRepository _repository = repository;
    private readonly INotifier _notifier = notifier;

    public BankAccount Create(BankAccountContext ctx)
    {
        BankAccount account = new(_notifier, Guid.NewGuid(), ctx);

        _repository.Add(account);

        return account;
    }
}
