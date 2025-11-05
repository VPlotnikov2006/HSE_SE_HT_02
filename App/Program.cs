using System.Globalization;
using System.Text;
using App;
using App.Core.Initialization;
using App.Data.BankAccountData;
using App.Data.CategoryData;
using App.Data.OperationData;
using App.DataProvider;
using App.DataSaving;
using App.DataSaving.JsonSerialization;
using App.Notification;
using App.UserActions;
using App.UserActions.ActionDecorators;
using App.UserActions.Create;
using Microsoft.Extensions.DependencyInjection;

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
            .AddSingleton<IDataProvider, SCDataProvider>()
            .AddTransient<ActionDecorator, TimerDecorator>()
            .AddKeyedSingleton<Serializer, JsonSerializer>("File")
            .AddTransient(provider =>
                {
                    List<string> keys = ["File", "Console"];
                    return provider.GetKeyedServicesDictionary<string, Serializer>(keys);
                }
            )
            .AddKeyedTransient<UserAction, CreateBankAccount>("Create/Bank Account")
            .AddTransient(
                provider =>
                {
                    List<string> paths = [
                        "Create/Bank Account"
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
        application.AskUserAction();
    }
}