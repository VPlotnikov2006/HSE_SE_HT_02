using App.Core;
using App.Core.Context;
using App.Notification;

namespace App.Data.BankAccountData;

public interface IBankAccountRepository<TNotifier>: IRepository<BankAccount<TNotifier>>
 where TNotifier : INotifier
{
    public BankAccount<TNotifier>? GetByName(string name);

    public bool Update(Guid id, BankAccountContext new_ctx);
}
