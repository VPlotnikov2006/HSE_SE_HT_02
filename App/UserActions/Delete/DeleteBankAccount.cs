using App.Core;
using App.DataProvider;
using Spectre.Console;

namespace App.UserActions.Delete;

public class DeleteBankAccount : UserAction
{
    public override void Invoke(Application app)
    {
        string accountInput = app.dataProvider.GetValue<string>("Enter account Id or Name", GetDataOptions.Repeat)!;

        BankAccount? account =
            Guid.TryParse(accountInput, out Guid accId)
                ? app.accounts.GetById(accId)
                : app.accounts.GetByName(accountInput);

        if (account is null)
        {
            AnsiConsole.MarkupLine($"[red]Account '{accountInput}' not found[/]");
            Console.ReadKey();
            return;
        }

        var relatedOps = app.operations.GetAccountOperations(account.Id);
        foreach (var op in relatedOps)
            app.operations.Delete(op.Id);

        app.accounts.Delete(account.Id);
    }
}