using App.DataProvider;
using Spectre.Console;

namespace App.UserActions.Delete;

public class DeleteOperation : UserAction
{
    public override void Invoke(Application app)
    {

        string operationInput = app.dataProvider.GetValue<string>("Enter operation Id", GetDataOptions.Repeat)!;

        if (!Guid.TryParse(operationInput, out Guid opId))
        {
            AnsiConsole.MarkupLine("[red]Wrong Guid format[/]");
            Console.ReadKey();
            return;
        }

        var operation = app.operations.GetById(opId);
        if (operation is null)
        {
            AnsiConsole.MarkupLine("[red]Operation not found[/]");
            Console.ReadKey();
            return;
        }

        app.operations.Delete(opId);
    }
}
