using App.Core;
using App.Core.Context;

namespace App.Data.BankAccountData;

/// <summary>
/// Bank account repository based on <see cref="Dictionary"/>
/// </summary>
public class DictBankAccountRepository : IBankAccountRepository
{
    /// <summary>
    /// Dictionary with all stored accounts
    /// </summary>
    private readonly Dictionary<Guid, BankAccount> accounts = [];

    /// <inheritdoc/>
    public bool Add(BankAccount account)
    {
        if (GetById(account.Id) is not null)
        {
            return false;
        }
        accounts[account.Id] = account;
        return true;
    }

    /// <inheritdoc/>
    public bool Delete(Guid id)
    {
        return accounts.Remove(id);
    }

    /// <inheritdoc/>
    public BankAccount? GetById(Guid id)
    {
        return accounts.GetValueOrDefault(id);
    }

    /// <inheritdoc/>
    public BankAccount? GetByName(string name)
    {
        return accounts.FirstOrDefault(account => account.Value?.Name == name).Value;
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<BankAccount> GetAll()
    {
        return [.. accounts.Values];
    }

    /// <inheritdoc/>
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
