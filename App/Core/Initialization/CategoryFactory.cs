using App.DataProvider;

namespace App.Core.Initialization;

public class CategoryFactory(IDataProvider provider): EntityFactory<Category>(provider)
{
    public override Category Create()
    {
        return new(Guid.NewGuid())
        {
            Name = _provider.GetValue<string>("Enter account name", GetDataOptions.Repeat)!,
            Type = _provider.SelectValue(Enum.GetValues<OperationType>(), "Select operation type")
        };
    }

    public Category Create(string Name)
    {
        return new(Guid.NewGuid())
        {
            Name = Name,
            Type = _provider.SelectValue(Enum.GetValues<OperationType>(), "Select operation type")
        };
    }
}
