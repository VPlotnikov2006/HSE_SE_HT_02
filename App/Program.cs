using System.Globalization;
using System.Text;
using App;
using App.Core.Initialization;
using App.Data.BankAccountData;
using App.Data.CategoryData;
using App.Data.OperationData;
using App.DataProvider;
using App.DataSaving;
using App.DataSaving.ConsoleSerialization;
using App.DataSaving.JsonSerialization;
using App.Notification;
using App.UserActions;
using App.UserActions.ActionDecorators;
using App.UserActions.Create;
using App.UserActions.Delete;
using App.UserActions.Export;
using App.UserActions.Update;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

internal static class Program
{
    public static void Main()
    {
        Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = new CultureInfo("eng-us");

        var services = new ServiceCollection()
            .AddSingleton<IOperationRepository, DictOperationRepository>()
            .AddSingleton<IBankAccountRepository, DictBankAccountRepository>()
            .AddSingleton<ICategoryRepository, DictCategoryRepository>()
            .AddTransient<INotifier, LogNotifier>(_ => (LogNotifier)LogNotifier.GetInstance())
            .AddSingleton<BankAccountFactory>()
            .AddSingleton<CategoryFactory>()
            .AddSingleton<OperationBuilder>()
            .AddSingleton<JsonSerializationVisitor>()
            .AddSingleton<ConsoleSerializationVisitor>()
            .AddSingleton<IDataProvider, SCDataProvider>()
            .AddTransient<ActionDecorator, TimerDecorator>()
            .AddKeyedSingleton<Serializer, JsonSerializer>("File")
            .AddKeyedSingleton<Serializer, ConsoleSerializer>("Console")
            .AddTransient(provider =>
                {
                    List<string> keys = ["File", "Console"];
                    return provider.GetKeyedServicesDictionary<string, Serializer>(keys);
                }
            )
            .AddKeyedTransient<UserAction, CreateBankAccount>("Create/Bank Account")
            .AddKeyedTransient<UserAction, CreateCategory>("Create/Category")
            .AddKeyedTransient<UserAction, CreateOperation>("Create/Operation")
            .AddKeyedTransient<UserAction, DeleteBankAccount>("Delete/Bank Account")
            .AddKeyedTransient<UserAction, DeleteCategory>("Delete/Category")
            .AddKeyedTransient<UserAction, DeleteOperation>("Delete/Operation")
            .AddKeyedTransient<UserAction, UpdateBankAccount>("Update/Bank Account")
            .AddKeyedTransient<UserAction, UpdateCategory>("Update/Category")
            .AddKeyedTransient<UserAction, UpdateOperation>("Update/Operation")
            .AddKeyedTransient<UserAction, ExportToFile>("Export/To File")
            .AddKeyedTransient<UserAction, ExportToConsole>("Export/To Console")
            .AddKeyedTransient<UserAction, Exit>("Exit")
            .AddTransient(
                provider =>
                {
                    List<string> paths = [
                        "Exit",
                        "Create/Bank Account",
                        "Create/Category",
                        "Create/Operation",
                        "Delete/Bank Account",
                        "Delete/Category",
                        "Delete/Operation",
                        "Update/Bank Account",
                        "Update/Category",
                        "Update/Operation",
                        "Export/To File",
                        "Export/To Console"
                    ];
                    var actions = provider.GetKeyedServicesDictionary<string, UserAction>(paths);
                    Dictionary<string, UserAction> result = [];
                    foreach ((string key, UserAction action) in actions)
                    {
                        var decorator = provider.GetRequiredService<ActionDecorator>();
                        decorator.Action = action;
                        result[key] = decorator;
                    }
                    return result;
                }
            )
            .AddTransient<Application>()
            .BuildServiceProvider();

        Application application = services.GetRequiredService<Application>();
        while (true)
        {
            AnsiConsole.Clear();
            application.AskUserAction();
        }
    }
}