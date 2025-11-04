using App.Core;
using App.Core.Context;

namespace App.Data.BankAccountData;

public interface IBankAccountRepository: IRepository<BankAccount>
{
    public BankAccount? GetByName(string name);

    public bool Update(Guid id, BankAccountContext new_ctx);
}
