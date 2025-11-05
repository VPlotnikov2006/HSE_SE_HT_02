using App.Core;
using App.Core.Context;

namespace App.Data.BankAccountData;

/// <summary>
/// Repository for all accounts
/// </summary>
public interface IBankAccountRepository : IRepository<BankAccount>
{
    /// <summary>
    /// Search account by name
    /// </summary>
    /// <param name="name">Account name</param>
    /// <returns>Account with current <paramref name="name"/> or <see cref="null"/></returns>
    public BankAccount? GetByName(string name);

    /// <summary>
    /// Update account parameters
    /// </summary>
    /// <param name="id">Account id</param>
    /// <param name="new_ctx">New account parameters</param>
    /// <returns><see cref="true"/> if operation successfully finished, otherwise <see cref="false"/></returns>
    public bool Update(Guid id, BankAccountContext new_ctx);
}
