using App.Core;
using App.Core.Context;

namespace App.Data.OperationData;

public interface IOperationRepository: IRepository<Operation>
{
    public IReadOnlyCollection<Operation> GetAccountOperations(Guid account_id);
    public IReadOnlyCollection<Operation> GetCategoryOperations(Guid category_id);

    public bool Update(Guid id, OperationLink new_link, OperationContext new_ctx);
}
