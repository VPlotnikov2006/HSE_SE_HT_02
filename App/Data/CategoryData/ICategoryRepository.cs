using App.Core;
using App.Core.Context;

namespace App.Data.CategoryData;

public interface ICategoryRepository: IRepository<Category>
{
    public Category? GetByName(string name);

    public bool Update(Guid id, CategoryContext new_ctx);
}
