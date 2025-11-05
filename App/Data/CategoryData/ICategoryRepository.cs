using App.Core;
using App.Core.Context;

namespace App.Data.CategoryData;

/// <summary>
/// Repository for all categories
/// </summary>
public interface ICategoryRepository : IRepository<Category>
{
    /// <summary>
    /// Search category by name
    /// </summary>
    /// <param name="name">Category name</param>
    /// <returns>Category with current <paramref name="name"/> or <see cref="null"/></returns>
    public Category? GetByName(string name);

    /// <summary>
    /// Update category parameters
    /// </summary>
    /// <param name="id">Category id</param>
    /// <param name="new_ctx">New category parameters</param>
    /// <returns><see cref="true"/> if operation successfully finished, otherwise <see cref="false"/></returns>
    public bool Update(Guid id, CategoryContext new_ctx);
}
