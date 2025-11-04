using System.Globalization;
using System.Text;
using App.Core.Initialization;
using App.Data.BankAccountData;
using App.Data.CategoryData;
using App.Data.OperationData;
using App.DataSaving;
using App.DataSaving.JsonSerialization;
using App.Notification;

internal static class Program
{
    public static void Main()
    {
        Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = new CultureInfo("eng-us");

        DictOperationRepository operations = new();
        DictCategoryRepository categories = new();
        DictBankAccountRepository accounts = new();
        OperationBuilder operationBuilder = new(operations);
        CategoryFactory categoryFactory = new(categories);
        BankAccountFactory<LogNotifier> accountFactory = new(accounts);

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