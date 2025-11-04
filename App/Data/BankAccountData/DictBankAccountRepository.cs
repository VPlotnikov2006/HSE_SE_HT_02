using App.Core;
using App.Core.Context;
using App.Notification;

namespace App.Data.BankAccountData;

public class DictBankAccountRepository<TNotifier> : IBankAccountRepository<TNotifier>
 where TNotifier : INotifier
{
    private readonly Dictionary<Guid, BankAccount<TNotifier>> accounts = [];

    public bool AddAccount(BankAccount<TNotifier> account)
    {
        if (GetAccountById(account.Id) is not null)
        {
            return false;
        }
        accounts[account.Id] = account;
        return true;
    }

    public bool DeleteAccount(Guid id)
    {
        return accounts.Remove(id);
    }

    public BankAccount<TNotifier>? GetAccountById(Guid id)
    {
        return accounts.GetValueOrDefault(id);
    }

    public BankAccount<TNotifier>? GetAccountByName(string name)
    {
        return accounts.FirstOrDefault(account => account.Value?.Name == name).Value;
    }

    public IReadOnlyCollection<BankAccount<TNotifier>> GetAll()
    {
        return [.. accounts.Values];
    }

    public bool UpdateAccount(Guid id, BankAccountContext new_ctx)
    {
        var acc = GetAccountById(id);
        if (acc is null)
        {
            return false;
        }
        acc.Balance = new_ctx.Balance;
        acc.Name = new_ctx.Name;
        return true;
    }
}
