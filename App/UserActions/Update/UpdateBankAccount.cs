using App.Core;
using App.Core.Context;
using App.DataProvider;
using Spectre.Console;

namespace App.UserActions.Update;

public class UpdateBankAccount : UserAction
{
    public override void Invoke(Application app)
    {
        AnsiConsole.MarkupLine("[yellow]--- Update Bank Account ---[/]");

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

        string newName = app.dataProvider.GetValue<string>($"Enter new name", GetDataOptions.Repeat)!;
        decimal newBalance = app.dataProvider.GetLimitedValue<decimal>(0m, null, $"Enter new balance");

        var ctx = new BankAccountContext
        (
            Name: newName,
            Balance: newBalance
        );
        app.accounts.Update(account.Id, ctx);

        AnsiConsole.MarkupLine("[green]Bank account updated successfully.[/]");
        Console.ReadKey();
    }
}
