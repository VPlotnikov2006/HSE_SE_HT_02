using App.DataProvider;

namespace App.Core.Initialization;

public abstract class EntityFactory<T>(IDataProvider provider)
{
    protected readonly IDataProvider _provider = provider;
    public abstract T Create();
}
