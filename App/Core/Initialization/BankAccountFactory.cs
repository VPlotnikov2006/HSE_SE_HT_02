using App.Core.Context;
using App.Data.BankAccountData;
using App.Notification;

namespace App.Core.Initialization;

public class BankAccountFactory<TNotifier>(IBankAccountRepository repository)
 where TNotifier : INotifier
{
    private readonly IBankAccountRepository _repository = repository;

    public BankAccount Create(BankAccountContext ctx)
    {
        BankAccount account = new(TNotifier.GetInstance(), Guid.NewGuid(), ctx);

        _repository.Add(account);

        return account;
    }
}
