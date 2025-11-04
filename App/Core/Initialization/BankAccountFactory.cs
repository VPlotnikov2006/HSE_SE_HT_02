using App.Core.Context;
using App.Data.BankAccountData;
using App.Notification;

namespace App.Core.Initialization;

public class BankAccountFactory<TNotifier>(IBankAccountRepository<TNotifier> repository)
 where TNotifier : INotifier
{
    private readonly IBankAccountRepository<TNotifier> _repository = repository;

    public BankAccount<TNotifier> Create(BankAccountContext ctx)
    {
        BankAccount<TNotifier> account = new(Guid.NewGuid(), ctx);

        _repository.AddAccount(account);

        return account;
    }
}
