using App.Core;
using App.Core.Context;

namespace App.Data.CategoryData;

public interface ICategoryRepository
{
    public Category? GetCategoryById(Guid id);
    public Category? GetCategoryByName(string name);
    public IReadOnlyCollection<Category> GetAll();

    public bool UpdateCategory(Guid id, CategoryContext new_ctx);
    
    public bool DeleteCategory(Guid id);

    public bool AddCategory(Category category);
}
