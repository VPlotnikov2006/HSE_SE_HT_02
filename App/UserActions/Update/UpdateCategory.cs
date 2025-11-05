using App.Core;
using App.Core.Context;
using App.DataProvider;
using Spectre.Console;

namespace App.UserActions.Update;

public class UpdateCategory : UserAction
{
    public override void Invoke(Application app)
    {
        AnsiConsole.MarkupLine("[yellow]--- Update Category ---[/]");

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

        string newName = app.dataProvider.GetValue<string>("Enter new name", GetDataOptions.Repeat)!;

        var ctx = new CategoryContext
        (
            Name: newName,
            Type: category.Type
        );

        
        app.categories.Update(category.Id, ctx);


        AnsiConsole.MarkupLine("[green]Category updated successfully.[/]");
        Console.ReadKey();
    }
}