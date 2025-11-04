using App.Core;
using App.Core.Context;

namespace App.Data.CategoryData;

public class DictCategoryRepository : ICategoryRepository
{
    private readonly Dictionary<Guid, Category> categories = [];

    public bool AddCategory(Category category)
    {
        if (GetCategoryById(category.Id) is not null || GetCategoryByName(category.Name!) is not null)
        {
            return false;
        }
        categories[category.Id] = category;
        return true;
    }

    public bool DeleteCategory(Guid id)
    {
        return categories.Remove(id);
    }

    public IReadOnlyCollection<Category> GetAll()
    {
        return [.. categories.Values];
    }

    public Category? GetCategoryById(Guid id)
    {
        return categories.GetValueOrDefault(id);
    }

    public Category? GetCategoryByName(string name)
    {
        return categories.FirstOrDefault(cat => cat.Value?.Name == name).Value;
    }

    public bool UpdateCategory(Guid id, CategoryContext new_ctx)
    {
        var cat1 = GetCategoryById(id);
        var cat2 = GetCategoryByName(new_ctx.Name);
        if (cat1 is null)
        {
            return false;
        }
        if (cat2 is not null)
        {
            if (cat1.Id == cat2.Id)
            {
                return true;
            }
            return false;
        }
        cat1.Name = new_ctx.Name;
        cat1.Type = new_ctx.Type;
        return true;
    }
}
