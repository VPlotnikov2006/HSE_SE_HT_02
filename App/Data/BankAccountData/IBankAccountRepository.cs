using App.Core;
using App.Notification;

namespace App.Data.BankAccountData;

public interface IBankAccountRepository<TNotifier>
 where TNotifier : INotifier
{
    public BankAccount<TNotifier> GetAccountByName();
    public BankAccount<TNotifier> GetAccountById();

    public bool UpdateAccount(Guid id, );
}
