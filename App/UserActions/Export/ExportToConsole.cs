using App.DataSaving;
using Spectre.Console;

namespace App.UserActions.Export;

public class ExportToConsole : UserAction
{
    public override void Invoke(Application app)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[yellow]--- Export Data to Console ---[/]");

        string type = app.dataProvider.SelectValue(["BankAccount", "Category", "Operation"], "Choose object type to display:");

        Serializer? serializer = app.serializers.GetValueOrDefault("Console");
        if (serializer is null)
        {
            AnsiConsole.MarkupLine("[red]Console serializer not found![/]");
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

        AnsiConsole.WriteLine(serializedData);

        AnsiConsole.MarkupLine("[green]End of data.[/]");
        Console.ReadKey();
    }
}