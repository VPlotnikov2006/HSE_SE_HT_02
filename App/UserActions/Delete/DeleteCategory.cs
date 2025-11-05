using App.Core;
using App.DataProvider;
using Spectre.Console;

namespace App.UserActions.Delete;

public class DeleteCategory : UserAction
{
    public override void Invoke(Application app)
    {
        string categoryInput = app.dataProvider.GetValue<string>("Enter category Id or Name", GetDataOptions.Repeat)!;

        Category? category =
            Guid.TryParse(categoryInput, out Guid catId)
                ? app.categories.GetById(catId)
                : app.categories.GetByName(categoryInput);

        if (category is null)
        {
            AnsiConsole.MarkupLine($"[red]Category '{categoryInput}' not found[/]");
            Console.ReadKey();
            return;
        }

        var relatedOps = app.operations.GetCategoryOperations(category.Id);
        foreach (var op in relatedOps)
            app.operations.Delete(op.Id);

        app.categories.Delete(category.Id);
    }
}