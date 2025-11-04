using App.Core;
using App.Core.Context;

namespace App.Data.OperationData;

public class DictOperationRepository : IOperationRepository
{
    private readonly Dictionary<Guid, Operation> operations = [];

    public bool Add(Operation operation)
    {
        if (GetById(operation.Id) is not null)
        {
            return false;
        }
        operations[operation.Id] = operation;
        return true;
    }

    public bool Delete(Guid id)
    {
        return operations.Remove(id);
    }

    public IReadOnlyCollection<Operation> GetAccountOperations(Guid account_id)
    {
        return [.. operations.Where(op => op.Value.BankAccountId == account_id).Select(op => op.Value)];
    }

    public IReadOnlyCollection<Operation> GetAll()
    {
        return [.. operations.Values];
    }

    public IReadOnlyCollection<Operation> GetCategoryOperations(Guid category_id)
    {
        return [.. operations.Where(op => op.Value.CategoryId == category_id).Select(op => op.Value)];
    }

    public Operation? GetById(Guid id)
    {
        return operations.GetValueOrDefault(id);
    }

    public bool Update(Guid id, OperationLink new_link, OperationContext new_ctx)
    {
        var op = GetById(id);
        if (op is null)
        {
            return false;
        }
        op.BankAccountId = new_link.BankAccountId;
        op.CategoryId = new_link.CategoryId;
        op.Amount = new_ctx.Amount;
        op.Date = new_ctx.Date;
        op.Description = new_ctx.Description;
        return true;
    }
}
