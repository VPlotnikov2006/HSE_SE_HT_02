using App.Core;
using App.Core.Context;
using App.DataProvider;
using Spectre.Console;

namespace App.UserActions.Update;

public class UpdateOperation : UserAction
{
    public override void Invoke(Application app)
    {
        AnsiConsole.MarkupLine("[yellow]--- Update Operation ---[/]");

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
        
        var oldAccount = app.accounts.GetById(operation.BankAccountId)!;
        decimal oldBalance = oldAccount.Balance;


        decimal newAmount = app.dataProvider.GetLimitedValue<decimal>(0m, null, "Enter new amount");

        DateTime dateInput = app.dataProvider.GetValue<DateTime>("Enter operation date & time");
        DateTime newDate = dateInput == default ? DateTime.Now : dateInput;

        string? newDescription = app.dataProvider.GetValue<string>("Enter new description");

        string accountInput = app.dataProvider.GetValue<string>(
            "Enter new account Id or Name", GetDataOptions.Repeat)!;

        BankAccount? newAccount =
            Guid.TryParse(accountInput, out Guid accId)
                ? app.accounts.GetById(accId)
                : app.accounts.GetByName(accountInput);

        if (newAccount is null)
        {
            AnsiConsole.MarkupLine($"[red]Account '{accountInput}' not found[/]");
            Console.ReadKey();
            return;
        }
        
        string categoryInput = app.dataProvider.GetValue<string>(
            "Enter new category Id or Name", GetDataOptions.Repeat)!;

        Category? newCategory =
            Guid.TryParse(categoryInput, out Guid catId)
                ? app.categories.GetById(catId)
                : app.categories.GetByName(categoryInput);

        if (newCategory is null)
        {
            AnsiConsole.MarkupLine($"[red]Category '{categoryInput}' not found[/]");
            Console.ReadKey();
            return;
        }

        if (newCategory.Type == OperationType.Expense)
            newAmount = -Math.Abs(newAmount);
        else
            newAmount = Math.Abs(newAmount);

        if (operation.Amount < oldAccount.Balance)
        {
            oldAccount.Balance -= operation.Amount;
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Insufficient balance for this operation on the old account. Update aborted.[/]");
            Console.ReadKey();
        }

        if (newAccount.Balance + newAmount < 0)
        {
            oldAccount.Balance = oldBalance;

            AnsiConsole.MarkupLine("[red]Insufficient balance for this operation on the new account. Update aborted.[/]");
            Console.ReadKey();
            return;
        }

        var link = new OperationLink(newAccount.Id, newCategory.Id);
        var ctx = new OperationContext(newAmount, newDate, newDescription);
        app.operations.Update(operation.Id, link, ctx);

        AnsiConsole.MarkupLine("[green]Operation updated successfully.[/]");
        Console.ReadKey();
    }
}
