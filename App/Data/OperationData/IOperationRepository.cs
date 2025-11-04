using App.Core;
using App.Core.Context;

namespace App.Data.OperationData;

public interface IOperationRepository
{
    public Operation? GetOperationById(Guid id);
    public IReadOnlyCollection<Operation> GetAll();
    public IReadOnlyCollection<Operation> GetAccountOperations(Guid account_id);
    public IReadOnlyCollection<Operation> GetCategoryOperations(Guid category_id);

    public bool UpdateOperation(Guid id, OperationLink new_link, OperationContext new_ctx);

    public bool DeleteOperation(Guid id);

    public bool AddOperation(Operation operation);
}
