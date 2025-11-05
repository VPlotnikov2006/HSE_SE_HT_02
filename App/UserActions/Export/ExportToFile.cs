using App.DataProvider;
using App.DataSaving;
using Spectre.Console;

namespace App.UserActions.Export;

/// <summary>
/// Export to file action
/// </summary>
public class ExportToFile : UserAction
{
    /// <inheritdoc/>
    public override void Invoke(Application app)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[yellow]--- Export Data to File ---[/]");

        string type = app.dataProvider.SelectValue(["BankAccount", "Category", "Operation"], "Choose object type to export:");

        Serializer? serializer = app.serializers.GetValueOrDefault("File");
        if (serializer is null)
        {
            AnsiConsole.MarkupLine("[red]File serializer not found![/]");
            Console.ReadKey();
            return;
        }

        IReadOnlyCollection<ISerializable> objects = type switch
        {
            "BankAccount" => [.. app.accounts.GetAll().Cast<ISerializable>()],
            "Category" => [.. app.categories.GetAll().Cast<ISerializable>()],
            "Operation" => [.. app.operations.GetAll().Cast<ISerializable>()],
            _ => Array.Empty<ISerializable>()
        };

        string serializedData = serializer.Serialize(objects);

        string path = app.dataProvider.GetValue<string>("Enter file path to save", GetDataOptions.Repeat)!;

        File.WriteAllText(path, serializedData);

        AnsiConsole.MarkupLine($"[green]Data exported successfully to {path}.[/]");
        Console.ReadKey();
    }
}