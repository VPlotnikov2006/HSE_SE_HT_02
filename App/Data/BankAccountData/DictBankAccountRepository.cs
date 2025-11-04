using App.Core;
using App.Core.Context;
using App.Notification;

namespace App.Data.BankAccountData;

public class DictBankAccountRepository<TNotifier> : IBankAccountRepository<TNotifier>
 where TNotifier : INotifier
{
    private readonly Dictionary<Guid, BankAccount<TNotifier>> accounts = [];

    public bool Add(BankAccount<TNotifier> account)
    {
        if (GetById(account.Id) is not null)
        {
            return false;
        }
        accounts[account.Id] = account;
        return true;
    }

    public bool Delete(Guid id)
    {
        return accounts.Remove(id);
    }

    public BankAccount<TNotifier>? GetById(Guid id)
    {
        return accounts.GetValueOrDefault(id);
    }

    public BankAccount<TNotifier>? GetByName(string name)
    {
        return accounts.FirstOrDefault(account => account.Value?.Name == name).Value;
    }

    public IReadOnlyCollection<BankAccount<TNotifier>> GetAll()
    {
        return [.. accounts.Values];
    }

    public bool Update(Guid id, BankAccountContext new_ctx)
    {
        var acc = GetById(id);
        if (acc is null)
        {
            return false;
        }
        acc.Balance = new_ctx.Balance;
        acc.Name = new_ctx.Name;
        return true;
    }
}
