using App.Core;
using App.Core.Context;

namespace App.Data.OperationData;

/// <summary>
/// Repository for all operations
/// </summary>
public interface IOperationRepository : IRepository<Operation>
{
    /// <summary>
    /// Get all operations from current account
    /// </summary>
    /// <param name="account_id">Account id</param>
    /// <returns></returns>
    public IReadOnlyCollection<Operation> GetAccountOperations(Guid account_id);

    /// <summary>
    /// Get all operations with current category
    /// </summary>
    /// <param name="category_id">Category id</param>
    /// <returns></returns>
    public IReadOnlyCollection<Operation> GetCategoryOperations(Guid category_id);

    /// <summary>
    /// Update operation parameters / references
    /// </summary>
    /// <param name="id">Operation id</param>
    /// <param name="new_link">New operation references</param>
    /// <param name="new_ctx">New operation parameters</param>
    /// <returns><see cref="true"/> if operation successfully finished, otherwise <see cref="false"/></returns>
    public bool Update(Guid id, OperationLink new_link, OperationContext new_ctx);
}
