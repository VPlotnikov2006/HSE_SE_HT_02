using System.Globalization;
using System.Text;
using App.Core.Initialization;
using App.Data.CategoryData;
using App.Data.OperationData;
using App.DataSaving;
using App.DataSaving.JsonSerialization;

internal static class Program
{
    public static void Main()
    {
        Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = new CultureInfo("eng-us");

        DictOperationRepository operations = new();
        DictCategoryRepository categories = new();
        OperationBuilder operationBuilder = new(operations);
        CategoryFactory categoryFactory = new(categories);

        Serializer serializer = new JsonSerializer(new());

        categoryFactory.Create(new("Зарплата", App.Core.OperationType.Income));
        categoryFactory.Create(new("Покупка в магазине", App.Core.OperationType.Expense));

        operationBuilder
            .SetBankAccount(Guid.NewGuid())
            .SetCategory(categories.GetByName("Зарплата")!.Id)
            .SetAmount(12.1m);

        operationBuilder.Build();

        operationBuilder.SetAmount(-100m).SetCategory(categories.GetByName("Покупка в магазине")!.Id);
        operationBuilder.Build();

        Console.WriteLine(serializer.Serialize(categories.GetAll()));
        Console.WriteLine();
        Console.WriteLine(serializer.Serialize(operations.GetAll()));
    }
}