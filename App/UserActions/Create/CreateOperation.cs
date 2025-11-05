using App.Core;
using App.Core.Context;
using App.DataProvider;
using Spectre.Console;

namespace App.UserActions.Create;

/// <summary>
/// Create operation action
/// </summary>
public class CreateOperation : UserAction
{
    /// <inheritdoc/>
    public override void Invoke(Application app)
    {
        string accountInput = app.dataProvider.GetValue<string>(
            "Enter account Id or Name", GetDataOptions.Repeat)!;

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

        string categoryInput = app.dataProvider.GetValue<string>(
            "Enter category Id or Name", GetDataOptions.Repeat)!;

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

        decimal amount = app.dataProvider.GetValue<decimal>("Enter operation amount", GetDataOptions.Repeat)!;

        if (category.Type == OperationType.Expense)
            amount = -Math.Abs(amount);
        else
            amount = Math.Abs(amount);

        if (category.Type == OperationType.Expense && account.Balance + amount < 0)
        {
            AnsiConsole.MarkupLine($"[red]Insufficient funds in the account '{account.Name}'[/]");
            AnsiConsole.MarkupLine($"Current balance: [yellow]{account.Balance}[/]");
            Console.ReadKey();
            return;
        }

        DateTime dateInput = app.dataProvider.GetValue<DateTime>("Enter operation date & time");
        DateTime date = dateInput == default ? DateTime.Now : dateInput;

        string? desc = app.dataProvider.GetValue<string>("Enter description");

        _ = app.operationBuilder
            .SetBankAccount(account.Id)
            .SetCategory(category.Id)
            .SetAmount(amount)
            .SetDate(date)
            .SetDescription(desc)
            .Build();

        app.accounts.Update(account.Id, new BankAccountContext
        (
            Name: account.Name!,
            Balance: account.Balance + amount
        ));
    }
}