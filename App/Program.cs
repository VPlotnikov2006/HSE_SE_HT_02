using System.Globalization;
using System.Text;
using App.Core.Initialization;
using App.Data.BankAccountData;
using App.Data.CategoryData;
using App.Data.OperationData;
using App.DataSaving;
using App.DataSaving.JsonSerialization;
using App.Notification;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceProviderKeyedExtensions
{
    public static Dictionary<object, T> GetKeyedServicesDictionary<T>(
        this IServiceProvider provider,
        IEnumerable<object> keys)
        where T : class
    {
        var dict = new Dictionary<object, T>();

        foreach (var key in keys)
        {
            var service = provider.GetKeyedService<T>(key);
            if (service is not null)
                dict[key] = service;
        }

        return dict;
    }

    public static Dictionary<KeyType, T> GetKeyedServicesDictionary<T, KeyType>(
        this IServiceProvider provider,
        IEnumerable<KeyType> keys)
        where T : class
        where KeyType : notnull
    {
        var dict = new Dictionary<KeyType, T>();

        foreach (var key in keys)
        {
            var service = provider.GetKeyedService<T>(key);
            if (service is not null)
                dict[key] = service;
        }

        return dict;
    }
}

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
            .AddKeyedSingleton<Serializer, JsonSerializer>("File")
            .AddTransient(provider =>
            {
                List<string> keys = ["File", "Console"];
                return provider.GetKeyedServicesDictionary<Serializer, string>(keys);
            })
            .BuildServiceProvider();

        IOperationRepository operations = services.GetRequiredService<IOperationRepository>();
        ICategoryRepository categories = services.GetRequiredService<ICategoryRepository>();
        IBankAccountRepository accounts = services.GetRequiredService<IBankAccountRepository>();
        OperationBuilder operationBuilder = new(operations);
        CategoryFactory categoryFactory = new(categories);
        BankAccountFactory accountFactory = services.GetRequiredService<BankAccountFactory>();

        Serializer serializer = new JsonSerializer(new());

        var cat_id1 = categoryFactory.Create(new("Зарплата", App.Core.OperationType.Income)).Id;
        var cat_id2 = categoryFactory.Create(new("Покупка в магазине", App.Core.OperationType.Expense)).Id;
        var acc_id1 = accountFactory.Create(new("Вася")).Id;
        var acc_id2 = accountFactory.Create(new("Петя", 100m)).Id;

        operationBuilder
            .SetBankAccount(acc_id1)
            .SetCategory(cat_id1)
            .SetAmount(12.1m);

        operationBuilder.Build();

        operationBuilder.SetAmount(-100m).SetCategory(cat_id2).SetBankAccount(acc_id2);
        operationBuilder.Build();

        accounts.GetById(acc_id2)?.Notify("Мало денег");
        accounts.GetById(acc_id1)?.Notify("Мало денег");

        Console.WriteLine(serializer.Serialize(accounts.GetAll()));
        Console.WriteLine();
        Console.WriteLine(serializer.Serialize(categories.GetAll()));
        Console.WriteLine();
        Console.WriteLine(serializer.Serialize(operations.GetAll()));
    }
}