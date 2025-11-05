using App.Core.Context;
using App.Data.CategoryData;

namespace App.Core.Initialization;

/// <summary>
/// Category factory
/// </summary>
/// <param name="repository">Repository to store new category</param>
public class CategoryFactory(ICategoryRepository repository)
{
    /// <summary>
    /// Repository to store new category
    /// </summary>
    private readonly ICategoryRepository _repository = repository;

    /// <summary>
    /// Category creation method
    /// </summary>
    /// <param name="ctx">Category parameters</param>
    /// <returns>New category, stored in <see cref="_repository"/> </returns>
    public Category Create(CategoryContext ctx)
    {
        Category cat = new(Guid.NewGuid(), ctx);

        _repository.Add(cat);

        return cat;
    }
}
