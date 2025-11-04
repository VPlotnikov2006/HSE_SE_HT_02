using App.Core.Context;
using App.Data.CategoryData;

namespace App.Core.Initialization;

public class CategoryFactory(ICategoryRepository repository)
{
    private readonly ICategoryRepository _repository = repository;

    public Category Create(CategoryContext ctx)
    {
        Category cat = new(Guid.NewGuid(), ctx);

        _repository.AddCategory(cat);

        return cat;
    }
}
